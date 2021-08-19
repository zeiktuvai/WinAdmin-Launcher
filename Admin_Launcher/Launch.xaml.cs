using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
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
using System.Drawing;
using System.Windows.Interop;
using System.IO;
using Newtonsoft.Json;

namespace Admin_Launcher
{
    /// <summary>
    /// Interaction logic for Launch.xaml
    /// </summary>
    public partial class Launch : Page
    {
        List<AdminApp> applist = new List<AdminApp>();
        public static List<AdminApp> ProcApplist = new List<AdminApp>();
        public static SecureString pass = new SecureString();
        public static string domain;
        public static string userName;
        public Launch()
        {
            InitializeComponent();

            if (Properties.Settings.Default["AdminUsrPass"].ToString() != "")
            {
               pass = new System.Net.NetworkCredential("", security.Unprotect(Properties.Settings.Default["AdminUsrPass"].ToString()).ToString()).SecurePassword;
            }

            if (Properties.Settings.Default["AdminUsrName"].ToString() != "")
            {
                string userString = Properties.Settings.Default["AdminUsrName"].ToString();
                domain = userString.Substring(0, userString.IndexOf('\\'));
                userName = userString.Substring(userString.IndexOf('\\') + 1);
                lblWarn.Visibility = Visibility.Collapsed;
            } else
            {
                lblWarn.Visibility = Visibility.Visible;
            }
            

            ProcApplist = Settings.GetAppList();
            appList.ItemsSource = ProcessAppList(ProcApplist);
        }

        public static ImageSource GetIcon(string fileName)
        {
            try
            {
                Icon icon = Icon.ExtractAssociatedIcon(fileName);
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                            icon.Handle,
                            new Int32Rect(0,0,icon.Width, icon.Height),
                            BitmapSizeOptions.FromEmptyOptions());
            }
            catch (FileNotFoundException)
            {
                Icon xicon = Icon.ExtractAssociatedIcon("C:\\Windows\\System32\\ieframe.dll");
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    xicon.Handle,
                    new Int32Rect(0,0,xicon.Width, xicon.Height),
                    BitmapSizeOptions.FromEmptyOptions());
            }
        }


       private void BttnLaunch_Click(object sender, RoutedEventArgs e)
        {
            if (lblWarn.Visibility == Visibility.Collapsed)
            {
                Process.Start((((sender as Button).DataContext) as AdminApp).AppStartPath, userName, pass, domain);
            } else
            {
                MessageBox.Show("Please set admin user and password!", "Missing Account Information", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                window.mainFrme.Source = new Uri("Settings.xaml", UriKind.Relative);
            }
        }

        private void BttnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddApp ap = new AddApp();
            
            ap.ShowDialog();
        }


        private List<AdminApp> ProcessAppList(List<AdminApp> list)
        {
            List<AdminApp> returnList = list;

            foreach (var item in returnList)
            {
                item.AppImage = (InteropBitmap)GetIcon(item.AppImagePath);
            }

            return returnList;
        }

        private void BttnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (appList.SelectedItem != null)
            {
                applist = Settings.GetAppList();
                AdminApp selectedApp = (AdminApp)appList.SelectedItem;
                selectedApp.AppImage = null;
                var resp = MessageBox.Show("Are you sure you wish to delte " + selectedApp.AppName + "?", "Delete App?", MessageBoxButton.YesNo);
                if (resp == System.Windows.MessageBoxResult.Yes)
                {
                    applist.RemoveAt(applist.FindIndex(x => x.AppName == selectedApp.AppName));
                    string sjson = JsonConvert.SerializeObject(applist);
                    Properties.Settings.Default["ApplicationList"] = sjson;
                    Properties.Settings.Default.Save();
                    MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    window.mainFrme.Source = new Uri("Launch.xaml", UriKind.Relative);
                }
            } else
            {
                MessageBox.Show("Please select an item to remove.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BttnMini_Click(object sender, RoutedEventArgs e)
        {
            if (lblWarn.Visibility == Visibility.Collapsed)
            {
                minibar bar = new minibar();
                bar.Show();
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            } else
            {
                MessageBox.Show("Please set admin user and password before opening the launcher!", "Missing Account Information", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                window.mainFrme.Source = new Uri("Settings.xaml", UriKind.Relative);
            }
        }
    }
}
