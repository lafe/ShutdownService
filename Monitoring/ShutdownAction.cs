using System;
using System.Management;
using lafe.Logging.Interface;
using lafe.ShutdownService.Monitoring.Interface;

namespace lafe.ShutdownService.Monitoring
{
    public class ShutdownAction : IAction
    {
        public ILog Logger { get; private set; }

        public ShutdownAction(ILog logger)
        {
            Logger = logger;
        }

        public void PerformAction()
        {
#if !DEBUG
            Logger.Info(LogNumbers.SystemShuttingDown, "Shutting down system");

            var success = false;
            try
            {
                Logger.Trace(LogNumbers.TryingAlternativeShutdown, "Trying alternative method to shutdown");
                System.Diagnostics.Process.Start("Shutdown", "-s -t 10");
                success = true;
            }
            catch (Exception ex)
            {
                Logger.Error(LogNumbers.ErrorWhilePerformingAlternativeShutdown, ex, string.Format("While performing the alternative method to shutdown, an error occured: {0}", ex));
            }
            
            if (!success)
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        ManagementBaseObject mboShutdown = null;
                        var mcWin32 = new ManagementClass("Win32_OperatingSystem");
                        mcWin32.Get();
                        Logger.Trace(LogNumbers.RetrievedWin32Object, "Retrieved Win32_OperatingSystem");

                        // You can't shutdown without security privileges
                        mcWin32.Scope.Options.EnablePrivileges = true;
                        var mboShutdownParams = mcWin32.GetMethodParameters("Win32Shutdown");
                        Logger.Trace(LogNumbers.SecurityPrivilegesEnabled, "Enabled security privileges");

                        // Flag 1 means we want to shut down the system
                        mboShutdownParams["Flags"] = "1";
                        mboShutdownParams["Reserved"] = "0";
                        Logger.Trace(LogNumbers.SetFlagsToShutdown, "Set flags to shutdown");

                        Logger.Trace(LogNumbers.SendingWin32ShutdownCommand, "Sending Win32Shutdown command to all instances");
                        foreach (ManagementObject manObj in mcWin32.GetInstances())
                        {

                            mboShutdown = manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
                        }
                        Logger.Trace(LogNumbers.Bye, "Bye Bye!");
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(LogNumbers.ErrorWhilePerformingShutdown, ex, string.Format("While performing the shutdown, an error occured: {0}", ex));
                    }
                }
            }
#else
            Logger.Trace(LogNumbers.InDebugMode, "Program is built in Debug-mode. Don't sending command to instances");
#endif
        }

    }
}