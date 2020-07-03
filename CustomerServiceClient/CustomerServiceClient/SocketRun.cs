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
		private const int PORT_NUMBER_CLIENT = 9997;
		private static TcpClient client;
		private static Stream stream;
		public static Form fm;
		public static int client_id, android;
		public static string ipcas, name, gate, ipServer, ipAndroid;
		public static void SocketCreate()
		{
			loadConfig();
			connect();
			sendData("login", 0);
			listenServer();
			if (android == 1)
			{
				connectAndroid();
			}
		}
		public static void connect()
		{
			try
			{
				client = new TcpClient();
				client.Connect(ipServer, PORT_NUMBER);
				stream = client.GetStream();
			}
			catch (Exception)
			{
				MessageBox.Show("Không kết nối được máy chủ. Vui lòng kiểm tra lại!");
				Application.Exit();
			}
		}

		static TcpClient clientAndroid;
		public static void connectAndroid()
		{
			try
			{
				clientAndroid = new TcpClient();
				clientAndroid.Connect(ipAndroid, PORT_NUMBER_ANDROID);
			}
			catch (Exception)
			{
				MessageBox.Show("Không kết nối được cổng Android. Vui lòng kiểm tra lại!");
				Application.Exit();
			}
			Thread t = new Thread((obj) =>
			{
				ListenAndroid();
			});
			t.Start();

		}
		public static void ListenAndroid()
		{

			streamAndroid = clientAndroid.GetStream();
			//processDataAndroid();


		}
		//=================================Listen server pass customer=========================================================/
		static TcpListener listenerServer;
		static Socket serverClient;
		static NetworkStream streamServer;
		static NetworkStream streamAndroid;
		public static void listenServer()
		{
			string ipadress = Dns.GetHostEntry(Dns.GetHostName())
.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
.ToString();
			IPAddress address = IPAddress.Parse(ipadress);
			listenerServer = new TcpListener(address, PORT_NUMBER_CLIENT);
			listenerServer.Start();

			Thread t = new Thread((obj) =>
			{

				ListenServer();

			});
			t.Start();

		}

		public static void ListenServer()
		{
			while (true)
			{
				serverClient = listenerServer.AcceptSocket();

				Thread thr = new Thread(getDataServer);
				thr.Start();
				//getDataServer();
			}
		}
		private static void getDataServer()
		{
			streamServer = new NetworkStream(serverClient);
			BinaryReader reader = new BinaryReader(streamServer);
			string processStr = reader.ReadString();			
			Delegate a = new Action<String>(Client.processDataAsync);
			fm.Invoke(a, processStr);
			stream.Close();

		}
		//=================================End=========================================================/

		public static void processDataAndroid()
		{
			//getDataAndroid();
			//sendDataAndroid(gate + "," + "0");
		}

		public static void getDataAndroid()
		{

			StreamReader reader = new StreamReader(streamAndroid);
			string line;
			line = reader.ReadLine();
		}
		public static void sendDataAndroid(String data)
		{
			try
			{
				StreamWriter writer = new StreamWriter(streamAndroid);
				writer.WriteLine(data);
				//MessageBox.Show("send data to android: ");
				writer.Flush();
				//MessageBox.Show("co khach: "+data);
			}
			catch (Exception)
			{
				MessageBox.Show("Không kết nối được cổng Android. Vui lòng kiểm tra lại!");
				Application.Exit();
			}

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
			Delegate a = new Action<String>(Client.processDataAsync);
			fm.Invoke(a, processStr);
			stream.Close();

		}

		public static void sendData(string method, int service)
		{
			BinaryWriter writer = new BinaryWriter(stream);
			if (method.Equals("login"))
			{
				writer.Write(method + "|" + ipcas + "|" + service);
			}
			else
			{
				writer.Write(method + "|" + client_id + "|" + service);
			}

			if (!method.Equals("logout"))
			{
				Thread thr = new Thread(getData);
				thr.Start();
			}

		}
		public static void sendDataSwitch(string method, int service_id, int receive_id, int customer)
		{
			BinaryWriter writer = new BinaryWriter(stream);
			writer.Write(method + "|" + client_id + "|" + service_id + "|" + receive_id + "|" + customer + "|" + gate);
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
					ipcas = (node.SelectSingleNode("id").InnerText);
				}
				catch (Exception) { }			
				try
				{
					ipServer = node.SelectSingleNode("ipServer").InnerText;
				}
				catch (Exception) { }
				try
				{
					ipAndroid = (node.SelectSingleNode("ipAndroid").InnerText);
					//MessageBox.Show(ipAndroid);
				}
				catch (Exception) { }
				try
				{
					android = Convert.ToInt32(node.SelectSingleNode("androidConnect").InnerText);
				}
				catch (Exception) { }

			}
		}
	}
}
