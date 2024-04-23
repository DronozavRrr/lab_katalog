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
    public partial class EditAlbumForm : Form
    {

        private Album _editedAlbum;
        public EditAlbumForm(Album album)
        {
            InitializeComponent();
            _editedAlbum = album;
            // Отобразите данные альбома в элементах управления формы
            textBoxTitle.Text = album.Title;
            textBoxArtist.Text = album.Artist;
            numericUpDownYear.Value = album.Year;
        }

        private void EditAlbumForm_Load(object sender, EventArgs e)
        {

        }

        public Album EditedAlbum
        {
            get { return _editedAlbum; }
        }


        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Сохраняем изменения
            _editedAlbum.Title = textBoxTitle.Text;
            _editedAlbum.Artist = textBoxArtist.Text;
            _editedAlbum.Year = (int)numericUpDownYear.Value;
            // Закрываем форму
            this.Close();
        }
    }
}
