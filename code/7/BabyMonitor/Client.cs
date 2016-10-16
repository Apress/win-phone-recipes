using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

namespace BabyMonitor
{
    public class Client
    {
        private int _port = 13001;
        static ManualResetEvent clientDone = new ManualResetEvent(false);
        static MemoryStream dataIn = null;

        private string _serverName = string.Empty;

        public Client(string serverName, int portNumber)
        {
            _serverName = serverName;
            _port = portNumber;
        }

        public void SendData(MemoryStream data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            dataIn = data;

            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();

            DnsEndPoint hostEntry = new DnsEndPoint(_serverName, _port);

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(SocketEventArg_Completed);
            socketEventArg.RemoteEndPoint = hostEntry;

            socketEventArg.UserToken = sock;

            try
            {
                sock.ConnectAsync(socketEventArg);
            }
            catch (SocketException ex)
            {
                throw new SocketException((int)ex.ErrorCode);
            }
        }

        void SocketEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
            }
        }

        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Socket sock = e.UserToken as Socket;

                sock.ReceiveAsync(e);
            }
            else
            {
                clientDone.Set();
            }
        }

        private void ProcessConnect(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // Successfully connected to the server 
                // Send data to the server 
                byte[] buffer = dataIn.ToArray();
                e.SetBuffer(buffer, 0, buffer.Length);
                Socket sock = e.UserToken as Socket;
                sock.SendAsync(e);
            }
            else
            {
                clientDone.Set();
            }
        }
    }
}
