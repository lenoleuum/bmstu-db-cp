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
using db_cp.UI;

namespace db_cp
{
    public partial class Home : Form, IHomeView
    {
        public event Action FormLoaded;
        public event Action RefreshForm;

        private HomePresenter Presenter;

        private User User;
        private List<Track> Tracks;
        private List<Playlist> Playlists;

        private int flag_sorted_tr_name = 0;
        private int flag_sorted_tr_duration = 0;
        private int flag_sorted_tr_date_of_creation = 0;

        private int flag_sorted_pl_name = 0;
        private int flag_sorted_pl_date_of_creation = 0;

        private Logger logger = LogManager.GetCurrentClassLogger();
        public Home(string Login, string Password)
        {
            InitializeComponent();

            this.Presenter = new HomePresenter(this, new Model(Login, Password));
        }
        public Home(Model model)
        {
            InitializeComponent();

            this.Presenter = new HomePresenter(this, new Model(model.GetUserInfo()));
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void Home_Load(object sender, EventArgs e)
        {
            this.FormLoaded();

            this.SortTracks.Items.Add("По названию");
            this.SortTracks.Items.Add("По дате добавления");
            this.SortTracks.Items.Add("По длительности");

            this.SortTracks.SelectedIndex = 1;

            this.SortPlaylists.Items.Add("По названию");
            this.SortPlaylists.Items.Add("По дате добавления");

            this.SortPlaylists.SelectedIndex = 0;
        }
        public void SetTracks(List<Track> tracks)
        {
            DataGridViewTextBoxColumn column01 = new DataGridViewTextBoxColumn();
            column01.Name = "track";
            column01.HeaderText = "Название трека";
            column01.Width = 201;
            column01.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView2.Columns.AddRange(column01);

            foreach (Track t in tracks)
            {
                AddTrackToTable(t);
            }

            this.Tracks = tracks;
        }
        public void SetPlaylists(List<Playlist> playlists)
        {
            DataGridViewTextBoxColumn column02 = new DataGridViewTextBoxColumn();
            column02.Name = "playlist";
            column02.HeaderText = "Название плейлиста";
            column02.Width = 201;
            column02.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.AddRange(column02);

            foreach (Playlist p in playlists)
            {
                AddPlaylistToTable(p);
            }

            this.Playlists = playlists;
        }
        public void SetUserInfo(User user)
        {
            logger.Info("пользователь авторизован!" + " id: " + user.Id + " login: " + user.Login);

            this.UName.Text = user.Name + "!";

            if (user.UserType == 2)
                this.BibTrack.Visible = false;

            if (user.UserType != 0)
                this.Users.Visible = false;

            this.User = user;
        }
        public void AddPlaylistToTable(Playlist playlist)
        {
            this.dataGridView1.Rows.Add(playlist.Name);
        }
        public void AddTrackToTable(Track track)
        {
            this.dataGridView2.Rows.Add(track.Name);
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var s = this.dataGridView2.Rows[e.RowIndex].Cells[0];
                Track t = this.Tracks.Find(a => a.Name == s.Value.ToString());

                TrackInfo track_info = new TrackInfo(t, this.User, this);
                track_info.Show();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
            }
        }
        public void RefreshInfo()
        {
            this.dataGridView2.Rows.Clear();
            this.dataGridView2.Refresh();

            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();

            this.RefreshForm();
        }
        private void AddTrack_Click(object sender, EventArgs e)
        {
            ChooseTrack choosetrack = new ChooseTrack(this.User, this);
            choosetrack.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.logger.Info("конец сеанса: id - " + this.User.Id + " login - " + this.User.Login);
            MessageBox.Show("Всего хорошего! Возвращайтесь!");
            this.Close();

            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var s = this.dataGridView1.Rows[e.RowIndex].Cells[0];
                Playlist p = this.Playlists.Find(a => a.Name == s.Value.ToString());

                PlaylistInfo playlist_info = new PlaylistInfo(this.User, p, this);
                playlist_info.Show();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreatePlaylist create_playlist = new CreatePlaylist(this.User, this);
            create_playlist.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var res = this.SortTracks.SelectedItem.ToString();

            switch (res)
            {
                case "По названию":
                    if (this.flag_sorted_tr_name == 0)
                    {
                        this.dataGridView2.Sort(new RowComparerName(SortOrder.Ascending));
                        this.flag_sorted_tr_name = 1;
                    }
                    else
                    {
                        this.dataGridView2.Sort(new RowComparerName(SortOrder.Descending));
                        this.flag_sorted_tr_name = 0;
                    }

                    break;

                case "По дате добавления":
                    if (this.flag_sorted_tr_date_of_creation == 0)
                    {
                        this.dataGridView2.Sort(new RowComparerDateOfCreation(SortOrder.Ascending, this.Tracks));
                        this.flag_sorted_tr_date_of_creation = 1;
                    }
                    else
                    {
                        this.dataGridView2.Sort(new RowComparerDateOfCreation(SortOrder.Descending, this.Tracks));
                        this.flag_sorted_tr_date_of_creation = 0;
                    }

                    break;

                case "По длительности":
                    if (this.flag_sorted_tr_duration == 0)
                    {
                        this.dataGridView2.Sort(new RowComparerDuration(SortOrder.Ascending, this.Tracks));
                        this.flag_sorted_tr_duration = 1;
                    }
                    else
                    {
                        this.dataGridView2.Sort(new RowComparerDuration(SortOrder.Descending, this.Tracks));
                        this.flag_sorted_tr_duration = 0;
                    }

                    break;
            }
        }

        private class RowComparerName : System.Collections.IComparer
        {
            private static int sortOrderModifier = 1;
            public RowComparerName(SortOrder sortOrder)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

                int CompareResult = System.String.Compare(
                    DataGridViewRow1.Cells[0].Value.ToString(),
                    DataGridViewRow2.Cells[0].Value.ToString());

                return CompareResult * sortOrderModifier;
            }
        }

        private class RowComparerDateOfCreation : System.Collections.IComparer
        {
            private static int sortOrderModifier = 1;
            private List<Track> tracks;
            public RowComparerDateOfCreation(SortOrder sortOrder, List<Track> tracks)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }

                this.tracks = tracks;   
            }
            private Track get_track_by_name(string Name)
            {
                foreach (Track t in this.tracks)
                {
                    if (t.Name == Name)
                        return t;
                }

                return null;
            }
            public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

                int CompareResult = System.DateTime.Compare(
                    get_track_by_name(DataGridViewRow1.Cells[0].Value.ToString()).DateOfCreation.Value,
                    get_track_by_name(DataGridViewRow2.Cells[0].Value.ToString()).DateOfCreation.Value);

                return CompareResult * sortOrderModifier;
            }
        }

        private class RowComparerDuration : System.Collections.IComparer
        {
            private static int sortOrderModifier = 1;
            private List<Track> tracks;
            public RowComparerDuration(SortOrder sortOrder, List<Track> tracks)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }

                this.tracks = tracks;
            }
            private Track get_track_by_name(string Name)
            {
                foreach (Track t in this.tracks)
                {
                    if (t.Name == Name)
                        return t;
                }

                return null;
            }
            public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

                int CompareResult = System.TimeSpan.Compare(
                    get_track_by_name(DataGridViewRow1.Cells[0].Value.ToString()).Duration.Value,
                    get_track_by_name(DataGridViewRow2.Cells[0].Value.ToString()).Duration.Value);

                return CompareResult * sortOrderModifier;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Profile profile = new Profile(this.User, this.Tracks, this.Playlists);
            profile.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var res = this.SortPlaylists.SelectedItem.ToString();

            switch (res)
            {
                case "По названию":
                    if (this.flag_sorted_pl_name == 0)
                    {
                        this.dataGridView1.Sort(new RowComparerName(SortOrder.Ascending));
                        this.flag_sorted_pl_name = 1;
                    }
                    else
                    {
                        this.dataGridView1.Sort(new RowComparerName(SortOrder.Descending));
                        this.flag_sorted_pl_name = 0;
                    }

                    break;
                    
                case "По дате добавления":
                    if (this.flag_sorted_pl_date_of_creation == 0)
                    {
                        this.dataGridView1.Sort(new RowComparerDateOfCreationPl(SortOrder.Ascending, this.Playlists));
                        this.flag_sorted_pl_date_of_creation = 1;
                    }
                    else
                    {
                        this.dataGridView1.Sort(new RowComparerDateOfCreationPl(SortOrder.Descending, this.Playlists));
                        this.flag_sorted_pl_date_of_creation = 0;
                    }

                    break;
            }
        }

        private class RowComparerDateOfCreationPl : System.Collections.IComparer
        {
            private static int sortOrderModifier = 1;
            private List<Playlist> pl;
            public RowComparerDateOfCreationPl(SortOrder sortOrder, List<Playlist> pl)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }

                this.pl = pl;
            }
            private Playlist get_playlist_by_name(string Name)
            {
                foreach (Playlist p in this.pl)
                {
                    if (p.Name == Name)
                        return p;
                }

                return null;
            }
            public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

                int CompareResult = System.DateTime.Compare(
                    (DateTime)get_playlist_by_name(DataGridViewRow1.Cells[0].Value.ToString()).DateOfCreation,
                    get_playlist_by_name(DataGridViewRow2.Cells[0].Value.ToString()).DateOfCreation.Value);

                return CompareResult * sortOrderModifier;
            }
        }

        private void SortTracks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            BibTracks bibtracks = new BibTracks(this.User, this);
            bibtracks.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            UsersList users = new UsersList(this.User);
            users.Show();
        }
        public void ChangeTrackName(Track track)
        {
            //this.model = this.presenter.ChangeTrackName(track);
        }
        public void ChangeTracDescription(Track track)
        {
            // todo
            //this.model = this.presenter.ChangeTrackName(track);
        }
    }
    
}
