using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static lab_katalog.Form1;

namespace lab_katalog
{
    public partial class AddSongForm : Form
    {

        private Song _newSong;
        public AddSongForm()
        {
            InitializeComponent();
            _newSong = new Song();
        }

        private void AddSongForm_Load(object sender, EventArgs e)
        {

        }

        public Song NewSong
        {
            get { return _newSong; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Создаем новую песню с данными из формы
            _newSong.Title = textBoxTitle.Text;
            _newSong.Artist = textBoxArtist.Text;
            _newSong.Album = textBoxAlbum.Text;
            // Закрываем форму
            this.Close();
        }
    }
}
