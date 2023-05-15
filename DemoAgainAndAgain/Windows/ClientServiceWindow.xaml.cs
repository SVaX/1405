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
    /// Логика взаимодействия для ClientServiceWindow.xaml
    /// </summary>
    public partial class ClientServiceWindow : Window
    {
        NewDemoDbAgainContext db = new NewDemoDbAgainContext();
        public ClientServiceWindow()
        {
            InitializeComponent();
            List<ClientService> services = db.ClientServices.Where(x => x.Start <= DateTime.Now.AddDays(1)).ToList();
            serviceList.Items.Clear();
            serviceList.ItemsSource = services;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ServicesWindow(true);
            window.Show();
            this.Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddClientServiceWindow();
            window.Show();
            this.Close();
        }
    }
}
