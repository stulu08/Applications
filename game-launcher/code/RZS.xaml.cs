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
    /// Interaktionslogik für RZS.xaml
    /// </summary>
    public partial class RZS : Page
    {
        public RZS()
        {
            InitializeComponent();
            ProgressD.Visibility = Visibility.Hidden;
            ProgressTextD.Content = "";

            if (!Launcher.CheckPath("Russian zombie survival"))
            {
                LaunchGame.Content = "Not installed";
                LaunchGame.IsEnabled = false;
                UpdateGame.Content = "Install";
            }
            else
            {
                LaunchGame.Content = "Play";
                LaunchGame.IsEnabled = true;
                UpdateGame.Content = "Update";
            }
        }
        private void LaunchGame_Click(object sender, RoutedEventArgs e)
        {
            Launcher.ExecuteGame("Russian zombie survival\\Russian zombie survival.exe", LaunchGame);


            if (!Launcher.CheckPath("Russian zombie survival"))
            {
                LaunchGame.Content = "Not installed";
                LaunchGame.IsEnabled = false;
                UpdateGame.Content = "Install";
            }
            else
            {
                LaunchGame.Content = "Play";
                LaunchGame.IsEnabled = true;
                UpdateGame.Content = "Update";
            }
        }

        private void WatchTrailer_Click(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchWeb("https://www.youtube.com/watch?v=D6yX7v2p49w");
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
                client.DownloadFileAsync(new Uri("https://github.com/stulu08/Russian-Zombie-Survival--UnityProject/blob/master/Files/LatestBuild.zip?raw=true"), "Russian zombie survival.zip");
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

            try
            {
                ProgressTextD.Content = "Unziping...";
                ZipFile.ExtractToDirectory("Russian zombie survival.zip", "Russian zombie survival");
                ProgressTextD.Content = "Ready";

            }
            catch (IOException ex)
            {
                ProgressTextD.Content = "Ex11";
                MessageBox.Show(ex.Message, "Ex11");
            }

            if (!Launcher.CheckPath("Russian zombie survival"))
            {
                LaunchGame.Content = "Not installed";
                LaunchGame.IsEnabled = false;
            }
            else
            {
                LaunchGame.Content = "Play";
                LaunchGame.IsEnabled = true;
            }

        }

        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            Launcher.delete("Russian zombie survival", DeleteGame, true);
            if (!Launcher.CheckPath("Russian zombie survival"))
            {
                LaunchGame.Content = "Not installed";
                LaunchGame.IsEnabled = false;
                UpdateGame.Content = "Install";
            }
            else
            {
                LaunchGame.Content = "Play";
                LaunchGame.IsEnabled = true;
                UpdateGame.Content = "Update";
            }

        }
    }
}
