using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_cp.UI.IView
{
    public interface IAuthorizeView
    {
        event Action<string, string> Authorize;
        void SetLoggedUser(bool res);
    }
}
