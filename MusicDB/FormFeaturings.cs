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
    public partial class FormFeaturings : Form
    {
        MusicDbContext db = new MusicDbContext();
        public FormFeaturings()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            dataGridView1.DataSource = db.GetFeaturings();
        }

        private void FormFeaturings_Load(object sender, EventArgs e)
        {
            LoadData();
            cmbArtistId.DataSource = db.GetFeaturings();
            cmbArtistId.ValueMember = "artist_id";
            cmbArtistId.DisplayMember = "artist_name";
            cmbSongId.DataSource = db.GetFeaturings();
            cmbSongId.ValueMember = "song_id";
            cmbSongId.DisplayMember = "song_name";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];
                cmbArtistId.SelectedValue = row.Cells["artist_id"].Value;
                cmbSongId.SelectedValue = row.Cells["song_id"].Value;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int artist_id = (int)cmbArtistId.SelectedValue;
            int song_id = (int)cmbSongId.SelectedValue;
            bool isSuccess = db.AddFeaturing(artist_id, song_id);
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
            if(dataGridView1.SelectedRows.Count==1)
            {
                var row = dataGridView1.SelectedRows[0];
                int featuring_id = (int)row.Cells["featuring_id"].Value;
                int artist_id = (int)cmbArtistId.SelectedValue;
                int song_id = (int)cmbSongId.SelectedValue;
                bool isSuccess = db.UpdateFeaturing(featuring_id, artist_id, song_id);
                if (isSuccess)
                {
                    MessageBox.Show("Update successfuly");
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
                bool isSuccess = db.DeleteFeaturing((int)row.Cells["featuring_id"].Value);
                if (isSuccess)
                {
                    MessageBox.Show("Delete successfuly");
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
