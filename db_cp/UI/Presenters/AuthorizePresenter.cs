using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.UI.IView;

namespace db_cp.UI.Presenters
{
    public class AuthorizePresenter
    {
        private Model model;
        private IAuthorizeView view;

        public AuthorizePresenter(IAuthorizeView view, Model model)
        {
            this.model = model;
            this.view = view;
            this.view.Authorize += LogUser;
        }
        public void LogUser(string Login, string Password)
        {
            if (this.model.CheckUser(Login, Password))
                this.view.SetLoggedUser(true);
            else
                this.view.SetLoggedUser(false);
        }
    }
}
