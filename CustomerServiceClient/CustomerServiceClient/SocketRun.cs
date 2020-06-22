using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CustomerServiceClient
{
	class SocketRun
	{

		private const int PORT_NUMBER = 9999;
		private const int PORT_NUMBER_ANDROID = 9998;
		private static TcpClient client;
		private static Stream stream;
		public static Form fm;
		public static int id;
		public static string name, gate, ip;
		public static void SocketCreate()
		{
			loadConfig();
			//connect();
			//sendData("login");
			connectAndroid();
		}
		public static void connect()
		{
			try
			{
				client = new TcpClient();
				client.Connect(ip, PORT_NUMBER);
				stream = client.GetStream();
			}
			catch (Exception)
			{
				MessageBox.Show("Không kết nối được máy chủ. Vui lòng kiểm tra lại!");
				Application.Exit();
			}
		}
		static TcpListener listener;
		static TcpClient cl;		
		public static void connectAndroid1()
		{
			cl = new TcpClient();
			cl.Connect("192.168.232.2", PORT_NUMBER_ANDROID);
			
			Thread t = new Thread((obj) =>
			{
				ListenAndroid();
			});
			t.Start();

		}
		public static void ListenAndroid1()
		{
			//while (true)
			//{
			using (Stream st = cl.GetStream())
			
			{
				using (StreamReader reader = new StreamReader(st))
				{
					string line;

					try
					{
						line = reader.ReadLine();
						MessageBox.Show("nhan duoc tin nhan tu android server: " + line);
					}
					catch (IOException)
					{

					}
					

				}//using reader
				
			}//using ns

			
			StreamWriter writer = new StreamWriter(cl.GetStream());
			//writer.AutoFlush = true;
			writer.Write("gate******************************");
			writer.Flush();
			
			
		}
		public static void connectAndroid()
		{
			IPAddress address = IPAddress.Parse("127.0.0.1");
			listener = new TcpListener(address, PORT_NUMBER_ANDROID);
			listener.Start();

			Thread t = new Thread((obj) =>
			{
				ListenAndroid();
			});
			t.Start();

		}
		static Socket clientSocket;
		public static void ListenAndroid()
		{
			
				clientSocket = listener.AcceptSocket();
				clientSocket.NoDelay = true;
				send();
				
		}
		public static void send()
		{	
				NetworkStream nt = new NetworkStream(clientSocket);

				StreamReader reader = new StreamReader(nt);

				string line;

				line = reader.ReadLine();
			
				reader.Close();
				nt.Close();
			//	MessageBox.Show("nhan duoc tin nhan tu android client: " + line);
			NetworkStream nt1 = new NetworkStream(clientSocket);
			StreamWriter writer = new StreamWriter(nt1);
				//writer.AutoFlush = true;
				writer.Write("gate******************************");
				writer.Flush();
				writer.Close();			
				nt1.Close();
			
		}
		public static void SocketClose()
		{
			//loadConfig();
			client.Close();
		}
		private static void getData()
		{
			BinaryReader reader = new BinaryReader(stream);
			string processStr = reader.ReadString();
			//MessageBox.Show("client "+id+" nhan duoc tin nhan tu server: " + processStr);
			Delegate a = new Action<String>(Client.processData);
			fm.Invoke(a, processStr);
			stream.Close();
		}
		public static void sendData(string method)
		{
			BinaryWriter writer = new BinaryWriter(stream);
			//writer.AutoFlush = true;
			writer.Write(method + "|" + id);

			Thread thr = new Thread(getData);
			thr.Start();
		}
		public static void loadConfig()
		{
			XmlDocument xd = new XmlDocument();
			xd.Load("config.xml");

			XmlNodeList nodelist = xd.SelectNodes("/config");

			foreach (XmlNode node in nodelist)
			{
				try
				{
					id = Convert.ToInt32(node.SelectSingleNode("id").InnerText);
				}
				catch (Exception) { }
				try
				{
					name = node.SelectSingleNode("name").InnerText;
				}
				catch (Exception) { }
				try
				{
					gate = node.SelectSingleNode("gate").InnerText;
				}
				catch (Exception) { }
				try
				{
					ip = node.SelectSingleNode("ip").InnerText;
				}
				catch (Exception) { }
			}
		}
	}
}
