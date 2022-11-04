using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp.buisiness_logic.bl_structures;

namespace db_cp.UI.IView
{
    public interface IUsersListView
    {
        event Action FormLoaded;
        event Action<User> RoleChangedToModer;
        event Action<User> RoleChangedToUser;
        event Action RefreshForm;
        void SetUsers(List<User> users);
        void SetModer();
        void SetFromModer();
    }
}
