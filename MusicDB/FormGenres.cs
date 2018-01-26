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
    public partial class FormGenres : Form
    {
        MusicDbContext db = new MusicDbContext();

        public FormGenres()
        {
            InitializeComponent();
        }

        private void FormGenres_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            dataGridView1.DataSource = db.GetGenres();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 1)
            {
                var row = dataGridView1.SelectedRows[0];
                txtGenreName.Text = row.Cells["genre_name"].Value.ToString();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            bool isSuccess = db.AddGenre(txtGenreName.Text);
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
            if (dataGridView1.SelectedRows.Count==1)
            {
                var row = dataGridView1.SelectedRows[0];
                int genre_id = (int)row.Cells["genre_id"].Value;
                bool isSuccess = db.UpdateGenre(genre_id,txtGenreName.Text);
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
                int genre_id = (int)row.Cells["genre_id"].Value;
                bool isSuccess = db.DeleteGenre(genre_id);
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
