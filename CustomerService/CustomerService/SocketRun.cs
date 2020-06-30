using System;
using System.Collections.Generic;
using System.Data;
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
		public static List<string> clientsList = new List<string>();
		const int MAX_CONNECTION = 100;
		const int PORT_NUMBER = 9999;
		public static TcpListener listener;
		public static Socket clientSocket;
		public static Form fm;
		public static void SocketCreate()
		{
			IPAddress address = IPAddress.Parse("127.0.0.1");
			listener = new TcpListener(address, PORT_NUMBER);
			listener.Start();
			for (int i = 0; i < MAX_CONNECTION; i++)
			{

				Thread t = new Thread((obj) =>
				{
					ListenSocket();
				});
				t.Start();
			}
		}
		public static void ListenSocket()
		{
			while (true)
			{
				clientSocket = listener.AcceptSocket();
				Thread t = new Thread((obj) =>
				{
					getData((Socket)obj);
				});
				t.Start(clientSocket);
			}
		}		
		public static void sendData(string method, int client_id, int service_id, string client_name, int gate, int customer_id, string data)
		{	
			try
			{
				var netStream = new NetworkStream(clientSocket);
				BinaryWriter writer = new BinaryWriter(netStream);
				//writer.AutoFlush = true;
			//	MessageBox.Show(method + "|" + client_id + "|" + service_id + "|" + client_name + "|" + gate + "|" + customer_id + data);
				writer.Write(method + "|" + client_id + "|" + service_id + "|" + client_name + "|" + gate + "|" + customer_id+data);
				//writer.Close();
			}
			catch (Exception) { }
		}
		private static void getData(Socket soc)
		{
			try
			{
				var netStream = new NetworkStream(clientSocket);
				BinaryReader reader = new BinaryReader(netStream);
				string processStr = reader.ReadString();

				Delegate a = new Action<String>(Main.processData);
				fm.Invoke(a, processStr);
				netStream.Close();
			}
			catch (Exception )
			{
				
			}
		}
	}
}
