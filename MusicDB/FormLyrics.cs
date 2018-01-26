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
    public partial class FormLyrics : Form
    {
        MusicDbContext db = new MusicDbContext();
        public FormLyrics()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            dataGridView1.DataSource = db.GetLyrics();
        }

        private void FormLyrics_Load(object sender, EventArgs e)
        {
            LoadData();
            cmbSongId.DataSource = db.GetSongs();
            cmbSongId.ValueMember = "song_id";
            cmbSongId.DisplayMember = "song_name";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];
                txtLanguage.Text = row.Cells["language"].Value.ToString();
                txtLyrics.Text = row.Cells["lyrics"].Value.ToString();
                cmbSongId.SelectedValue = row.Cells["song_id"].Value.ToString();

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];
                int lyric_id = (int)row.Cells["lyric_id"].Value;
                bool isSuccess = db.UpdateLyric(lyric_id, txtLanguage.Text, txtLyrics.Text, (int)cmbSongId.SelectedValue);
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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bool isSuccess = db.AddLyric(txtLanguage.Text, txtLyrics.Text, (int)cmbSongId.SelectedValue);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];
                int lyric_id = (int)row.Cells["lyric_id"].Value;
                bool isSuccess = db.DeleteLyric(lyric_id);
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
