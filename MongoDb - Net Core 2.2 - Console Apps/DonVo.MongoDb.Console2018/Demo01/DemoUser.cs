using System;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.Demo01
{
    public class DemoUser
    {
        #region Connection and Creation of MongoDB
        UsersRepository _mongoDbRepo = new UsersRepository("mongodb://localhost:27017"); // GOOD
        //UsersRepository _mongoDbRepoFailed = new UsersRepository("mongodb://localhost:27016"); // FAIL

        public DemoUser(UsersRepository mongoDbRepo)
        {
            _mongoDbRepo = mongoDbRepo;
        }

        public async Task Initialize()
        {
            try
            {
                var user = new User()
                {
                    Name = "John Doe",
                    Age = 30,
                    Blog = "JohnBlog.net",
                    Location = "Atlanta, GA"
                };
                await _mongoDbRepo.InsertUser(user);

                user = new User()
                {
                    Name = "Jenifer Hook",
                    Age = 22,
                    Blog = "JeniferBlog.com",
                    Location = "Miami, FL"
                };
                await _mongoDbRepo.InsertUser(user);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }
        public void DeleteDb(string dbName)
        {
            Console.WriteLine("----- Drop Database 'blogs' -----");
            var isDelete = _mongoDbRepo.DeleteDatabase(dbName);
            Console.WriteLine("{0} is deleted {1}", dbName, isDelete ? "Sucessful" : "Unsuccessful");
            Console.WriteLine("\n");
        }
        public bool IsConnectionEstablished(UsersRepository mongoDbRepo)
        {
            var connected = mongoDbRepo.CheckConnection();
            return connected;
        }
        #endregion

        #region Read/Retrieve part
        public async Task GetAllUsers()
        {
            Console.WriteLine("----- Get All Users -----");
            var users = await _mongoDbRepo.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine("Id: {0} | Name: {1} | Age: {2} | Location: {3}", user.Id, user.Name, user.Age, user.Location);
            }
            Console.WriteLine("\n");
        }
        public async Task GetUserByField(string fieldName, string fieldValue)
        {
            Console.WriteLine("----- Get User By Field -----");
            var users = await _mongoDbRepo.GetUsersByField(fieldName, fieldValue);
            Console.WriteLine("{0} {1} ", fieldValue, users.Count > 0 ? "exists." : "does not exist.");
            Console.WriteLine("\n");
        }
        public async Task GetUserCount(int count)
        {
            Console.WriteLine("----- Get User Count -----");
            var users = await _mongoDbRepo.GetUsers(0, count);
            Console.WriteLine("Users Count: {0}", users.Count);
            Console.WriteLine("\n");
        }
        #endregion

        #region Insert part
        public async Task InsertUser()
        {
            Console.WriteLine("----- Insert User -----");

            var user = new User()
            {
                Name = "Susan Best",
                Age = 0,
                Blog = "SusanBlog.org",
                Location = "Seatle, WA"
            };

            var users = await _mongoDbRepo.GetAllUsers();
            var countBeforeInsert = users.Count;

            await _mongoDbRepo.InsertUser(user);

            users = await _mongoDbRepo.GetAllUsers();

            Console.WriteLine("Before Insert: {0} users and then After Insert: {1} users.", countBeforeInsert, users.Count);
            Console.WriteLine("\n");
        }
        #endregion

        #region Delete part
        public async Task DeleteUserById(MongoDB.Bson.ObjectId objectID)
        {
            var result = await _mongoDbRepo.DeleteUserById(objectID);
            Console.WriteLine("Delete is {0}", result ? "Success" : "Failed");
            Console.WriteLine("\n");
        }
        public async Task DeleteAllUsers()
        {
            Console.WriteLine("----- Delete All Users -----");
            var result = await _mongoDbRepo.DeleteAllUsers();

            Console.WriteLine("All users have been deleted.");
            Console.WriteLine("\n");
        }
        #endregion

        #region Update part
        public async Task UpdateFirstUserByField(string fieldName, string fieldValue)
        {
            Console.WriteLine("----- Update First User By Field -----");

            var users = await _mongoDbRepo.GetUsersByField(fieldName, fieldValue);
            var user = users.FirstOrDefault();

            await _mongoDbRepo.UpdateUser(user.Id, "blog", "Rubik's Code");

            users = await _mongoDbRepo.GetUsersByField(fieldName, fieldValue);
            user = users.FirstOrDefault();

            Console.WriteLine("User Blog = '{0}' has been updated.", user.Blog);
            Console.WriteLine("\n");
        }
        public async Task UpdateUserById(MongoDB.Bson.ObjectId objectId)
        {
            bool isUpdate = await _mongoDbRepo.UpdateUser(objectId, "Age", "100");

            var user = isUpdate ? await _mongoDbRepo.GetUserById(objectId) : null;

            Console.WriteLine("Age is updated to {0}", isUpdate ? user.Age.ToString() : "nothing because it's failed.");
            Console.WriteLine("\n");
        }
        public async Task UpdateUserExtendingWithNewField()
        {
            Console.WriteLine("----- Update User Extending With New Field -----");

            var users = await _mongoDbRepo.GetUsersByField("name", "John Doe");
            var user = users.FirstOrDefault();

            var result = await _mongoDbRepo.UpdateUser(user.Id, "address", "test address");

            Console.WriteLine("userId: {0} with {1}", user.Id, result ? "A new field 'address' has been created." : "fail to update");
            Console.WriteLine("\n");
        }
        #endregion

    }
}
