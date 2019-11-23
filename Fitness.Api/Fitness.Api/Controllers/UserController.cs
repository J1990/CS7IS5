using System.Collections.Generic;
using System.Web.Http;
using Fitness.DataObjects;
using Fitness.BusinessLogic;

namespace Fitness.Api.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<User> Get()
        {
            UserBL userBL = new UserBL();
            return userBL.GetAllUsers();
        }

        // GET: api/User/5
        public User Get(int id)
        {
            UserBL userBL = new UserBL();
            return userBL.GetUserById(id);
        }

        // POST: api/User
        public void Post([FromBody]User newUser)
        {
            UserBL userBL = new UserBL();
            userBL.AddNewUser(newUser);
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
