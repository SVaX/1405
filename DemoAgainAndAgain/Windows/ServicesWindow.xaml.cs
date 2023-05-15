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

            InitList();
            InitComboBoxes();

            if (_isAdmin)
            {
                addButton.Visibility = Visibility.Visible;
                recordButton.Visibility = Visibility.Visible;
            }
        }

        private void InitList()
        {
            services = db.Services.ToList();
            servicesList.ItemsSource = db.Services.ToList();
        }

        private void InitComboBoxes()
        {
            discoundFilter.ItemsSource = new List<string>
            {
                "Все", "0 до 5", "5 до 15", "15 до 30", "30-70", "70-100"
            };

            costSortComboBox.ItemsSource = new List<string>
            {
                "По возрастанию", "По убыванию"
            };

            discoundFilter.SelectedIndex = 0;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var foundAgents = services.Where(x => x.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
            servicesList.ItemsSource = foundAgents;
        }

        private void discoundFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (discoundFilter.SelectedIndex)
            {
                case 0:
                    {
                        InitList();
                        break;
                    }
                case 1:
                    {
                        services = db.Services.Where(x => x.Discount >= 0 && x.Discount < 5).ToList();
                        servicesList.ItemsSource = services;
                        break;
                    }
                case 2:
                    {
                        services = db.Services.Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
                        servicesList.ItemsSource = services;
                        break;
                    }
                case 3:
                    {
                        services = db.Services.Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
                        servicesList.ItemsSource = services;
                        break;
                    }
                case 4:
                    {
                        services = db.Services.Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
                        servicesList.ItemsSource = services;
                        break;
                    }
                case 5:
                    {
                        services = db.Services.Where(x => x.Discount >= 70 && x.Discount < 100).ToList();
                        servicesList.ItemsSource = services;
                        break;
                    }
            }

            costSortComboBox_SelectionChanged(sender, e);
        }

        private void costSortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (costSortComboBox.SelectedIndex)
            {
                case 0:
                    {
                        services = services.OrderBy(x => x.CostWithDiscount).ToList();
                        servicesList.ItemsSource = services;
                        break;
                    }
                case 1:
                    {
                        services = services.OrderByDescending(x => x.CostWithDiscount).ToList();
                        servicesList.ItemsSource = services;
                        break;
                    }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdmin)
            {
                var window = new AddServiceWindow();
                window.Show();
                this.Close();
            }
        }

        private void recordButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isAdmin)
            {
                var window = new ClientServiceWindow();
                window.Show();
                this.Close();
            }
        }

        private void servicesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var window = new EditServiceWindow(servicesList.SelectedItem as Service);
            window.Show();
            this.Close();
        }
    }
}
