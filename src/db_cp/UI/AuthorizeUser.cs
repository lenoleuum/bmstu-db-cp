using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

using db_cp.buisiness_logic;
using db_cp.UI.Presenters;
using db_cp.UI.IView;
using db_cp.UI;

namespace db_cp
{
    public partial class AuthorizeUser : Form, IAuthorizeView
    {
        private AuthorizePresenter presenter;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public event Action<string, string> Authorize;

        public AuthorizeUser()
        {
            InitializeComponent();

            this.presenter = new AuthorizePresenter(this, new Model());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void SetLoggedUser(bool res)
        {
            if (res)
            {
                Home home = new Home(this.Login.Text, this.Password.Text);
                home.Show();
                this.Hide();
            }
            else
            {
                logger.Error("invalid login or password during authorization!");
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void AuthorizeButton_Click(object sender, EventArgs e)
        {
            string Login = this.Login.Text;
            string Password = this.Password.Text;

            this.Authorize(Login, Password);
        }
    }
}
