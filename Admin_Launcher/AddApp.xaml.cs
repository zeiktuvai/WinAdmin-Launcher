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
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;
using Application = System.Windows.Application;

namespace Admin_Launcher
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddApp : Window
    {
        public AddApp()
        {
            InitializeComponent();
        }

        private void BttnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbxIcn.Text != "" && tbxPath.Text != "")
            {
                string appPath = tbxPath.Text;
                string iconPath = tbxIcn.Text;
                List<AdminApp> listApps = Settings.GetAppList();

                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(appPath);

                listApps.Add(new AdminApp() { AppName = fileInfo.ProductName, AppVer = fileInfo.ProductVersion, AppStartPath = fileInfo.FileName, AppImagePath = iconPath });

                string sjson = JsonConvert.SerializeObject(listApps);

                Properties.Settings.Default["ApplicationList"] = sjson;
                Properties.Settings.Default.Save();
            }

            //myinfo = FileVersionInfo.GetVersionInfo("C:\\Windows\\System32\\ServerManager.exe");
            //applist.Add(new AdminApp() { AppName = myinfo.ProductName, AppVer = myinfo.ProductVersion, AppStartPath = myinfo.FileName, AppImagePath = myinfo.FileName });

            //myinfo = FileVersionInfo.GetVersionInfo("C:\\Program Files\\Microsoft Data Protection Manager\\Common\\Launcher.exe");
            //applist.Add(new AdminApp() { AppName = myinfo.ProductName, AppVer = myinfo.ProductVersion, AppStartPath = myinfo.FileName, AppImagePath = myinfo.FileName });

            this.Close();
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            window.mainFrme.Source = new Uri("Launch.xaml", UriKind.Relative);

        }

        private void BttnChoose_Click(object sender, RoutedEventArgs e)
        {
            var filed = new OpenFileDialog();
            var res = filed.ShowDialog();
            switch (res)
            {
                case System.Windows.Forms.DialogResult.OK:
                    tbxPath.Text = filed.FileName;
                    tbxIcn.Text = filed.FileName;
                    var fileinfo = FileVersionInfo.GetVersionInfo(filed.FileName);
                    imgIcnSample.Source = Launch.GetIcon(filed.FileName);
                    lblApp.Text = "Adding " + fileinfo.ProductName;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    break;
            }
        }

        private void BttnIcon_Click(object sender, RoutedEventArgs e)
        {
            var filed = new OpenFileDialog();
            var res = filed.ShowDialog();
            switch (res)
            {
                case System.Windows.Forms.DialogResult.OK:
                    tbxIcn.Text = filed.FileName;
                    var fileinfo = FileVersionInfo.GetVersionInfo(filed.FileName);
                    imgIcnSample.Source = Launch.GetIcon(filed.FileName);
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    break;
            }
        }
    }
}
