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
    /// Логика взаимодействия для AddClientServiceWindow.xaml
    /// </summary>
    public partial class AddClientServiceWindow : Window
    {
        NewDemoDbAgainContext db = new NewDemoDbAgainContext();
        public AddClientServiceWindow()
        {
            InitializeComponent();
            InitTextBoxes();
        }

        private void InitTextBoxes()
        {
            foreach (var ser in db.Services)
            {
                serviceNameCombo.Items.Add(ser.Name);
            }

            foreach (var cli in db.Clients)
            {
                clientNameCombo.Items.Add(cli.Name);
            }

            serviceNameCombo.SelectedIndex = 0;
            clientNameCombo.SelectedIndex = 0;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ServicesWindow(true);
            window.Show();
            this.Close();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            var clientservice = new ClientService();
            var service = new Service();
            var client = new Client();
            foreach (var ser in db.Services)
            {
                if (ser.Name == serviceNameCombo.SelectedItem.ToString())
                {
                    service = ser;
                }
            }
            foreach (var cli in db.Clients)
            {
                if (cli.Name == clientNameCombo.SelectedItem.ToString())
                {
                    client = cli;
                }
            }

            clientservice.ClientId = client.ClientId;
            clientservice.ServiceId = service.ServiceId;
            clientservice.ServiceName = service.Name;
            clientservice.ClientName = client.Name;
            clientservice.Start = calendar.SelectedDate ;

            db.ClientServices.Add(clientservice);
            db.SaveChanges();
            MessageBox.Show("Успешно добавлено!");
            backButton_Click(sender, e);
        }
    }
}
