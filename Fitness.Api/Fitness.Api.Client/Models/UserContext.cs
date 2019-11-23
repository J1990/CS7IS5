using Fitness.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fitness.Api.Client.Models
{
    public static class UserContext
    {
        private static User _user;

        public static int UserId { get { return _user.Id; } }
        public static string UserName { get { return _user.UserName; } }
        public static string UserEmailAddress { get { return _user.Email; } }

        static UserContext()
        {
            _user = new User();
        }

        public static void SetUserContext(User user)
        {
            _user = user;
        }
    }
}