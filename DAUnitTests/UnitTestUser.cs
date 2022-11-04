using Xunit;
using System;
using db_cp;
using System.Linq;

using db_cp.data_access.PSQLRepository;
using db_cp.data_access.EntityStructures;
using db_cp.data_access;

namespace DAUnitTests
{
    public class UnitTestUser
    {
        PSQLRepositoryUser rep_user = new PSQLRepositoryUser();
        private void CheckEqual(Users u1, Users u2)
        {
            Assert.Equal(u1.Name, u2.Name);
            Assert.Equal(u1.Id, u2.Id);
            Assert.Equal(u1.Country, u2.Country);
            Assert.Equal(u1.Login, u2.Login);
            Assert.Equal(u1.Password, u2.Password);
            Assert.Equal(u1.DateOfBirth, u2.DateOfBirth);
            Assert.Equal(u1.Mail, u2.Mail);
        }

        [Fact]
        public void TestInsertUser()
        {
            Users users = new Users();
            users.Name = "Test";
            users.Id = rep_user.GetNewId();
            users.Country = "Russia";
            users.Login = "test";
            users.Password = "test1234";
            users.Mail = "test@test.ru";
            users.DateOfBirth = DateTime.Today;

            rep_user.InsertUser(users);

            using (PSQLContext db = new PSQLContext())
            {
                var u = db.Users.Find(users.Id);

                CheckEqual(users, u);
            }
        }

        [Theory]
        [InlineData("moder")]

        public void TestGetUserInfo(string Login)
        {
            Users user = rep_user.GetUserByLogin(Login);

            using (PSQLContext db = new PSQLContext())
            {
                var u = db.Users.Find(user.Id);

                CheckEqual(user, u);
            }
        }

        [Fact]
        public void TestDeleteUser()
        {
            using (PSQLContext db = new PSQLContext())
            {
                int Id = db.Users.Count();
                rep_user.DeleteUser(Id);

                if (db.Users.Find(Id) != null)
                    throw new Exception("Problem with delete_user!");
            }
        }

        [Theory]
        [InlineData("1234")]
        public void TestUpdatePassword(string Password)
        {
            int Id = 1;

            rep_user.UpdatePassword(Id, Password);

            using (PSQLContext db = new PSQLContext())
            {
                var u = db.Users.Find(Id);

                Assert.Equal(Password, u.Password);
            }
        }
    }
}