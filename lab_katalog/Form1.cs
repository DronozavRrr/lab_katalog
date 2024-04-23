using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static lab_katalog.Form1;

namespace lab_katalog
{

    


    public partial class Form1 : Form
    {
        public DataGridView GetActiveGridView;
        public Artist SelectedArtist;
        public Album SelectedAlbum;

        [Serializable]
        public class Song
        {
            public string Title { get; set; }
            public string Artist { get; set; }
            public string Album { get; set; }
        }

        [Serializable]
        public class Album
        {
            public string Title { get; set; }
            public string Artist { get; set; }
            public int Year { get; set; }
            public List<Song> Songs { get; set; }
        }

        [Serializable]
        public class Artist
        {
            public string Name { get; set; }
            public List<Album> Albums { get; set; }
        }
        public class Catalog
        {
            public List<Artist> Artists { get; set; }
            public List<Album> Albums { get; set; }
            public List<Song> Songs { get; set; }

            public Catalog()
            {
                Artists = new List<Artist>();
                Albums = new List<Album>();
                Songs = new List<Song>();
            }
        }

        private Catalog catalog;

        private List<Artist> artistList;
        private List<Album> albumList;
        private List<Song> songList;

        public Form1()
        {
            InitializeComponent();
            catalog = new Catalog();
            this.FormClosing += MainForm_FormClosing;
            LoadData();
            ShowCatalog();
        }



        private void LoadData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Catalog));
            using (FileStream fileStream = new FileStream("catalog.xml", FileMode.Open))
            {
                catalog = (Catalog)serializer.Deserialize(fileStream);
            }

            albumList = catalog.Artists.SelectMany(artist => artist.Albums).ToList();
            songList = catalog.Artists.SelectMany(artist => artist.Albums)
                                       .SelectMany(album => album.Songs)
                                       .ToList();
        }

        private void SaveData()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Catalog));
                string filePath = Path.Combine("catalog.xml");
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fileStream, catalog);
                }
                MessageBox.Show("Данные успешно сохранены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        private void ShowCatalog()
        {
            BindingSource source1 = new BindingSource();
            source1.DataSource = catalog.Artists;

            BindingSource source2 = new BindingSource();
            if (SelectedArtist != null) 
            source2.DataSource = SelectedArtist.Albums;

            
            BindingSource source3 = new BindingSource();
            if (SelectedAlbum != null)
                source3.DataSource = SelectedAlbum.Songs;


            dataGridView1.DataSource = source1;

            dataGridView2.DataSource = source2;


            dataGridView3.DataSource = source3;
        }
        private void btnAddArtist_Click_1(object sender, EventArgs e)
        {
            Artist artist = new Artist();
            artist.Albums = new List<Album>();
            Album album = new Album();
            album.Songs = new List<Song>();
            Song song = new Song();
            DataGridView activeGridView = GetActiveGridView;

            if (activeGridView != null)
            {
                int selectedIndex = activeGridView.SelectedRows.Count > 0 ? activeGridView.SelectedRows[0].Index : 0;

                if (selectedIndex >= 0)
                {
                    if (activeGridView == dataGridView1)
                    {
                        catalog.Artists.Add(artist);

                        ShowCatalog();
                    }
                    else if (activeGridView == dataGridView2)
                    {
                        catalog.Albums.Add(album);

                        if (SelectedArtist != null && SelectedArtist.Albums != null)
                        {
                            SelectedArtist.Albums.Add(album);
                        }
                        ShowCatalog();
                    }
                    else if (activeGridView == dataGridView3)
                    {
                        catalog.Songs.Add(song);
                        if (SelectedAlbum != null)
                        {
                            SelectedAlbum.Songs.Add(song);
                        }
                        ShowCatalog();
                    }
                }
                else
                {
                    if (activeGridView == dataGridView1)
                    {
                        catalog.Artists.Add(artist);
                    }
                    else if (activeGridView == dataGridView2)
                    {
                        catalog.Albums.Add(album);
                    }
                    else if (activeGridView == dataGridView3)
                    {
                        catalog.Songs.Add(song);
                    }
                    ShowCatalog();
                }
            }
            else
            {
                catalog.Artists.Add(artist);
                ShowCatalog();
            }
        }

        private void btnEditArtist_Click_1(object sender, EventArgs e)
        {
            if (GetActiveGridView.SelectedRows.Count == 0) return;
            int selectedIndex = GetActiveGridView.SelectedRows[0].Index;
            if (selectedIndex >= 0)
            {
                if (GetActiveGridView == dataGridView1)
                {
                    Artist selectedArtist = catalog.Artists[selectedIndex];

                    EditArtistForm editForm = new EditArtistForm(selectedArtist);
                    editForm.ShowDialog();

                    catalog.Artists[selectedIndex] = editForm.EditedArtist;
                    ShowCatalog();
                }
                else if (GetActiveGridView == dataGridView2)
                {

                    Album selectedAlbum = SelectedArtist.Albums[selectedIndex];
                    int i = 0;
                    bool flag = false;
                    foreach (Album album in catalog.Albums)
                    {
                        
                        if (album == selectedAlbum)
                        {
                            flag = true;
                            break;
                        }
                        i++;
                    }
                    EditAlbumForm editForm = new EditAlbumForm(selectedAlbum);
                    editForm.ShowDialog();
                    if(flag) catalog.Albums[i] = editForm.EditedAlbum;


                    ShowCatalog();
                }
                else if (GetActiveGridView == dataGridView3)
                {

                    Song selectedSong = SelectedAlbum.Songs[selectedIndex];
                    int i = 0;
                    bool flag = false;
                    foreach (Song song in catalog.Songs)
                    {

                        if (song == selectedSong)
                        {
                            flag = true;
                            break;
                        }
                        i++;
                    }
                    EditSongForm editForm = new EditSongForm(selectedSong);
                    editForm.ShowDialog();

                    if (flag)  catalog.Songs[i] = editForm.EditedSong;
                    ShowCatalog();
                }

               
            }
        }

        private void btnDeleteArtist_Click_1(object sender, EventArgs e)
        {
            if (GetActiveGridView.SelectedRows.Count == 0) return;
            // Логика удаления артиста
            int selectedIndex = GetActiveGridView.SelectedRows[0].Index;

            if (selectedIndex >= 0)
            {
                if (GetActiveGridView == dataGridView1)
                {
                    if (catalog.Artists.Count > 0)
                    {
                        catalog.Artists.RemoveAt(selectedIndex);
                        ShowCatalog();
                    }
                }
                else if (GetActiveGridView == dataGridView2)
                {
                    if (catalog.Albums.Count > 0)
                    {
                        Album a = SelectedArtist.Albums[selectedIndex];
                        catalog.Albums.Remove(a);
                        SelectedArtist.Albums.RemoveAt(selectedIndex);
                        ShowCatalog();
                    }
                }
                else if (GetActiveGridView == dataGridView3)
                {
                    if (catalog.Songs.Count > 0)
                    {
                        Song a = SelectedAlbum.Songs[selectedIndex];
                        catalog.Songs.Remove(a);
                        SelectedAlbum.Songs.RemoveAt(selectedIndex);
                        ShowCatalog();
                    }
                }


            }

          
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            GetActiveGridView = (DataGridView)sender;
            

        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            GetActiveGridView = (DataGridView)sender;
        }

        private void dataGridView3_Click(object sender, EventArgs e)
        {
            GetActiveGridView = (DataGridView)sender;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
            {
                if (e.RowIndex >= 0)
                {
                    SelectedArtist = catalog.Artists[e.RowIndex];
                    if(SelectedArtist.Albums != null)
                    {
                        if (SelectedArtist.Albums.Count == 0) SelectedAlbum = null;
                        else SelectedAlbum = SelectedArtist.Albums[0];
                    }
                   
                    BindingSource source2 = new BindingSource();
                    if (SelectedArtist != null)
                        source2.DataSource = SelectedArtist.Albums;


                    BindingSource source3 = new BindingSource();
                    if (SelectedAlbum != null)
                        source3.DataSource = SelectedAlbum.Songs;

                    dataGridView2.DataSource = source2;


                    dataGridView3.DataSource = source3;
                }
            }
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            for (int i = 0; i < dataGridView2.SelectedCells.Count; i++)
            {
                if (e.RowIndex >= 0)
                {
                    SelectedAlbum = SelectedArtist.Albums[e.RowIndex];

                    BindingSource source3 = new BindingSource();
                    if (SelectedAlbum != null) source3.DataSource = SelectedAlbum.Songs;
                    dataGridView3.DataSource = source3;

                }
            }
        }
    }
}
