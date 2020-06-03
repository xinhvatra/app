using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerService
{
	class SocketRun


	{
		static List<TcpClient> clients = new List<TcpClient>();		
		const int MAX_CONNECTION = 500;
		const int PORT_NUMBER = 9999;
		public static TcpListener listener;
		public static TcpClient clientSocket;
		public static Form fm;
		public static void SocketCreate()
		{

			IPAddress address = IPAddress.Parse("127.0.0.1");

			listener = new TcpListener(address, PORT_NUMBER);

			listener.Start();

			for (int i = 0; i < MAX_CONNECTION; i++)
			{
				new Thread(ListenSocket).Start();
			}
			

		}
		public static void ListenSocket()
		{
			while (true)
			{
				//soc = listener.AcceptSocket();
				clientSocket = listener.AcceptTcpClient();
				clients.Add(clientSocket);
				Thread t = new Thread(getData);
				t.Start();
			}
		}

		public static void sendData(string method, int client_id, int service_id,string client_name, int gate, int customer_id)
		{
			try
			{
				BinaryWriter writer = new BinaryWriter(clientSocket.GetStream());
				writer.Write(method + "|" + client_id + "|" + service_id + "|" + client_name + "|" + gate + "|" + customer_id);
				writer.Close();
			}
			catch (Exception) { }
			}
		private static void getData()
		{
			//while (true)
			//{
				try
				{
					BinaryReader reader = new BinaryReader(clientSocket.GetStream());
					Delegate a = new Action<String>(Main.processData);
					fm.Invoke(a, reader.ReadString());
				}
				catch (Exception)
				{
					clientSocket.Close();
				}

			//}

		}
		
	}
}
