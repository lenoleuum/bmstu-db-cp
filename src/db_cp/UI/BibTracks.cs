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

namespace db_cp.UI
{
    public partial class BibTracks : Form, IBibTracksView
    {
        private BibTracksPresenter presenter;

        private Home home;

        private int flag_sorted_tr_name = 0;
        private int flag_sorted_tr_duration = 0;
        private int flag_sorted_tr_date_of_creation = 0;

        private List<Track> tracks = null;
        private User user;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public event Action FormLoaded;
        public event Action RefreshForm;
        public BibTracks(User u, Home home)
        {
            InitializeComponent();

            this.home = home;
            this.presenter = new BibTracksPresenter(this, new Model(u.Login, u.Password));
            this.user = u;
        }

        private void BibTracks_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn column01 = new DataGridViewTextBoxColumn();
            column01.Name = "track";
            column01.HeaderText = "Название трека";
            column01.Width = 201;
            column01.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.AddRange(column01);

            this.FormLoaded();

            this.SortTracks.Items.Add("По названию");
            this.SortTracks.Items.Add("По дате добавления");
            this.SortTracks.Items.Add("По длительности");

            this.SortTracks.SelectedIndex = 1;
        }
        public void SetTracks(List<Track> tracks)
        {
            foreach (Track t in tracks)
            {
                AddTrackToTable(t);
            }

            this.tracks = tracks;
        }
        public void AddTrackToTable(Track track)
        {
            this.dataGridView1.Rows.Add(track.Name);
        }
        public void RefreshInfo()
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();

            this.RefreshForm();
        }
        public void AddTrackModel(Track track)
        {
            this.logger.Info("трек " + track.Name + " добавлен в библиотеку треков пользователем ");
        }

        public void TrackNameChange(Track track)
        {
            this.home.ChangeTrackName(track);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddTrack addtrack = new AddTrack(this.user, this);
            addtrack.Show();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var s = this.dataGridView1.Rows[e.RowIndex].Cells[0];
                Track track = this.tracks.Find(a => a.Name == s.Value.ToString());

                TrackInfo track_info = new TrackInfo(this.user, track, this);
                track_info.Show();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
