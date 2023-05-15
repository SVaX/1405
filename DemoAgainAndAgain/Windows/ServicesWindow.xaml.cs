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

namespace DemoAgainAndAgain.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServicesWindow.xaml
    /// </summary>
    public partial class ServicesWindow : Window
    {
        private bool _isAdmin;
        NewDemoDbAgainContext db = new NewDemoDbAgainContext();
        List<Service> services = new List<Service>();
        public ServicesWindow(bool IsAdmin = false)
        {
            InitializeComponent();
            _isAdmin = IsAdmin;
            services = db.Services.ToList();
            foreach (var ser in services)
            {
                ser.Photo = ser.Photo.Replace($"/Resources/", "");
                ser.Photo = $"/Resources/{ser.Photo}";
                if (ser.Discount != 0)
                {
                    ser.CostWithDiscount = ser.Cost - (ser.Cost * (ser.Discount / 100.00));
                }
                else
                {
                    ser.CostWithDiscount = ser.Cost;
                }
                db.Entry(ser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }

            servicesList.ItemsSource = db.Services.ToList();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void discoundFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void costSortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void recordButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
