using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetCore.Services;

namespace TweetRefactorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var tweetService = new TweetService();

            var tweets = tweetService.GetAllTweetsForSpecifiedPeriod().Result;
        }
    }
}
