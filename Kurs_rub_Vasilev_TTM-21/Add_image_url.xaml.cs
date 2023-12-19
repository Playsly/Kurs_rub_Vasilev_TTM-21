using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kurs_rub_Vasilev_TTM_21
{
    /// <summary>
    /// Логика взаимодействия для Add_image_url.xaml
    /// </summary>
    public partial class Add_image_url : Window
    {
        public Add_image_url()
        {
            InitializeComponent();
        }

        private void okey_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void otmena_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
