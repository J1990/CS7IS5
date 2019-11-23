using Fitness.DataAccess;
using Fitness.DataObjects;

namespace Fitness.BusinessLogic
{
    public class UserProfileBL
    {
        UserProfileDA userProfileDA = null;

        public UserProfileBL()
        {
            userProfileDA = new UserProfileDA();
        }

        public UserProfile GetLatestUserProfileForUser(int userId)
        {
            return userProfileDA.GetLatestUserProfileForUser(userId);
        }
    }
}
