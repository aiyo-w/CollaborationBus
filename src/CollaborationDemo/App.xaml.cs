﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            Connector connector = new Connector("192.168.8.143", 9898);
            connector.SendMessage("Hello");

            //var msg = Encoding.UTF8.GetString(s);
            //Debug.WriteLine("receive {0}", msg);

            //var mainWindow = new MainWindow();
            //mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
