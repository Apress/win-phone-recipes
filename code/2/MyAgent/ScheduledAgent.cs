#define DEBUG_AGENT

using System.Windows;
using Microsoft.Phone.Scheduler;
using System.IO.IsolatedStorage;
using System;
using Microsoft.Phone.Shell;
using System.IO;

namespace MyAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        public ScheduledAgent()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            bool bRes = false;

            if (task is PeriodicTask)
            {
                bRes = CheckFile();
                if (bRes)
                {
                    ShellToast toast = new ShellToast();
                    toast.Title = "New files arrived";
                    toast.Content = "Processing..."; 
                    toast.Show();
                }
            }
            else
            {
                bRes = LoadFile();
            }

            // If debugging is enabled, launch the agent again in one minute.
#if DEBUG_AGENT
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(60));
#endif
            NotifyComplete();
        }

        private bool CheckFile()
        {
            string destinationFileName = @"\ProcessFile\{0}";
            string dest = string.Empty;

            try
            {
                bool bFound = false;
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    foreach (string filename in file.GetFileNames("*.xml"))
                    {
                        dest = string.Format(destinationFileName, filename);
                        file.MoveFile(filename, dest);
                        bFound = true;
                    }

                    return bFound;
                }
            }
            catch
            {
                Abort();
                return false;
            }
        }

        private bool LoadFile()
        {
            // Do a resourse intensive job here
            return true;
        }
    }
}