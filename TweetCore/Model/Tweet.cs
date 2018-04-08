using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetCore.Model
{
    public class Tweet : IEquatable<Tweet>
    {
        public string id { get; set; }
        public DateTime stamp { get; set; }
        public string text { get; set; }

        public bool Equals(Tweet other)
        {
            return id.Equals(other.id) && stamp.Equals(other.stamp) && text.Equals(other.text);
        }
    }
}
