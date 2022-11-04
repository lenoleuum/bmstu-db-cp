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

using db_cp.buisiness_logic.bl_structures;
using db_cp.UI.IView;
using db_cp.UI.Presenters;

namespace db_cp.UI
{
    public partial class UsersList : Form, IUsersListView
    {
        private UsersListPresenter presenter;

        private User selected;
        private List<User> users;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public event Action FormLoaded;
        public event Action RefreshForm;
        public event Action<User> RoleChangedToModer;
        public event Action<User> RoleChangedToUser;
        public UsersList(User u)
        {
            this.presenter = new UsersListPresenter(this, new Model(u));

            InitializeComponent();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn column01 = new DataGridViewTextBoxColumn();
            column01.Name = "id";
            column01.HeaderText = "ID";
            column01.Width = 50;
            column01.CellTemplate = new DataGridViewTextBoxCell();

            DataGridViewTextBoxColumn column02 = new DataGridViewTextBoxColumn();
            column02.Name = "login";
            column02.HeaderText = "Логин";
            column02.Width = 100;
            column02.CellTemplate = new DataGridViewTextBoxCell();

            DataGridViewTextBoxColumn column03 = new DataGridViewTextBoxColumn();
            column03.Name = "name";
            column03.HeaderText = "Имя";
            column03.Width = 100;
            column03.CellTemplate = new DataGridViewTextBoxCell();

            DataGridViewTextBoxColumn column04 = new DataGridViewTextBoxColumn();
            column04.Name = "role";
            column04.HeaderText = "Роль";
            column04.Width = 90;
            column04.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.AddRange(column01);
            dataGridView1.Columns.AddRange(column02);
            dataGridView1.Columns.AddRange(column03);
            dataGridView1.Columns.AddRange(column04);

            this.FormLoaded();
        }
        public void SetUsers(List<User> users)
        {
            foreach (User u in users)
            {
                string role = "administrator";

                if (u.UserType == 1)
                    role = "moderator";
                else if (u.UserType == 2)
                    role = "user";

                this.dataGridView1.Rows.Add(u.Id, u.Login, u.Name, role);
            }

            this.users = users;
        }
        
        public void SetModer()
        {
            try
            {
                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Refresh();

                this.RefreshForm();

                MessageBox.Show("Пользователь " + this.selected.Login + " сделан модератором!");

                logger.Info("Пользователь " + this.selected.Login + " сделан модератором!");
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
            }
        }
        public void SetFromModer()
        {
            try
            {
                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Refresh();

                this.RefreshForm();

                MessageBox.Show("Пользователь " + this.selected.Login + " больше не является модератором!");

                logger.Info("Пользователь " + this.selected.Login + " больше не является модератором!");
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.selected != null)
                {
                    if (this.selected.UserType == 1)
                        MessageBox.Show("Пользователь " + this.selected.Login + " уже является модератором!");
                    else
                        this.RoleChangedToModer(this.selected);
                }
                else
                    MessageBox.Show("Клините на нужного пользователя!");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var s = this.dataGridView1.Rows[e.RowIndex].Cells[0];
                this.selected = this.users.Find(a => a.Id.ToString() == s.Value.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.selected != null)
                {
                    if (this.selected.UserType != 1)
                        MessageBox.Show("Пользователь " + this.selected.Login + " не является модератором!");
                    else
                        this.RoleChangedToUser(this.selected);
                }
                else
                    MessageBox.Show("Клините на нужного пользователя!");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
