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
    public partial class FormAnasayfa : Form
    {
        MusicDbContext db = new MusicDbContext();
        FormAlbums frmAlbum = new FormAlbums();
        FormArtists frmArtist = new FormArtists();
        FormFeaturings frmFeaturing = new FormFeaturings();
        FormGenres frmGenre = new FormGenres();
        FormLyrics frmLyric = new FormLyrics();
        FormSongs frmSong = new FormSongs();
        FormLogTable frmLog = new FormLogTable();
        FormAboutMe frmAbout = new FormAboutMe();

        public FormAnasayfa()
        {
            InitializeComponent();
        }

        private void albumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAlbum.ShowDialog();
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArtist.ShowDialog();
        }

        private void featuringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFeaturing.ShowDialog();
        }

        private void genresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGenre.ShowDialog();
        }

        private void lyricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLyric.ShowDialog();
        }

        private void songsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSong.ShowDialog();
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLog.ShowDialog();
        }

        private void FormAnasayfa_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.GetSongInfo();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string kelime = textBox1.Text;
            dataGridView1.DataSource = db.Search(kelime);

        }

        private void aboutMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout.ShowDialog();
        }
    }
}
