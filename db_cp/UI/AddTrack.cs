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
using db_cp.UI.Presenters;
using db_cp.UI.IView;

namespace db_cp
{
    public partial class AddTrack : Form, IAddTrackView
    {
        private AddTrackPresenter presenter;

        private Logger logger = LogManager.GetCurrentClassLogger();

        private BibTracks bibtrack;

        private User user;
        private Track t;

        public event Action<Track> TrackAddSelected;
        public AddTrack(User u, BibTracks bibtrack)
        {
            InitializeComponent();

            this.user = u;
            this.presenter = new AddTrackPresenter(this, new Model(u));
            this.bibtrack = bibtrack;
            this.t = new Track();
        }

        private void AddMidiFile_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.AddMidiFile.ShowDialog() == DialogResult.Cancel)
                    return;

                string filename = this.AddMidiFile.FileName;

                TrackFile.Text = filename;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
            }
        }

        private void AddTrack_Load(object sender, EventArgs e)
        {

        }
        public void SetAddTrack()
        {
            this.bibtrack.AddTrackToTable(this.t);
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TrackName.Text != "" && this.AddMidiFile.FileName != "")
                {
                    DateTime dt = this.TrackDuration.Value;
                    User cur_user = this.user;
                    t.Id = 0;
                    t.AuthorId = cur_user.Id;
                    t.Name = this.TrackName.Text;
                    t.Duration = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
                    t.DateOfCreation = DateTime.Today;
                    t.Description = this.TrackDescription.Text;
                    t.MidiFile = this.AddMidiFile.FileName;

                    this.TrackAddSelected(t);

                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
