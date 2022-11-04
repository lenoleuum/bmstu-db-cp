using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using db_cp.data_access.EntityStructures;

namespace db_cp.data_access.PSQLRepository
{
    public class PSQLRepositoryUser : IRepositoryUser
    {
        private PSQLContext db;

        public PSQLRepositoryUser()
        {
            this.db = new PSQLContext();
        }
        public PSQLRepositoryUser(string ConString)
        {
            this.db = new PSQLContext(ConString);
        }
        public List<Users> GetAllUsers()
        {
            return this.db.Users.ToList();
        }

        public Users GetUserByLogin(string Login)
        {
            var res = this.db.Users.Where(u => u.Login == Login);

            return res.FirstOrDefault();

        }
        public void InsertUser(Users u)
        {
            this.db.Users.Add(u);
            this.db.SaveChanges();
        }

        public bool CheckUserLogin(string Login)
        {
            return this.db.Users.Any(u => u.Login == Login);
        }

        public int GetNewId()
        {
            try
            {
                return this.db.Users.Max(a => a.Id) + 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public void UpdatePassword(int Id, string Password)
        {
            var User = this.db.Users.Where(u => u.Id == Id);
            User.First().Password = Password;
            this.db.SaveChanges();
        }

        public void UpdateMail(int Id, string Mail)
        {
            var User = this.db.Users.Where(u => u.Id == Id);
            User.First().Mail = Mail;
            this.db.SaveChanges();
        }
        public void UpdateLogin(int Id, string Login)
        {
            var User = this.db.Users.Where(u => u.Id == Id);
            User.First().Login = Login;
            this.db.SaveChanges();
        }

        public void DeleteUser(Users u)
        {
            this.db.Remove(u);
            this.db.SaveChanges();
        }
        public void DeleteUser(int Id)
        {
            var u = this.db.Users.Find(Id);
            this.db.Remove(u);
            this.db.SaveChanges();
        }
        public void UpdateUser(Users user)
        {
            var u = this.db.Users.Find(user.Id);
            u.UserType = user.UserType;
            this.db.SaveChanges();
        }
    }
}
