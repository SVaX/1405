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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoAgainAndAgain.Windows;

namespace DemoAgainAndAgain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _adminCode = "0000";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void clientButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ServicesWindow();
            window.Show();
            this.Close();
        }

        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
            codeLabel.Visibility = Visibility.Visible;
            codeTextBox.Visibility = Visibility.Visible;
            enterButton.Visibility = Visibility.Visible;
        }

        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            if (codeTextBox.Text == _adminCode)
            {
                var window = new ServicesWindow(true);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Код неверный!");
                return;
            }
        }
    }
}
