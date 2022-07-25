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
using System.Diagnostics;
using System.IO;

namespace PC_Analyse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

        
    {
        // info über ein ordner zu haben
        public DirectoryInfo winTemp;
        public DirectoryInfo appTemp;
        public MainWindow()
        {
            winTemp = new DirectoryInfo(@"C:\windows\Temp");
            appTemp = new DirectoryInfo(System.IO.Path.GetTempPath());
            recupdate();
            InitializeComponent();
        }

        // größe des Ordner berechnen
        public long DirSize(DirectoryInfo size)
        {
            return size.GetFiles().Sum(fi => fi.Length) + size.GetDirectories().Sum(di => DirSize(di));
        }

        public void ClearData(DirectoryInfo data)
        {
            foreach(FileInfo file in data.GetFiles())
            {
                
                
                try

                {

                    file.Delete();
                    Console.WriteLine($"{file.FullName}");
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
            foreach (DirectoryInfo dat in data.GetDirectories())
            {


                try

                {

                    dat.Delete(true);
                    Console.WriteLine($"{dat.FullName}");
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

       

      
        
        private void btn_reinigen_Click(object sender, RoutedEventArgs e)
        {

           
            Clipboard.Clear();
            
            try
            {
                ClearData(winTemp);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erooor {ex.Message}");
            }
            try
            {
                ClearData(appTemp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erooor {ex.Message}");
            }
            titel.Content = "Reinigen Successful";
            Platz.Content = "0 Mb";

        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Die Software ist auf dem neuen Stand !", "Update",MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btn_verlauf_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void btn_website_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("http://www.google.com")
                {
                    UseShellExecute = true
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Eroor {ex.Message}");
            }
          
           
        }

        private void btn_analyse_Click(object sender, RoutedEventArgs e)
        {
            Analysefolders();
            btn_analyse.Content = "anlalyse erfolgreich";
        }

        public void Analysefolders()
        {
            Console.WriteLine("Beginn der Analyse.....");
            long totalsize = 0;
            try
            {
                totalsize += DirSize(winTemp) / 1000000;
                totalsize += DirSize(appTemp) / 1000000;

            }
            catch(Exception ex)
            {
                Console.WriteLine($" Die Analys ist nicht möglich  {ex.Message}");
            }


            Platz.Content = totalsize + "MB";
            Datum.Content = DateTime.Today;
            savedate();
        }
        public void savedate()
        {
            string date = DateTime.Today.ToString();
            File.WriteAllText("date.txt", date);
        }
        public void recupdate()
        {
            string datefichier = File.ReadAllText("date.txt");
            
        }
    }
}
