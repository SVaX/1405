using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DemoAgainAndAgain.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        NewDemoDbAgainContext db = new NewDemoDbAgainContext();
        Service _currentService = new Service();

        public AddServiceWindow()
        {
            InitializeComponent();
            InitImage();
        }

        private void InitImage()
        {
            BitmapImage imageSource = new BitmapImage();
            imageSource.BeginInit();
            try
            {
                if (_currentService != null)
                {
                    imageSource.UriSource = new Uri(@"/DemoApp;component" + _currentService.Photo);
                    BitmapImage picture = new BitmapImage();
                    picture.BeginInit();
                    picture.UriSource = new Uri(@"/DemoApp;component/Resources/", UriKind.Relative);
                    if (imageSource.UriSource == picture.UriSource)
                    {
                        imageSource.UriSource = new Uri(@"/DemoApp;component/Resources/school_logo.png", UriKind.Relative);
                    }
                }
                else
                    imageSource.UriSource = new Uri(@"/DemoApp;component/Resources/school_logo.png");
            }
            catch
            {
                return;
            }
            imageSource.EndInit();
            Picture.Source = imageSource;
        }


        private void ImageButoon_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.DefaultExt = ".jpg";
            ofd.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                string filename = ofd.FileName;
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                FileInfo fileInfo = new FileInfo(currentDir);
                DirectoryInfo dirInfo = fileInfo.Directory.Parent;
                string parentDirName = dirInfo.Name;

                fileInfo = new FileInfo(parentDirName);
                dirInfo = fileInfo.Directory.Parent;
                parentDirName = dirInfo.Name;

                fileInfo = new FileInfo(dirInfo.ToString());
                dirInfo = fileInfo.Directory.Parent;
                parentDirName = dirInfo.ToString() + "\\Resources\\" + ofd.SafeFileName;

                System.IO.File.Copy(filename, parentDirName, true);

                _currentService.Photo = ofd.SafeFileName;

                InitImage();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentService == null)
                return;
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text))
                MessageBox.Show("Введите название", "Ошибка");

            if (string.IsNullOrEmpty(DurationTextBox.Text))
                MessageBox.Show("Введите длительность", "Ошибка");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка");
                return;
            }

            if (Convert.ToInt32(DurationTextBox.Text) > 240)
            {
                MessageBox.Show("Нельзя делать услугу более 4 часов");
                return;
            }

            _currentService.Name = NameTextBox.Text;
            _currentService.Cost = Convert.ToDouble(CostTextBox.Text);
            _currentService.Duration = Convert.ToInt32(DurationTextBox.Text);
            _currentService.Discount = Convert.ToInt32(DiscountTextBox.Text);
            try
            {
                db.Services.Add(_currentService);
                db.SaveChanges();
                MessageBox.Show("Данные были успешно добавлены!", "Внимание");
                var window = new ServicesWindow();
                window.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка");
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var admin = new ServicesWindow(true);
            admin.Show();
            Close();
        }
    }
}
