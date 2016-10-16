using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Xna.Framework;

namespace MusicVideosHubIntegration
{
    public class XNADispatcherService : IApplicationService
    {
        private DispatcherTimer frameworkDispatcherTimer;

        public void StartService(ApplicationServiceContext context)
        {
            this.frameworkDispatcherTimer.Start();
        }

        public void StopService()
        {
            this.frameworkDispatcherTimer.Stop();
        }

        public XNADispatcherService()
        {
            this.frameworkDispatcherTimer = new DispatcherTimer();
            this.frameworkDispatcherTimer.Interval = TimeSpan.FromTicks(333333);
            this.frameworkDispatcherTimer.Tick += frameworkDispatcherTimer_Tick;
            FrameworkDispatcher.Update();
        }

        void frameworkDispatcherTimer_Tick(object sender, EventArgs e) { FrameworkDispatcher.Update(); }
    }
}
