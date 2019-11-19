using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataObjects
{
    public class UserFeedback
    {
        public int UserId { get; set; }

        public int ItemId { get; set; }

        public ItemType ItemType { get; set; }

        public bool IsLike { get; set; }

        public DateTime FeedbackTime { get; set; }
    }
}
