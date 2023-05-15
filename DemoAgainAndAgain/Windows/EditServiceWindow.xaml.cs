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
    /// Логика взаимодействия для EditServiceWindow.xaml
    /// </summary>
    public partial class EditServiceWindow : Window
    {
        NewDemoDbAgainContext db = new NewDemoDbAgainContext();
        Service _currentService;

        public EditServiceWindow(Service service)
        {
            InitializeComponent();
            _currentService = service;
            DataInitMethod();
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

        private void DataInitMethod()
        {
            if (_currentService != null)
            {
                NameTextBox.Text = _currentService.Name;
                CostTextBox.Text = Convert.ToString(_currentService.Cost);
                DescriptionTextBox.Text = _currentService.Discription;
                DurationTextBox.Text = Convert.ToString(_currentService.Duration);
                DiscountTextBox.Text = Convert.ToString(_currentService.Discount);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _currentService.Name = NameTextBox.Text;
            _currentService.Cost = Convert.ToDouble(CostTextBox.Text);
            _currentService.Discription = DescriptionTextBox.Text;
            _currentService.Discount = Convert.ToInt32(DiscountTextBox.Text);
            _currentService.Duration = Convert.ToInt32(DurationTextBox.Text);
            try
            {
                db.Entry(_currentService).State = EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Информация успешно изменена!", "Окно оповещений");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

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

                System.IO.File.Copy(filename, parentDirName);

                _currentService.Photo = ofd.SafeFileName;
                db.Entry(_currentService).State = EntityState.Modified;
                db.SaveChanges();

                InitImage();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var admin = new ServicesWindow();
            admin.Show();
            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var hist in db.ClientServices)
            {
                if (hist.ServiceId == _currentService.ServiceId)
                {
                    MessageBox.Show("Нельзя удалить сервис для которого существует запись");
                    return;
                }
            }

            db.Services.Remove(_currentService);
            db.SaveChanges();

            MessageBox.Show("Услуга успешно удалена");

            var window = new ServicesWindow();
            window.Show();
            this.Close();
        }
    }
}
