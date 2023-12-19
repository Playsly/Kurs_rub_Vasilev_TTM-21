using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kurs_rub_Vasilev_TTM_21
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Tanks> pictures = new List<Tanks>();
        List<Tanks> TempPictures = new List<Tanks>();

        static string NameFileJSON = "PicturesCatalog.json";
        public MainWindow()
        {
            InitializeComponent();

            spisok.Items.Add("All");

            if (File.Exists(NameFileJSON))
            {
                List<Tanks> readedPicturs = JsonSerializer.Deserialize<List<Tanks>>(File.ReadAllText(NameFileJSON));

                foreach (Tanks pic in readedPicturs)
                {
                    pictures.Add(pic);

                    Country.Items.Add(pic.Name);

                    if (!(spisok.Items.Contains(pic.Categ)))
                        spisok.Items.Add(pic.Categ);

                }
            }
        }

        private void spisok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (spisok.SelectedIndex != -1)
            {
                Country.Items.Clear();
                if (spisok.SelectedItem.ToString().Equals("All"))
                {
                    foreach (Tanks pic in pictures)
                        Country.Items.Add(pic.Name);
                    return;
                }

                TempPictures.Clear();

                foreach (Tanks pic in pictures)
                {
                    if (pic.Categ == spisok.SelectedItem.ToString())
                    {
                        Country.Items.Add(pic.Name);
                        TempPictures.Add(pic);
                    }
                }
            }
        }

        private void Country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((spisok.SelectedIndex == 0) && (str_sch.Text.Length == 0 && str_sch_tg.Text.Length == 0))
            {
                if (Country.Items.Count > 0 && Country.SelectedIndex != -1)
                {
                    img.Source = ByteToImage(Convert.FromBase64String(pictures[Country.SelectedIndex].Img));
                }
            }
            else
            {
                if (TempPictures.Count > 0 && Country.SelectedIndex != -1)
                {
                    img.Source = ByteToImage(Convert.FromBase64String(TempPictures[Country.SelectedIndex].Img));
                }
            }
        }

        private void sve_Click(object sender, RoutedEventArgs e)
        {
            string jsonString = JsonSerializer.Serialize(pictures);
            File.WriteAllText(NameFileJSON, jsonString);
        }

        private void ldfl_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (!(bool)dlg.ShowDialog())
                return;

            Uri fileUri = new Uri(dlg.FileName);

            Add_image_file addPic = new Add_image_file();

            if (addPic.ShowDialog() == true)
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(dlg.FileName);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                Tanks pic = new Tanks(addPic.nm.Text, base64ImageRepresentation, addPic.cr.Text);

                pictures.Add(pic);
                Country.Items.Add(pic.Name);

                if (!(spisok.Items.Contains(pic.Categ)))
                    spisok.Items.Add(pic.Categ);
            }
        }
        static ImageSource ByteToImage(byte[] imageData)
        {
            var bitmap = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            bitmap.BeginInit();
            bitmap.StreamSource = ms;
            bitmap.EndInit();

            return (ImageSource)bitmap;
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (Country.SelectedIndex != -1)
            {
                pictures.Remove(pictures[Country.SelectedIndex]);
                Country.Items.Clear();

                foreach (Tanks pic in pictures)
                    Country.Items.Add(pic.Name);

                spisok.Items.Clear();
                spisok.Items.Add("All");

                foreach (Tanks pic in pictures)
                {
                    if (!(spisok.Items.Contains(pic.Categ)))
                        spisok.Items.Add(pic.Categ);
                }
                spisok.SelectedIndex = 0;
            }
        }

        private void ldurl_Click(object sender, RoutedEventArgs e)
        {
            Add_image_url addPic = new Add_image_url();

            if (addPic.ShowDialog() == true)
            {
                WebClient client = new WebClient();
                string imageUrl = addPic.turl.Text;
                byte[] imageArray = client.DownloadData(imageUrl);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                Tanks pic = new Tanks(addPic.nm1.Text, base64ImageRepresentation, addPic.Country.Text);

                pictures.Add(pic);
                Country.Items.Add(pic.Name);

                if (!(spisok.Items.Contains(pic.Categ)))
                    spisok.Items.Add(pic.Categ);
            }
        }

        private void adtg_Click(object sender, RoutedEventArgs e)
        {
            if (Country.SelectedIndex != -1 && Country.Items.Count == pictures.Count && add_tag.Text.Length > 0)
            {
                pictures[Country.SelectedIndex].add_tag(add_tag.Text);
            }
            else
                MessageBox.Show("Select category All or fill in the tag field");
        }

        private void srchtg_Click(object sender, RoutedEventArgs e)
        {
            Country.Items.Clear();
            TempPictures.Clear();
            foreach (Tanks pic in pictures)
            {
                foreach (string tag in pic.Tags)
                {
                    if (tag.ToLower().Equals(str_sch_tg.Text.ToLower()))
                    {
                        Country.Items.Add(pic.Name);
                        TempPictures.Add(pic);
                    }
                }
            }
        }

        private void sch_Click(object sender, RoutedEventArgs e)
        {
            Country.Items.Clear();
            TempPictures.Clear();
            foreach (Tanks pic in pictures)
            {
                if (pic.Name.ToLower().Contains(str_sch.Text.ToLower()))
                {
                    Country.Items.Add(pic.Name);
                    TempPictures.Add(pic);
                }
            }
        }
    }
}