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

        public List<UserFeedback> GetFeedbackByUser(int userId)
        {
            return userFeedbackDA.GetFeedbackByUser(userId);
        }

        public void RecordUserFeedback(UserFeedback userFeedback)
        {
            userFeedbackDA.RecordUserFeedback(userFeedback);
        }
    }
}
