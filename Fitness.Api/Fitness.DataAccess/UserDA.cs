using System.Collections.Generic;
using System.Configuration;
using Fitness.DataObjects;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Fitness.DataAccess
{
    public class UserDA
    {
        string connectionString = string.Empty;

        public UserDA()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        }

        public List<User> GetAllUsers()
        {
            List<User> allUsers = new List<User>();

            using(var dbConnection = new SqlConnection(connectionString))
            {
                using (var sqlAdapter = new SqlDataAdapter(DatabaseQueries.SELECT_ALL_USERS, dbConnection))
                {
                    var userTable = new DataTable();
                    sqlAdapter.Fill(userTable);

                    foreach(DataRow row in userTable.Rows)
                    {
                        var user = new User
                        {
                            Id = row.Field<int>("Id"),
                            UserName = row.Field<string>("UserName"),
                            Email = row.Field<string>("Email"),
                            ContactNumber = row.Field<string>("ContactNumber")
                        };
                        allUsers.Add(user);
                    }
                }
            }

            return allUsers;
        }

        public User GetUserById(int id)
        {
            using (var dbConnection = new SqlConnection(connectionString))
            {
                using (var sqlAdapter = new SqlDataAdapter(string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_USER_BY_ID, id), dbConnection))
                {
                    var userTable = new DataTable();
                    sqlAdapter.Fill(userTable);

                    if(userTable.Rows.Count > 0)
                    {
                        var user = new User
                        {
                            Id = userTable.Rows[0].Field<int>("Id"),
                            UserName = userTable.Rows[0].Field<string>("UserName"),
                            Email = userTable.Rows[0].Field<string>("Email"),
                            ContactNumber = userTable.Rows[0].Field<string>("ContactNumber")
                        };

                        return user;
                    }
                }
            }

            return new User();
        }

        public void AddNewUser(User user)
        {
            using (var dbConnection = new SqlConnection(connectionString))
            {
                var insertQuery = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.INSERT_NEW_USER, user.UserName, user.Email, user.ContactNumber);
                using(var command = new SqlCommand(insertQuery, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
