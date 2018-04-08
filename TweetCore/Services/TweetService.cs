using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using TweetCore.Interfaces;
using TweetCore.Model;

namespace TweetCore.Services
{
    public class TweetService : ITweetService
    {
        #region Private Declarations

        private HttpClient _client;
        private const string _baseAddress = "https://badapi.iqvia.io/api/v1/";

        #endregion

        #region Ctor
        public TweetService()
        {
            Initialize();
        }

        #endregion

        #region Private Methods
        private void Initialize()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseAddress);

        }

        private async Task<ICollection<Tweet>> GetAllTweetsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var tweetUrl = string.Format("Tweets?startDate={0}&endDate={1}", startDate, endDate.ToString("yyyy-MM-dd"));

                Console.WriteLine(tweetUrl);
                var response = await _client.GetAsync(tweetUrl).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<Tweet>>().ConfigureAwait(false);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns collections of unique tweets between specified period.
        /// </summary>
        /// <param name="startDate">Optional parameter - start date</param>
        /// <param name="endDate">Optional parameter - start date</param>
        /// <returns>Returns collections of unique tweets</returns>
        public async Task<ICollection<Tweet>> GetAllTweetsForSpecifiedPeriod(Nullable<DateTime> startDate = null, Nullable<DateTime> endDate = null)
        {
            var lstTweets = new List<Tweet>();

            if (startDate == null || endDate == null)
            {
                //Default date range as per requirement i.e. all tweets between 2016 and 2017.
                startDate = new DateTime(2016, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                endDate = new DateTime(2017, 12, 31, 23, 59, 59, DateTimeKind.Utc);
            }

            lstTweets.AddRange(await GetAllTweetsByDateRange((DateTime)startDate, (DateTime)endDate));

            if (lstTweets.Any())
            {
                var lastRecordStamp = lstTweets[lstTweets.Count - 1].stamp;

                while (lastRecordStamp <= endDate)
                {
                    startDate = lastRecordStamp;

                    lstTweets.AddRange(await GetAllTweetsByDateRange((DateTime)startDate, (DateTime)endDate));

                    lastRecordStamp = lstTweets[lstTweets.Count - 1].stamp;

                    if (startDate == lastRecordStamp)
                        break;
                }
            }

            return lstTweets.Distinct().ToList();
        }

        #endregion

    }
}
