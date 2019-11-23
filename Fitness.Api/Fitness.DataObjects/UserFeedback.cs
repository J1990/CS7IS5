using System;

namespace Fitness.DataObjects
{
    public class UserFeedback
    {
        public int UserId { get; set; }

        public int ItemId { get; set; }

        public ItemType ItemType { get; set; }

        public UserFeedbackType FeedbackType { get; set; }

        public DateTime FeedbackTime { get; set; }
    }
}