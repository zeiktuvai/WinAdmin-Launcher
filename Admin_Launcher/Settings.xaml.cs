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
using System.Windows.Automation;
using Newtonsoft.Json;

namespace Admin_Launcher
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            string uname = Properties.Settings.Default["AdminUsrName"].ToString();
            string upass = Properties.Settings.Default["AdminUsrPass"].ToString();

            if (uname != "" && upass != "")
            {
                tbxUsername.Text = uname;
                tbxPass.Password = security.Unprotect(upass);
            }
        }

        private void Psswrd_Click(object sender, RoutedEventArgs e)
        {

            if (tbxPass.Password != "" && tbxUsername.Text != "")
            {
                Properties.Settings.Default["AdminUsrPass"] = security.Protect(tbxPass.Password);
                Properties.Settings.Default["AdminUsrName"] = tbxUsername.Text;
                Properties.Settings.Default.Save();
                lblSaved.Visibility = Visibility.Visible;
            }
            

        }



        public static List<AdminApp> GetAppList()
        {
            List<AdminApp> RetrievedApps = new List<AdminApp>();
            string JSONStr = Properties.Settings.Default["ApplicationList"].ToString();

            if (JSONStr != "")
            {
                RetrievedApps = JsonConvert.DeserializeObject<List<AdminApp>>(JSONStr);
            }

            return RetrievedApps;
        }
    }
}
