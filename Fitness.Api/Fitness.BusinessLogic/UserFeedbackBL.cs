using Fitness.DataAccess;
using Fitness.DataObjects;
using System.Collections.Generic;

namespace Fitness.BusinessLogic
{
    public class UserFeedbackBL
    {
        UserFeedbackDA userFeedbackDA = null;

        public UserFeedbackBL()
        {
            userFeedbackDA = new UserFeedbackDA();
        }

        public List<UserFeedback> GetRecipeFeedbackByUser(int userId)
        {
            return userFeedbackDA.GetRecipeFeedbackByUser(userId);
        }

        public void RecordUserFeedback(UserFeedback userFeedback)
        {
            userFeedbackDA.RecordUserFeedback(userFeedback);
        }
    }
}
