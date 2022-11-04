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
    public partial class PlaylistInfo : Form, IPlaylistInfoView
    {
        private PlaylistInfoPresenter presenter;

        private Home home;

        private int flag_sorted_tr_name = 0;
        private int flag_sorted_tr_duration = 0;
        private int flag_sorted_tr_date_of_creation = 0;

        private Playlist p = null;
        private User u;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public event Action<Playlist> FormLoaded;
        public event Action<Track, Playlist> TrackAdded;
        public event Action<Playlist> DeletePlaylistSelected;
        public PlaylistInfo(User u, Playlist p, Home home)
        {
            InitializeComponent();

            this.presenter = new PlaylistInfoPresenter(this, new Model(u.Login, u.Password));
            this.u = u;
            this.p = p;

            this.home = home;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var s = this.dataGridView1.Rows[e.RowIndex].Cells[0];
            Track t = this.p.tracks.Find(a => a.Name == s.Value.ToString());

            TrackInfo track_info = new TrackInfo(this.u, t, this.p, this);
            track_info.Show();
        }

        private void PlaylistInfo_Load(object sender, EventArgs e)
        {
            this.PlName.Text = p.Name;
            this.Description.Text = p.Description;

            DataGridViewTextBoxColumn column01 = new DataGridViewTextBoxColumn();
            column01.Name = "track";
            column01.HeaderText = "Название трека";
            column01.Width = 201;
            column01.CellTemplate = new DataGridViewTextBoxCell();

            this.dataGridView1.Columns.AddRange(column01);

            this.FormLoaded(this.p);

            this.SortTracks.Items.Add("По названию");
            this.SortTracks.Items.Add("По дате добавления");
            this.SortTracks.Items.Add("По длительности");

            this.SortTracks.SelectedIndex = 1;
        }

        public void SetTracks(List<Track> tracks)
        {
            if (tracks != null)
            {
                foreach (Track t in tracks)
                {
                    AddTrackToTable(t);
                }
            }

            this.p.tracks = tracks;
        }
        public void AddTrackToTable(Track track)
        {
            this.dataGridView1.Rows.Add(track.Name);
        }
        public void RefreshInfo()
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();

            this.FormLoaded(this.p);
        }
        public void SetAddTrackToPlaylist(Track track)
        {
            AddTrackToTable(track);

            this.logger.Info("трек " + track.Name + " добавлен в плейлист " + this.p.Name +
                " пользователем " + this.u.Login);

            this.RefreshInfo();
        }
        public void AddTrack(Track track)
        {
            if (this.p.tracks.Find(a => a.Name == track.Name) == null)
            {
                this.TrackAdded(track, this.p);
            }
            else
                MessageBox.Show("Данный трек уже добавлен в этот плейлист!");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ChooseTrack choose_track = new ChooseTrack(this.u.Login, this.u.Password, this);
            choose_track.Show();
        }

        private void button2_Click(object sender, EventArgs e)
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
                        this.dataGridView1.Sort(new RowComparerDateOfCreation(SortOrder.Ascending, this.p.tracks));
                        this.flag_sorted_tr_date_of_creation = 1;
                    }
                    else
                    {
                        this.dataGridView1.Sort(new RowComparerDateOfCreation(SortOrder.Descending, this.p.tracks));
                        this.flag_sorted_tr_date_of_creation = 0;
                    }

                    break;

                case "По длительности":
                    if (this.flag_sorted_tr_duration == 0)
                    {
                        this.dataGridView1.Sort(new RowComparerDuration(SortOrder.Ascending, this.p.tracks));
                        this.flag_sorted_tr_duration = 1;
                    }
                    else
                    {
                        this.dataGridView1.Sort(new RowComparerDuration(SortOrder.Descending, this.p.tracks));
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

        private void button3_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить этот плейлист?\nЭто действие нельзя будет отменить.",
                                         "Подтвердите действие.",
                                         MessageBoxButtons.YesNo);

            if (confirmResult.ToString() == "Yes")
            {
                this.DeletePlaylistSelected(this.p);
            }

            this.home.RefreshInfo();
            this.Hide();
        }
    }
}
