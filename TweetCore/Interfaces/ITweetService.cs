using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetCore.Model;

namespace TweetCore.Interfaces
{
    public interface ITweetService
    {
        Task<ICollection<Tweet>> GetAllTweetsForSpecifiedPeriod(Nullable<DateTime> startDate = null, Nullable<DateTime> endDate = null);
    }
}
