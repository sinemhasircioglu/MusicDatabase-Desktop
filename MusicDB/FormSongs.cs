using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicDB
{
    public partial class FormSongs : Form
    {
        MusicDbContext db = new MusicDbContext();
        public FormSongs()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            dataGridView1.DataSource = db.GetSongs();
        }
        private void FormSongs_Load(object sender, EventArgs e)
        {
            LoadData();
            cmbAlbumId.DataSource = db.GetAlbums();
            cmbArtistId.DataSource = db.GetArtists();
            cmbGenreId.DataSource = db.GetGenres();
            cmbAlbumId.ValueMember = "album_id";
            cmbAlbumId.DisplayMember = "album_name";
            cmbArtistId.ValueMember = "artist_id";
            cmbArtistId.DisplayMember = "artist-name";
            cmbGenreId.ValueMember = "genre_id";
            cmbGenreId.DisplayMember = "genre_name";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];
                cmbAlbumId.SelectedValue = (int)row.Cells["album_id"].Value;
                cmbArtistId.SelectedValue = (int)row.Cells["artist_id"].Value;
                cmbGenreId.SelectedValue = (int)row.Cells["genre_id"].Value;
                chcIsFeaturing.Checked = (bool)row.Cells["is_featuring"].Value;
                txtLanguage.Text = row.Cells["language"].Value.ToString();
                txtSongName.Text = row.Cells["song_name"].Value.ToString();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bool isSuccess = db.AddSong((int)cmbAlbumId.SelectedValue, (int)cmbArtistId.SelectedValue, (int)cmbGenreId.SelectedValue, chcIsFeaturing.Checked, txtLanguage.Text, txtSongName.Text);
            if (isSuccess)
            {
                MessageBox.Show("Insert Successfuly");
                LoadData();
            }
            else
            {
                MessageBox.Show("Insert Error");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];
                bool isSuccess = db.UpdateSong((int)row.Cells["song_id"].Value, (int)cmbAlbumId.SelectedValue, (int)cmbArtistId.SelectedValue, (int)cmbGenreId.SelectedValue, chcIsFeaturing.Checked, txtLanguage.Text, txtSongName.Text);
                if (isSuccess)
                {
                    MessageBox.Show("Update Successfuly");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Update Error");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count==1)
            {
                var row = dataGridView1.SelectedRows[0];
                bool isSuccess = db.DeleteSong((int)row.Cells["song_id"].Value);
                if (isSuccess)
                {
                    MessageBox.Show("Delete Successfuly");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Delete Error");
                }
            }
        }
    }
}
