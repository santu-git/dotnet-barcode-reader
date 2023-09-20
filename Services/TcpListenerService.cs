using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.DataProtection;

namespace BarCodeReader.Services
{
    public class TcpListenerService
    {
        private TcpListener _listener;
        private CancellationTokenSource _cancellationTokenSource;

        public TcpListenerService()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void StartListener(int port)
        {
            try
            {

                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                _listener = new TcpListener(ipAddress, port);
                _listener.Start();
                ListenAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void StopListener()
        {
            _cancellationTokenSource.Cancel();
            _listener.Stop();
            Console.WriteLine("TCP listener stopped.");
        }

        private async void ListenAsync()
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    ProcessClient(client);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Listener is Stopped..................");
                    // Handle socket exception, e.g., if the listener is stopped.
                }
            }
        }

        private void ProcessClient(TcpClient client)
        {
            // Handle client connection here.
            // For simplicity, we'll just read data and log it.
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                var data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {data}");
            }

            client.Close();
        }
    }
}

