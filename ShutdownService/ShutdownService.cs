using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using lafe.ServiceBase.Interface;
using lafe.ShutdownService.ServiceLibrary;

namespace ShutdownService
{
    public partial class ShutdownService : ServiceBase
    {
        IService Service { get; set; }

        public ShutdownService()
        {
            InitializeComponent();
            Service = new Service();
        }

        protected override void OnStart(string[] args)
        {
            Service.OnStart(args);
        }

        protected override void OnStop()
        {
            Service.OnStop();
        }

        protected override void OnContinue()
        {
            Service.OnContinue();
        }

        protected override void OnPause()
        {
            Service.OnPause();
        }
    }
}
