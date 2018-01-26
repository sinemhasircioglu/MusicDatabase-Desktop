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
    public partial class FormArtists : Form
    {
        MusicDbContext db = new MusicDbContext();

        public FormArtists()
        {
            InitializeComponent();
        }

        private void FormArtists_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            dataGridView1.DataSource = db.GetArtists();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bool isSuccess = db.AddArtist(txtArtistName.Text, txtCountry.Text, chkIsGroup.Checked, txtRealName.Text, (int)txtStartedYear.Value);
            if (isSuccess)
            {
                MessageBox.Show("Insert Successfully");
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
                int artistId = (int)row.Cells["artist_id"].Value;
                bool isSuccess = db.UpdateArtist(artistId, txtArtistName.Text, txtCountry.Text, chkIsGroup.Checked, txtRealName.Text, (int)txtStartedYear.Value);
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
                int artistId = (int)row.Cells["artist_id"].Value;
                bool isSuccess = db.DeleteArtist(artistId);
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
                txtArtistName.Text = row.Cells["artist_name"].Value.ToString();
                txtCountry.Text = row.Cells["country"].Value.ToString();
                txtRealName.Text = row.Cells["real_name"].Value.ToString();
                try
                {
                    chkIsGroup.Checked = (bool)row.Cells["is_group"].Value;
                }
                catch
                {
                    chkIsGroup.Checked = false;
                }
                try
                {
                    txtStartedYear.Value = (decimal)row.Cells["started_year"].Value;
                }
                catch
                {
                    txtStartedYear.Value = txtStartedYear.Minimum;
                }
            }
        }
    }
}
