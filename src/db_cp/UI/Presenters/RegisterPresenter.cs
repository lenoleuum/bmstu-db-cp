using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.UI.IView;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI.Presenters
{
    public class RegisterPresenter
    {
        private IRegisterView view;
        private Model model;
        public RegisterPresenter(IRegisterView view, Model model)
        {
            this.view = view;
            this.model = model;

            this.view.UserRegistered += RegisterUser;
        }
        public bool CheckLogin(string Login)
        {
            return this.model.CheckLogin(Login);
        }
        public void RegisterUser(User user)
        {
            this.model.Register(user);
            this.view.SetRegistration();
        }
    }
}
