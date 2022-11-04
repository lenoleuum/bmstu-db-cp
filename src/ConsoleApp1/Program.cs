using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using db_cp;

namespace db_cp_console
{
    public class Program
    {
        static void Main(string[] args)
        {
            View view = new View();

            Presenter presenter = new Presenter(new Model("postgres", "1234"), view);

            view.StartMenu();
        }
    }
}
