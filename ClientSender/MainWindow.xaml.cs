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
using System.Net.Sockets;
using System.Net;

namespace ClientSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NetworkStream netStream;
        TcpClient clients;
        string address;
        int usedPort;
        public MainWindow()
        {
            InitializeComponent();

            adrress.Text = IPAddress.Loopback.ToString();
            usedPort = 22;
            port.Text = usedPort.ToString();
        }

        private void Button_Connect(object sender, RoutedEventArgs e)
        {
            address = adrress.Text;
            usedPort = int.Parse(port.Text);
            try
            {
                clients = new TcpClient(address, usedPort);
                info.Items.Add("Wysyłam zapytanie...");
            }
            catch (Exception ex)
            {
                info.Items.Add("Błąd: Nie udało się nawiązać połączenia!");
                MessageBox.Show(ex.ToString());
            }
        }


        private void Button_Send(object sender, RoutedEventArgs e)
        {
            netStream = clients.GetStream();

            if (netStream.CanWrite)
            {
                Byte[] sendBytes = Encoding.UTF8.GetBytes(message.Text);
                netStream.Write(sendBytes, 0, sendBytes.Length);
                
            }
            else
            {
                info.Items.Add("Nie możesz wysłać wiadomości!");
                clients.Close();
                netStream.Close();
                return;
            }
        }

        private void Button_Disconnect(object sender, RoutedEventArgs e)
        {
            netStream.Close();
            clients.Close();
            info.Items.Add("Nastąpiło rozłączenie... ");
        }
    }
}
