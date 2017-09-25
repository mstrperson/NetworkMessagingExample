using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace TcpMessageServer
{
	class MainClass
	{
		protected static readonly int PORT = 8888;
		protected static TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), PORT);

		protected static List<TcpClient> clientList = new List<TcpClient>();

		public static void Main(string[] args)
		{
			server.Start();
			Console.WriteLine("Server Started.");

			while (true)
			{
				// This allows the server I'm running in this program to accept your connection
				TcpClient client = server.AcceptTcpClient();

				NetworkStream clientStream = client.GetStream();

				byte[] responseData = new byte[client.ReceiveBufferSize];
				clientStream.Read(responseData, 0, client.ReceiveBufferSize);
				String response = Encoding.UTF8.GetString(responseData);

				Console.WriteLine("New Client Connected.");
				Console.WriteLine(response);
				Console.Write(">  ");

				String message = Console.ReadLine();

				byte[] messageData = Encoding.UTF8.GetBytes("^" + message + "$");
				clientStream.Write(messageData, 0, messageData.Length);

				Console.WriteLine("Sent.");

				clientList.Add(client);
			}
		}
	}
}
