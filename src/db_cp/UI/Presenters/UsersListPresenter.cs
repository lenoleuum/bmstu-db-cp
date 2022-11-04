using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;
using db_cp.UI.IView;

namespace db_cp.UI.Presenters
{
    public class UsersListPresenter
    {
        private Model model;
        private IUsersListView view;
        public UsersListPresenter(IUsersListView view, Model model)
        {
            this.view = view;
            this.model = model;

            this.view.FormLoaded += LoadUsers;
            this.view.RoleChangedToModer += ChangeUserRoleToModer;
            this.view.RoleChangedToUser += ChangeUserRoleFromModer;
            this.view.RefreshForm += LoadUsers;
        }
        public void LoadUsers()
        {
            var users = this.model.GetAllUsers();
            this.view.SetUsers(users);
        }
        public void ChangeUserRoleToModer(User u)
        {
            this.model.ChangeUserRoleToModer(u);
            this.view.SetModer();
        }
        public void ChangeUserRoleFromModer(User u)
        {
            this.model.ChangeUserRoleFromModer(u);
            this.view.SetFromModer();
        }
    }
}
