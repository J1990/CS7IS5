using Fitness.DataAccess;
using Fitness.DataObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.IO;
using System.Linq;

namespace Fitness.BusinessLogic
{
    public class UserFeedbackBL
    {
        UserFeedbackDA userFeedbackDA = null;
        RecipeDA recipeDA = null;

        public UserFeedbackBL()
        {
            userFeedbackDA = new UserFeedbackDA();
            recipeDA = new RecipeDA();
        }

        public List<UserFeedback> GetRecipeFeedbackByUser(int userId)
        {
            return userFeedbackDA.GetRecipeFeedbackByUser(userId);
        }

        public void RecordUserFeedback(UserFeedback userFeedback)
        {
            userFeedbackDA.RecordUserFeedback(userFeedback);

            //TrainMLRecommenderModel();
        }

        private void TrainMLRecommenderModel()
        {
            //var paramsForTraining = recipeDA.GetRecipeParamsForMLTraining();
            //string jsonParams = string.Empty;

            //using (StreamReader r = new StreamReader(@"D:\LocalIIS\TestWebApi\bin\train_chicken.json"))
            //{
            //    jsonParams = r.ReadToEnd();
            //}
            //var jsonParams = JsonConvert.SerializeObject(paramsForTraining);

            List<Model> modelList = new List<Model>();
            var lines = System.IO.File.ReadAllLines(@"D:\LocalIIS\TestWebApi\bin\train_chicken.csv");
            foreach (string line in lines.Skip(1))
            {
                var columns = line.Split(','); // or, populate YourClass 
                modelList.Add(new Model
                {
                    FeedbackType = columns[1],
                    Ingredients = columns[2],
                    IngredientsNER = columns[3],
                    Instructions = columns[4],
                    RatingStars = columns[5],
                    RecipeId = columns[6],
                    ReviewCount = columns[7],
                    UserId = columns[8],
                    cuisine = columns[9],
                });
            }
            string jsonParams = JsonConvert.SerializeObject(modelList);


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://e24a67b2.ngrok.io/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var buffer = System.Text.Encoding.UTF8.GetBytes(jsonParams);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseTask = client.PostAsync("/api/v1/train_model", byteContent);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                }
            }
        }
    }

    public class Model
    {
        public string RecipeId { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public string RatingStars { get; set; }

        public string ReviewCount { get; set; }

        public string IngredientsNER { get; set; }

        public string UserId { get; set; }

        public string cuisine { get; set; }

        public string FeedbackType { get; set; }

        public int user_response { get; set; }
    }
}
