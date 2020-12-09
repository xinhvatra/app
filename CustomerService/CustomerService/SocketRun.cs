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
		const int PORT_NUMBER_CLIENT = 9997;
		public static TcpClient client;
		public static TcpListener listener;
		public static Socket clientSocket;
		public static Form fm;
		
		[Obsolete]
		public static void SocketCreate()
		{			
			listener = new TcpListener(PORT_NUMBER);
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
		public static string ip;
		public static void ListenSocket()
		{
			while (true)
			{
				clientSocket = listener.AcceptSocket();
				ip = ((IPEndPoint)(clientSocket.RemoteEndPoint)).Address.ToString(); 
				//MessageBox.Show("get data from client :" + Function.ip);
				Thread t = new Thread((obj) =>
				{
					getData();
				});
				t.Start();
			}
		}
		public static void encoding(String utf8String)
		{
			byte[] outbyte = new byte[1024];
			BufferedStream bfStream = new BufferedStream(new NetworkStream(clientSocket));
			bfStream.Write(Encoding.UTF8.GetBytes(utf8String), 0, Encoding.UTF8.GetBytes(utf8String).Length);
			bfStream.Flush();
		}
		public static void sendData(string method, int client_id, int service_id, string client_name, int gate, int customer_id, string data)
		{
			try
			{
				//var netStream = new NetworkStream(clientSocket);
				//BinaryWriter writer = new BinaryWriter(netStream);
				//writer.AutoFlush = true;
				//	MessageBox.Show(method + "|" + client_id + "|" + service_id + "|" + client_name + "|" + gate + "|" + customer_id + data);
				encoding(method + "|" + client_id + "|" + service_id + "|" + client_name + "|" + gate + "|" + customer_id+ data);
				//writer.Close();
			}
			catch (Exception) { }
		}
		
		private static void getData()
		{
			try
			{
				
				Byte[] inputByte = new Byte[1024];
				BufferedStream strd = new BufferedStream(new NetworkStream(clientSocket));
				int read = strd.Read(inputByte, 0, inputByte.Length);
				string processStr = Encoding.UTF8.GetString(inputByte,0, read);

				
				Delegate a = new Action<String>(Main.processData);
				fm.Invoke(a, processStr);
				strd.Close();
			}
			catch (Exception)
			{

			}
		}

		
		public static void connectClient(string ipClient, string send_name,int customer_id)
		{
			try
			{
				client = new TcpClient();
				client.Connect(ipClient, PORT_NUMBER_CLIENT);
				NetworkStream stream = client.GetStream();
				BinaryWriter writer = new BinaryWriter(stream);
				writer.Write("forcecustomer" + "|" + send_name + "|" + customer_id);
				//MessageBox.Show("send client "+ipClient + "forcecustomer" + "|" + send_name + "|" + customer_id);
				//stream.Close();
			}
			catch (Exception)
			{
			}
		}
	}
}
