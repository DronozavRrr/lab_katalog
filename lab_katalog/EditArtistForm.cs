using static lab_katalog.Form1;
using System.Windows.Forms;
using System;

namespace lab_katalog
{
    public partial class EditArtistForm : Form
    {
        private Artist _editedArtist;

        // Конструктор формы, принимающий выбранного артиста
        public EditArtistForm(Artist artist)
        {
            InitializeComponent();
            _editedArtist = artist;
            // Отобразите данные артиста в элементах управления формы
            textBoxName.Text = artist.Name;
        }

        // Свойство для доступа к отредактированному артисту после сохранения изменений
        public Artist EditedArtist
        {
            get { return _editedArtist; }
        }

        private void EditArtistForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Сохраняем изменения
            _editedArtist.Name = textBoxName.Text;
            // Закрываем форму
            this.Close();
        }
    }
}
