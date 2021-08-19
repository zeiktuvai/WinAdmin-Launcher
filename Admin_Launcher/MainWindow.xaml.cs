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

namespace Admin_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            lvMenu.SelectedIndex = 0;


        }

        private void LvMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string target = ((sender as ListView).SelectedItem as ListViewItem).Name.ToString();

            switch (target)
            {
                case "lstLaunch":
                    mainFrme.Navigate(new System.Uri("Launch.xaml", UriKind.Relative));
                    break;

                case "lstSettings":
                    mainFrme.Navigate(new System.Uri("Settings.xaml", UriKind.Relative));
                    break;
            }
            
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
