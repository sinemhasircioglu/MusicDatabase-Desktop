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
    public partial class FormAlbums : Form
    {
        MusicDbContext db = new MusicDbContext();

        public FormAlbums()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            dataGridView1.DataSource = db.GetAlbums();
        }

        private void FormAlbums_Load(object sender, EventArgs e)
        {
            LoadData();
            cmbArtistId.DataSource = db.GetArtists();
            cmbArtistId.ValueMember = "artist_id";
            cmbArtistId.DisplayMember = "artist_name";
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bool isSuccess = db.AddAlbum(txtAlbumName.Text, (int)cmbArtistId.SelectedValue, txtBarcode.Text, txtCountry.Text, chcIsSingle.Checked, (int)numReleaseYear.Value);
            if (isSuccess)
            {
                MessageBox.Show("Insert successfuly");
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
                int album_id = (int)row.Cells["album_id"].Value;
                bool isSuccess = db.UpdateAlbum(album_id, txtAlbumName.Text, (int)cmbArtistId.SelectedValue, txtBarcode.Text, txtCountry.Text, chcIsSingle.Checked, (int)numReleaseYear.Value);
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
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];
                int album_id = (int)row.Cells["album_id"].Value;
                bool isSuccess = db.DeleteAlbum(album_id);
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];
                txtAlbumName.Text = row.Cells["album_name"].Value.ToString();
                cmbArtistId.SelectedValue = (int)row.Cells["artist_id"].Value;
                txtBarcode.Text = row.Cells["barcode"].Value.ToString();
                txtCountry.Text = row.Cells["country"].Value.ToString();
                chcIsSingle.Checked = (bool)row.Cells["is_single"].Value;
                numReleaseYear.Value = (int)row.Cells["release_year"].Value;
            }
        }
    }
}
