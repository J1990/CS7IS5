using Fitness.BusinessLogic;
using Fitness.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fitness.Api.Controllers
{
    public class UserProfileController : ApiController
    {
        public UserProfile Get(int id)
        {
            UserProfileBL userProfileBL = new UserProfileBL();
            return userProfileBL.GetLatestUserProfileForUser(id);
        }
    }
}
