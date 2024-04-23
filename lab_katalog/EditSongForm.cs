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
    public partial class EditSongForm : Form
    {

        private Song _editedSong;
        public EditSongForm(Song song)
        {
            InitializeComponent();
            _editedSong = song;
            // Отобразите данные песни в элементах управления формы
            textBoxTitle.Text = song.Title;
            textBoxArtist.Text = song.Artist;
            textBoxAlbum.Text = song.Album;
        }


        public Song EditedSong
        {
            get { return _editedSong; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }


        private void EditSongForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Сохраняем изменения
            _editedSong.Title = textBoxTitle.Text;
            _editedSong.Artist = textBoxArtist.Text;
            _editedSong.Album = textBoxAlbum.Text;
            // Закрываем форму
            this.Close();
        }
    }
}
