using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CollaborationBus;

namespace CollaborationDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Conector conector = new Conector("192.168.8.187", 9898);
            conector.SendMessage("Hello");

            //var mainWindow = new MainWindow();
            //mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
