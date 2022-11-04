using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using db_cp.UI.Presenters;
using db_cp.UI.IView;
using db_cp.buisiness_logic.bl_structures;

namespace db_cp
{
    public partial class CreatePlaylist : Form, ICreatePlaylistView
    {
        private CreatePlaylistPresenter presenter;

        private List<Track> tracks;
        private User u;

        private Home home;

        public event Action<Playlist> PlaylistCreated;
        public CreatePlaylist(User u, Home home)
        {
            InitializeComponent();
            this.presenter = new CreatePlaylistPresenter(this, new Model(u));
            this.u = u;
            this.home = home;

            this.tracks = new List<Track>();
        }
        private void AddTrack_Click(object sender, EventArgs e)
        {
            ChooseTrack choose_track = new ChooseTrack(this.u.Login, this.u.Password, this);
            choose_track.Show();
        }

        private void CreatePlaylist_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn column01 = new DataGridViewTextBoxColumn();
            column01.Name = "track";
            column01.HeaderText = "Название трека";
            column01.Width = 201;
            column01.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.AddRange(column01);
        }
        public void SetCreatePlaylist()
        {
            this.home.RefreshInfo();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Playlist playlist = new Playlist();
            playlist.Name = this.PlName.Text;
            playlist.DateOfCreation = DateTime.Today;
            playlist.Description = this.Description.Text;
            playlist.tracks = this.tracks;

            this.PlaylistCreated(playlist);

            this.Hide();
        }

        public void AddTrackToTable(Track track)
        {
            if (this.tracks.Find(a => a.Name == track.Name) == null)
            {
                this.dataGridView1.Rows.Add(track.Name);

                this.tracks.Add(track);
            }
            else
                MessageBox.Show("Данный трек уже добавлен в этот плейлист!");
        }

        private void Description_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
