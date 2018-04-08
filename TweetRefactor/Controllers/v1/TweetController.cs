using Microsoft.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TweetCore.Interfaces;
using TweetCore.Services;

namespace TweetRefactor.Controllers.v1
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/tweets")]
    public class TweetController : ApiController
    {
        private readonly ITweetService _tweetService;
        public TweetController()
        {
            // DI can be used
            _tweetService = new TweetService();
        }

        [HttpGet]
        [Route("")]

        /// <summary>
        /// Returns collections of unique tweets between specified period.
        /// </summary>
        /// <param name="startDate">Optional parameter - start date</param>
        /// <param name="endDate">Optional parameter - start date</param>
        /// <returns>Returns collections of unique tweets</returns>
        public async Task<IHttpActionResult> GetAllTweetsForSpecifiedPeriod(Nullable<DateTime> startDate = null, Nullable<DateTime> endDate = null)
        {
            return Ok(await _tweetService.GetAllTweetsForSpecifiedPeriod(startDate, endDate));
        }

        [Route("{id}")]
        // GET api/<controller>/5
        public string Get(string id)
        {
            return "Fake Tweet";
        }

    }
}
