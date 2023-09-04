using System;
using Microsoft.AspNetCore.Hosting.Server;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace BarCodeReader.Services
{
	public class TCPListenerClient
	{
        private TcpClient tcpClient;
        private NetworkStream stream;

        public TCPListenerClient()
		{
		}

        public void StartListener(string ip, Int32 port)
		{
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.

                // Prefer a using declaration to ensure the instance is Disposed later.
                tcpClient = new TcpClient(ip, port);

                ListenAsync();
                // Receive the server response.

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            
        }
        public void StopListener(string ip, Int32 port)
        {
            try
            {
                stream.Close();
                tcpClient.Close();

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }


        }

        private void ListenAsync()
        {
            while (tcpClient.Connected)
            {
                // Get a client stream for reading and writing.
                stream  = tcpClient.GetStream();
                var buffer = new byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(buffer, 0, buffer.Length);
                responseData = System.Text.Encoding.ASCII.GetString(buffer, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
            }
        }
    }
}

