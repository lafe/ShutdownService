using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using lafe.Logging.Interface;
using lafe.ServiceBase.Interface;
using lafe.ShutdownService.Monitoring;
using lafe.ShutdownService.Monitoring.Interface;
using Ninject;

namespace lafe.ShutdownService.ServiceLibrary
{
    public class Service : IService
    {
        protected IKernel Kernel { get; private set; }

        protected ILog Log { get; private set; }
        protected IMonitoringTimer MonitoringTimer { get; private set; }

        protected string[] CommandLineArguments { get; private set; }

        public Service()
        {

        }

        public void Initalize()
        {
            Kernel = new StandardKernel();
            Kernel.Load<Binding>();

            Log = Kernel.Get<ILog>();
            MonitoringTimer = Kernel.Get<IMonitoringTimer>();

#if DEBUG
            Log.Warn(LogNumbers.RunningInDebugMode, "The service has been built in DEBUG mode. No shutdown possible.");
#endif
        }

        public void OnContinue()
        {
            Log.Info(LogNumbers.OnContinue, string.Format("Continuing execution of ShutdownService"));
            Start();
        }

        public void OnPause()
        {
            Log.Info(LogNumbers.OnPause, string.Format("Pausing execution of ShutdownService"));
            Stop();
        }

        public void OnStart(string[] args)
        {
            CommandLineArguments = args;

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += InitalizeService;
            backgroundWorker.RunWorkerCompleted += InitalizationComplete;
            backgroundWorker.RunWorkerAsync();
        }

        //Perform Initalizing asynchron, because Windows services have to be initalized within a short time span.
        public void InitalizeService(object sender, DoWorkEventArgs e)
        {
            if (Kernel == null)
            {
                Initalize();
            }
        }

        void InitalizationComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Log.Info(LogNumbers.OnStart, string.Format("Starting ShutdownService"));
            Start();
        }


        public void OnStop()
        {
            Log.Info(LogNumbers.OnStop, string.Format("Stopping ShutdownService"));
            Stop();
        }

        protected void Start()
        {
            Log.Trace(LogNumbers.StartingMonitor, "Starting Monitor");
            MonitoringTimer.StartMonitoring();
        }

        protected void Stop()
        {
            Log.Trace(LogNumbers.StoppingMonitor, "Stopping Monitor");
            MonitoringTimer.StopMonitoring();
        }
    }
}
