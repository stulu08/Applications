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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WebSite_Click(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchWeb("https://derstulu.wixsite.com/website");
        }

        private void Github_Click(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchWeb("https://github.com/stulu08");
        }

        private void RMLB_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new MC();
        }

        private void RZSB_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new RZS();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Launcher.QuitMenu();
        }
    }
}
