using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
using System.IO.Compression;
using System.IO;
using System.IO.Packaging;

namespace Stulu_Game_Launcher
{
    /// <summary>
    /// Interaktionslogik für MC.xaml
    /// </summary>
    public partial class MC : Page
    {
        public MC()
        {
            InitializeComponent();
            ProgressD.Visibility = Visibility.Hidden;
            ProgressTextD.Content = "";

            if (!Launcher.CheckPath("MCClient"))
            {
                
                UpdateGame.Content = "Install";
            }
            else
            {
                
                UpdateGame.Content = "Update";
            }
        }
        private void LaunchGame_Click(object sender, RoutedEventArgs e)
        {
           


            if (!Launcher.CheckPath("MCClient"))
            {
               
                UpdateGame.Content = "Install";
            }
            else
            {
                
                UpdateGame.Content = "Update";
            }
        }

        
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Launcher.QuitMenu();
        }

        private void UpdateGame_Click(object sender, RoutedEventArgs e)
        {
            var client = new System.Net.WebClient();
            try
            {
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri("https://github.com/stulu08/Russian-Zombie-Survival--UnityProject/blob/master/Files/LatestBuild.zip?raw=true"), "MCClient.zip");
                ProgressD.Visibility = Visibility.Visible;


            }
            catch (IOException ex)
            {
                UpdateGame.Content = "Ex10";
                MessageBox.Show(ex.Message);
            }




        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressBar progress = ProgressD;
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            double Value = int.Parse(Math.Truncate(percentage).ToString());
            progress.Value = Math.Round(Value);
            ProgressTextD.Content = Value + "% | " + e.BytesReceived / 1000 / 1000 + "mb / " + e.TotalBytesToReceive / 1000 / 1000 + "mb";


        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            UpdateGame.Content = "Update";
            UpdateGame.IsEnabled = true;
            ProgressTextD.Content = "Complete";

           
        }
    }
}
