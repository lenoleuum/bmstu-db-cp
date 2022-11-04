using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using NLog;

using db_cp.data_access;
using db_cp.buisiness_logic;
using db_cp.buisiness_logic.bl_structures;

using db_cp.UI.IView;
using db_cp.UI.Presenters;

namespace db_cp
{
    public partial class Register : Form, IRegisterView
    {
        private RegisterPresenter Presenter;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public event Action<User> UserRegistered;
        public Register()
        {
            InitializeComponent();

            this.Presenter = new RegisterPresenter(this, new Model());
        }
        public void SetRegistration()
        {
            MessageBox.Show("Вы успешно зарегистрированы!");
            logger.Info("пользователь зарегистрирован!" + " login: " + this.Login.Text);

            AuthorizeUser authorize = new AuthorizeUser();
            authorize.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.UName.Text == "" || this.Country.Text == "" || this.Mail.Text == ""
                || this.Login.Text == "" || this.Password.Text == "" || this.RetryPassword.Text == "")
            {
                MessageBox.Show("Необходимо заполнить все поля!");
            }
            else
            {
                if (!this.Presenter.CheckLogin(this.Login.Text))
                {
                    if (this.Password.Text == this.RetryPassword.Text)
                    {
                        try
                        {
                            User u = new User();
                            u.Id = 0;
                            u.Name = this.UName.Text;
                            u.Country = this.Country.Text;

                            if (Regex.IsMatch(this.Mail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                            {
                                u.Mail = this.Mail.Text;

                                u.DateOfBirth = this.DateOfBirth.Value;
                                u.Login = this.Login.Text;
                                u.Password = this.Password.Text;
                                u.UserType = 2;

                                this.UserRegistered(u);
                            }
                            else
                            {
                                MessageBox.Show("Некорректно задана почта!");
                                this.logger.Error("invalid mail!");
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                    }
                    else
                    {
                        this.logger.Error("passwords don't match!");
                        MessageBox.Show("Пароли не совпадают!");
                    }
                }
                else
                {
                    this.logger.Error("reserved login!");
                    MessageBox.Show("Данный логин уже занят!");
                }
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
