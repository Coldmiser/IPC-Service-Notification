using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using System.Net;

namespace MonitorService
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timer = new System.Timers.Timer(); // name space(using System.Timers;)  

        public string MyURL { get; private set; }

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteLog();
            WriteLog("Starting Service");
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60 * 1000; //number in milisecinds  
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            WriteLog("Stopping Service");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteLog("Service is running");
            WriteLog(DownloadWebpage("https://www.qrz.com/db/Wo0den"));
        }
        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
                WriteLog("TODO:  This section isn't running");
            }
        }
#region WriteLog
        public int WriteLog(String Status)
        {
            System.IO.File.AppendAllText(@"C:\Temp\log.txt", string.Concat(Status, "  ", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), System.Environment.NewLine));
            return 0;
        }
        public int WriteLog()
        {
            System.IO.File.AppendAllText(@"C:\Temp\log.txt", System.Environment.NewLine);
            return 0;
        }
        #endregion
        public String DownloadWebpage(String URL)
        {
            // https://stackoverflow.com/questions/599275/how-can-i-download-html-source-in-c-sharp
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                string MyURL = client.DownloadString(URL);
            }
            string hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = BitConverter.ToString(
                  md5.ComputeHash(Encoding.UTF8.GetBytes(MyURL))
                ).Replace("-", String.Empty);
            }
            return hash;
        }
    }
}
