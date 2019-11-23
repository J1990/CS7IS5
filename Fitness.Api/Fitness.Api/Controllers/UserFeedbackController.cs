using System.Collections.Generic;
using System.Web.Http;
using Fitness.DataObjects;
using Fitness.BusinessLogic;

namespace Fitness.Api.Controllers
{
    public class UserFeedbackController : ApiController
    {
        // GET: api/UserFeedback/5
        //public List<UserFeedback> Get(int userId)
        //{
        //    UserFeedbackBL userFeedbackBL = new UserFeedbackBL();
        //    return userFeedbackBL.GetFeedbackByUser(userId);
        //}

        // POST: api/UserFeedback
        public void Post([FromBody]UserFeedback newUserFeedback)
        {
            UserFeedbackBL userFeedbackBL = new UserFeedbackBL();
            userFeedbackBL.RecordUserFeedback(newUserFeedback);
        }

        // PUT: api/UserFeedback/5
        public void Put(int id, [FromBody]UserFeedback newUserFeedback)
        {
        }

        // DELETE: api/UserFeedback/5
        //public void Delete(int id)
        //{
        //}
    }
}