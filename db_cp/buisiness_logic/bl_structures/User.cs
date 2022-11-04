using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_cp.buisiness_logic.bl_structures
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Mail { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }

        public User() { }

        public User(int _Id, string _Name, string _Country, string _Mail, 
            DateTime? _DateOfBirth, string _Login, string _Password, int UserType) 
        {
            this.Id = _Id;
            this.Name = _Name;
            this.Country = _Country;
            this.Mail = _Mail;
            this.DateOfBirth = _DateOfBirth;
            this.Login = _Login;
            this.Password = _Password;
            this.UserType = UserType;
        }
    }
}
