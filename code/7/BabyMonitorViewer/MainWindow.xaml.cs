using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace BabyMonitorViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Thread signal.
        public ManualResetEvent allDone = new ManualResetEvent(false);
        Thread _tcpServer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _tcpServer = new Thread(new ThreadStart(() =>
            {
                StartListening();}));
            _tcpServer.Start();
        }

        private string StartListening()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 13001);

            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    allDone.Reset();

                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                state.ms.Write(state.buffer, 0, bytesRead);

                if ((state.ms.ToArray()[state.ms.Length - 1] == 217) &&
                    (state.ms.ToArray()[state.ms.Length - 2] == 255))
                {
                    try
                    {
                        state.ms.Seek(0, SeekOrigin.Begin);

                        Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        (ThreadStart)delegate
                        {
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.StreamSource = state.ms;
                            bi.EndInit();
                            imgVideo.Source = new WriteableBitmap(bi);
                        });
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        (ThreadStart)delegate
                        {
                            MessageBox.Show(ex.Message);
                        });
                    }
                }
                else
                {
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _tcpServer.Abort();
        }
    }
}
