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
            var userProfile = userProfileDA.GetLatestUserProfileForUser(userId);

            var dailyCalorieNeedsOfUser = BMRProvider.CalculateBMR(userProfile.WeightInKg, userProfile.HeightInCm, userProfile.AgeInYears, userProfile.Gender);

            userProfile.BMR = dailyCalorieNeedsOfUser;

            return userProfile;
        }
    }
}
