using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Admin_Launcher
{
    /// <summary>
    /// Interaction logic for minibar.xaml
    /// </summary>
    public partial class minibar : Window
    {
        public minibar()
        {
            InitializeComponent();
           

            lvApps.ItemsSource = Launch.ProcApplist;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double controlWidth = lvApps.ActualWidth + 20;
            this.Width = controlWidth;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.WorkArea.Bottom;
            double windowWidth = this.Width;
            double middleHorzPos = (screenWidth / 2) - (windowWidth / 2);
            double leftHorzPos = (screenWidth / 4) - (windowWidth / 2);
            double rightHorzPos = ((screenWidth / 4) * 3) - (windowWidth / 2);
            double bottomScrn = screenHeight - (lvApps.ActualHeight + 10);


            //bottom middle
            this.Left = rightHorzPos;
            this.Top = bottomScrn;

        }

        private void BttnLaunch_Click(object sender, RoutedEventArgs e)
        {
               Process.Start((((sender as Button).DataContext) as AdminApp).AppStartPath, Launch.userName, Launch.pass, Launch.domain);
        }
    }
}
