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
    public partial class FormLogTable : Form
    {
        MusicDbContext db = new MusicDbContext();
        public FormLogTable()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            dataGridView1.DataSource = db.GetLogTable();
        }
        private void FormLogTable_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
