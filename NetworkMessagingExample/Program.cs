using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace NetworkMessagingExample
{
	class MainClass
	{
		protected static TcpClient client = new TcpClient();
		protected static readonly int PORT = 8888;

		public static void Main(string[] args)
		{
			Console.WriteLine("Enter the IP Address of the computer you wish to talk to:");
			String IP = Console.ReadLine();

			// Connect to the destination ip address.
			client.Connect(IP, PORT);

			Console.WriteLine("Connecting...");
			while (!client.Connected)
			{
				// wait....
			}

			Console.WriteLine("Client is connected!");

			Console.WriteLine("Enter a message to send.");
			String message = Console.ReadLine();

			byte[] messageData = Encoding.UTF8.GetBytes("^" + message + "$");

			NetworkStream clientStream = client.GetStream();
			clientStream.Write(messageData, 0, messageData.Length);
			clientStream.Flush();

			Console.WriteLine("Message Sent.");

			Console.WriteLine("Awaiting Response...");

			byte[] responseData = new byte[client.ReceiveBufferSize];
			clientStream.Read(responseData, 0, client.ReceiveBufferSize);
			String response = Encoding.UTF8.GetString(responseData);

			Console.WriteLine("Remote sender says: {0}", response);
			Console.ReadKey();
			client.Close();
		}
	}
}
