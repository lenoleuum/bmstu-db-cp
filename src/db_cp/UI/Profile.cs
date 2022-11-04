using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using db_cp.buisiness_logic.bl_structures;

namespace db_cp
{
    public partial class Profile : Form
    {
        private User user;
        private List<Track> tracks;
        private List<Playlist> playlists;
        public Profile(User user, List<Track> tracks, List<Playlist> playlists)
        {
            InitializeComponent();
            
            this.user = user;
            this.tracks = tracks;
            this.playlists = playlists;

            SetUp();
        }

        private void Profile_Load(object sender, EventArgs e)
        {

        }
        public void SetUp()
        {
            this.UMail.Text = this.user.Mail;
            this.ULogin.Text = this.user.Login;

            if (this.user.UserType == 2)
                this.UserRole.Text = "Пользователь";
            else if (this.user.UserType == 1)
                this.UserRole.Text = "Модератор";
            else if (this.user.UserType == 0)
                this.UserRole.Text = "Администратор";

            this.TrackNum.Text = this.tracks.Count().ToString();
            this.PlNum.Text = this.playlists.Count().ToString();
        }


        private void UMail_TextChanged(object sender, EventArgs e)
        {

        }

        private void OldPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void UMail_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
