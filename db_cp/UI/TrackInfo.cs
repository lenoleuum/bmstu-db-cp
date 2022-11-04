using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NLog;

using db_cp.buisiness_logic.bl_structures;
using db_cp.UI;
using db_cp.UI.Presenters;
using db_cp.UI.IView;

namespace db_cp
{
    public partial class TrackInfo : Form, ITrackInfoView
    {
        private BibTracks bibtracks;
        private Home home;
        private PlaylistInfo playlist_info;

        private int flag = 0;

        private Track track = null;
        private Playlist p = null;
        private User user = null;

        private TrackInfoPresenter Presenter;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public event Action<Track> TrackChanged;
        public event Action<Track> TrackDeletedFromUser;
        public event Action<Track, Playlist> TrackDeletedFromPlaylist;
        public event Action<Track> TrackDeletedFromBib;
        public TrackInfo(Track track, User user, Home home)
        {
            InitializeComponent();
            this.track = track;
            this.user = user;
            this.home = home;

            this.Presenter = new TrackInfoPresenter(this, new Model(user.Login, user.Password));

            flag = 0;
        }
        public TrackInfo(User u, Track track, Playlist p, PlaylistInfo playlist_info)
        {
            InitializeComponent();

            this.user = u;
            this.Presenter = new TrackInfoPresenter(this, new Model(this.user.Login, this.user.Password));
            this.playlist_info = playlist_info;

            flag = 2;

            this.track = track;
            this.p = p;
        }
        public TrackInfo(User u, Track track, BibTracks bibtracks)
        {
            InitializeComponent();

            this.Presenter = new TrackInfoPresenter(this, new Model(u.Login, u.Password));

            this.bibtracks = bibtracks;
            this.track = track;
            this.user = u;

            flag = 1;
        }

        private void TrackInfo_Load(object sender, EventArgs e)
        {
            this.TrackName.Text = this.track.Name;
            this.TrackDescription.Text = this.track.Description;
            this.TrackDuration.Text = this.track.Duration.ToString();
            DateTime dt = this.track.DateOfCreation.Value;
            this.TrackDate.Text = dt.ToShortDateString();

            if (this.user.UserType != 2 && this.bibtracks != null)
            {
                this.TrackName.ReadOnly = false;
                this.TrackDescription.ReadOnly = false;
            }
            else
                this.SaveChanges.Visible = false;
        }

        private void Download_Click(object sender, EventArgs e)
        {
          
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Download_Click_1(object sender, EventArgs e)
        {
            try
            {
                string filename = this.track.MidiFile;

                this.folderBrowserDialog1.ShowDialog();

                string folder = this.folderBrowserDialog1.SelectedPath;

                string path = folder + "\\" + this.track.Name + ".mid";

                using (FileStream fs_to = File.Create(path))
                {
                    using (FileStream fs_from = File.OpenRead(filename))
                    {
                        fs_from.CopyTo(fs_to);
                        fs_to.Close();
                        fs_from.Close();
                    }
                }

                MessageBox.Show("Трек успешно сохранен в выбранном каталоге!\n" + path);
                this.logger.Info("трек " + this.track.Name + "скачан пользователем " + this.user.Login);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
            }
        }

        private void TrackName_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.logger.Info("имя трека изменено с " + this.track.Name + " на " + this.TrackName.Text);

            this.track.Name = this.TrackName.Text;
            this.track.Description = this.TrackDescription.Text;
            
            this.TrackChanged(this.track);
        }
        public void SetChanges()
        {
            MessageBox.Show("Изменения сохранены!");
        }
        public void SetDeleteTrackFromUser()
        {
            try
            {
                MessageBox.Show("Трек успешно удален из вашей медиатеки!");
                this.logger.Info("трек " + this.track.Name + "удален из медиатеки пользователя " + this.user.Login);
                this.home.RefreshInfo();
                this.Hide();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
            }
        }
        public void SetDeleteTrackFromPlaylist()
        {
            MessageBox.Show("Трек успешно удален из плейлиста!");
            this.logger.Info("трек " + this.track.Name + " удален из плейлиста " + this.p.Name);
            this.playlist_info.RefreshInfo();
            this.Hide();
        }
        public void SetDeleteTrack()
        {
            MessageBox.Show("Трек успешно удален из библиотеки!");
            this.logger.Info("трек " + this.track.Name + "удален из библиотеки треков");
            this.bibtracks.RefreshInfo();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (flag == 2)
            {
                var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить этот трек из плейлиста?\nЭто действие нельзя будет отменить.",
                                         "Подтвердите действие.",
                                         MessageBoxButtons.YesNo);

                if (confirmResult.ToString() == "Yes")
                {
                    this.TrackDeletedFromPlaylist(this.track, this.p);
                }
            }
            else
            {
                if (flag == 1)
                {
                    var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить этот трек из библиотеки?\nОн будет также удален из медиотек всех пользователей.",
                                         "Подтвердите действие.",
                                         MessageBoxButtons.YesNo);

                    if (confirmResult.ToString() == "Yes")
                    {
                        this.TrackDeletedFromBib(this.track);
                        
                    }
                }
                else
                {
                    var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить этот трек из своей медиатеки?\nОн будет также удален из всех плейлистов.",
                                     "Подтвердите действие",
                                     MessageBoxButtons.YesNo);

                    if (confirmResult.ToString() == "Yes")
                    {
                        this.TrackDeletedFromUser(this.track);
                    }
                }
            }
        }
    }
}
