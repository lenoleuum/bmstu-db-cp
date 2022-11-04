using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

using db_cp.data_access;
using db_cp.data_access.PSQLRepository;
using db_cp.data_access.MSSQLRepository;

namespace db_cp.Util
{
    public class IOC
    {
        public IKernel ninjectKernel;
        public IOC()
        {
            ninjectKernel = new StandardKernel();

            string con = System.Configuration.ConfigurationManager.AppSettings["DBMS"];

            if (con == "PSQL")
                PSQL();
            else
                if (con == "MSSQL") 
                    MSSQL();
        }
        public IOC(string Login, string Password)
        {
            ninjectKernel = new StandardKernel();

            string con = System.Configuration.ConfigurationManager.AppSettings["DBMS"];

            if (con == "PSQL")
                PSQLConString(Login, Password);
            else
                if (con == "MSSQL")
                MSSQLConString(Login, Password);
        }

        private void PSQL()
        {
            ninjectKernel.Bind<IRepositoryUser>().To<PSQLRepositoryUser>();
            ninjectKernel.Bind<IRepositoryTrack>().To<PSQLRepositoryTrack>();
            ninjectKernel.Bind<IRepositoryPlaylist>().To<PSQLRepositoryPlaylist>();
        }
        private void PSQLConString(string Login, string Password)
        {
            string ConString = "Host=localhost;Port=5432;Database=db_cp;Username=" + Login + ";Password=" + Password;

            ninjectKernel.Bind<IRepositoryUser>().To<PSQLRepositoryUser>().WithConstructorArgument(ConString);
            ninjectKernel.Bind<IRepositoryTrack>().To<PSQLRepositoryTrack>().WithConstructorArgument(ConString);
            ninjectKernel.Bind<IRepositoryPlaylist>().To<PSQLRepositoryPlaylist>().WithConstructorArgument(ConString);
        }  
        private void MSSQL()
        {
            ninjectKernel.Bind<IRepositoryUser>().To<MSSQLRepositoryUser>();
            ninjectKernel.Bind<IRepositoryTrack>().To<MSSQLRepositoryTrack>();
            ninjectKernel.Bind<IRepositoryPlaylist>().To<MSSQLRepositoryPlaylist>();
        }
        private void MSSQLConString(string Login, string Password)
        {
            string ConString = "Server=DESKTOP-2LBKKJG;Database=db_cp;Persist Security Info=False;" +
                "TrustServerCertificate=true;User Id=" + Login + ";Password=" + Password + ";";

            ninjectKernel.Bind<IRepositoryUser>().To<MSSQLRepositoryUser>().WithConstructorArgument(ConString);
            ninjectKernel.Bind<IRepositoryTrack>().To<MSSQLRepositoryTrack>().WithConstructorArgument(ConString);
            ninjectKernel.Bind<IRepositoryPlaylist>().To<MSSQLRepositoryPlaylist>().WithConstructorArgument(ConString);
        }
    }
}
