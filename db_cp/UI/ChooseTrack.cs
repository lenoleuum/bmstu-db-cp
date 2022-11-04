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

using db_cp.UI.IView;
using db_cp.UI.Presenters;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp
{
    public partial class ChooseTrack : Form, IChooseTrackView
    {
        private ChooseTrackPresenter presenter;

        private CreatePlaylist create_playlist;
        private PlaylistInfo playlist_info;
        private Home home;

        private int flag_sorted_tr_name = 0;
        private int flag_sorted_tr_duration = 0;
        private int flag_sorted_tr_date_of_creation = 0;

        private Logger logger = LogManager.GetCurrentClassLogger();

        private int flag;

        private List<Track> tracks;

        public event Action FormLoadedTracks;
        public event Action FormLoadedUserTracks;
        public event Action<Track> TrackSelected;
        public ChooseTrack(User u, Home home)
        {
            InitializeComponent();

            this.presenter = new ChooseTrackPresenter(this, new Model(u));
            this.home = home;

            flag = 1;
        }
        public ChooseTrack(string Login, string Password, CreatePlaylist create_playlist)
        {
            InitializeComponent();

            this.presenter = new ChooseTrackPresenter(this, new Model(Login, Password));
            this.create_playlist = create_playlist;

            flag = 2;
        }

        public ChooseTrack(string Login, string Password, PlaylistInfo playlist_info)
        {
            InitializeComponent();

            this.presenter = new ChooseTrackPresenter(this, new Model(Login, Password));
            this.playlist_info = playlist_info;

            flag = 3;
        }

        private void ChooseTrack_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn column01 = new DataGridViewTextBoxColumn();
            column01.Name = "track";
            column01.HeaderText = "Название трека";
            column01.Width = 201;
            column01.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.AddRange(column01);

            if (flag == 1)
                this.FormLoadedTracks();
            else
                this.FormLoadedUserTracks();

            this.SortTracks.Items.Add("По названию");
            this.SortTracks.Items.Add("По дате добавления");
            this.SortTracks.Items.Add("По длительности");

            this.SortTracks.SelectedIndex = 1;
        }
        public void SetTracks(List<Track> tracks)
        {
            foreach (Track t in tracks)
            {
                dataGridView1.Rows.Add(t.Name);
            }

            this.tracks = tracks;
        }
        public void SetAddTrackToUser()
        {
            this.home.RefreshInfo();
            this.Hide();
        }
        public void SetAddTrackToUserFail()
        {
            MessageBox.Show("Данный трек уже добавлен в вашу медиатеку!");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var s = this.dataGridView1.Rows[e.RowIndex].Cells[0];
                Track track = this.tracks.Find(a => a.Name == s.Value.ToString());

                if (this.flag == 3)
                    this.playlist_info.AddTrack(track);
                else
                {
                    if (this.flag == 2)
                        this.create_playlist.AddTrackToTable(track);
                    else
                    {
                        this.TrackSelected(track);
                    }
                }

                this.Hide();
            }
            catch (Exception ex)
            {
                this.logger.Info(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var res = this.SortTracks.SelectedItem.ToString();

            switch (res)
            {
                case "По названию":
                    if (this.flag_sorted_tr_name == 0)
                    {
                        this.dataGridView1.Sort(new RowComparerName(SortOrder.Ascending));
                        this.flag_sorted_tr_name = 1;
                    }
                    else
                    {
                        this.dataGridView1.Sort(new RowComparerName(SortOrder.Descending));
                        this.flag_sorted_tr_name = 0;
                    }

                    break;

                case "По дате добавления":
                    if (this.flag_sorted_tr_date_of_creation == 0)
                    {
                        this.dataGridView1.Sort(new RowComparerDateOfCreation(SortOrder.Ascending, this.tracks));
                        this.flag_sorted_tr_date_of_creation = 1;
                    }
                    else
                    {
                        this.dataGridView1.Sort(new RowComparerDateOfCreation(SortOrder.Descending, this.tracks));
                        this.flag_sorted_tr_date_of_creation = 0;
                    }

                    break;

                case "По длительности":
                    if (this.flag_sorted_tr_duration == 0)
                    {
                        this.dataGridView1.Sort(new RowComparerDuration(SortOrder.Ascending, this.tracks));
                        this.flag_sorted_tr_duration = 1;
                    }
                    else
                    {
                        this.dataGridView1.Sort(new RowComparerDuration(SortOrder.Descending, this.tracks));
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
    }
}
