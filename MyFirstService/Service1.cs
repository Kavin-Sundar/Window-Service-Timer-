﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyFirstService
{
   
    public partial class Service1 : ServiceBase
    {

        Timer timer = new Timer(); // name space(using System.Timers;)

        [Obsolete("This method is obsolete. Call CallNewMethod instead.", false)]
        //The Obsolete attribute is read at compile time and it is used to generate a warning or an error to the developer..
        public Service1()
        {
            InitializeComponent();
            WriteToFile("hi");
           

        }
      
        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);

            WriteToFile("hi");
        }
       
        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);
        }

       
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
