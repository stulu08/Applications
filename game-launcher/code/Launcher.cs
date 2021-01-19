using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.IO;

namespace Stulu_Game_Launcher
{
    class Launcher
    {
        public static void ExecuteGame(string path, Button button)
        {
            try
            {
                Process.Start(path);
                QuitMenu();
            }
            catch (IOException ex)
            {
                button.Content = "Play";
                MessageBox.Show(ex.Message, "Ex01");
            }
        }
        public static void LaunchWeb(String url)
        {
            Process.Start(url);
        }
        public static void QuitMenu()
        {
            Application application = Application.Current;
            application.Shutdown();
        }
        public static void delete(string path, Button button, bool bc)
        {
            try
            {
                Directory.Delete(path, true);
                if(bc)
                    button.Content = "Deleted";
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Ex01");
                if(bc)
                    button.Content = "Delete";
            }

        }

        public static bool CheckPath(string path)
        {
            if (Directory.Exists(path))
                return true;
            else
                return false;
        }
        public static void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                //Delete all files from the Directory
                foreach (string file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }
                //Delete all child Directories
                foreach (string directory in Directory.GetDirectories(path))
                {
                    DeleteDirectory(directory);
                }
                //Delete a Directory
                Directory.Delete(path);
            }
            else
            {
                MessageBox.Show("Missing directory", "Ex01");

            }
        }
    }
}
