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
    public partial class AddAlbumForm : Form
    {
        private Album _newAlbum;
        public AddAlbumForm()
        {
            InitializeComponent();
            _newAlbum = new Album();
        }

        private void AddAlbumForm_Load(object sender, EventArgs e)
        {

        }

        public Album NewAlbum
        {
            get { return _newAlbum; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Создаем новый альбом с данными из формы
            _newAlbum.Title = textBoxTitle.Text;
            _newAlbum.Artist = textBoxArtist.Text;
            _newAlbum.Year = (int)numericUpDownYear.Value;
            // Закрываем форму
            this.Close();
        }
    }
}
