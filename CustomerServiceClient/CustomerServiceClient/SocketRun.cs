﻿using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
		public static string ipcas, gate, serverIp, androidIp;

		[Obsolete]
		public static void SocketCreate()
		{
			loadConfig();
			connect();
			sendData("login", 0);
			listenServer();
		}
		public static void connect()
		{
			try
			{
				client = new TcpClient();
				client.Connect(serverIp, PORT_NUMBER);
				stream = client.GetStream();
			}
			catch (Exception)
			{
				MessageBox.Show("Không kết nối được máy chủ. Vui lòng kiểm tra lại!");
				Application.Exit();
			}
		}
		//====================================android port============================================/
		static TcpClient clientAndroid;
		static NetworkStream streamAndroid;
		public static void androidConnect(string data)
		{
			try
			{
				clientAndroid = new TcpClient();
				clientAndroid.Connect(androidIp, PORT_NUMBER_ANDROID);
				//MessageBox.Show(data + "");
				streamAndroid = clientAndroid.GetStream();
				StreamWriter writer = new StreamWriter(streamAndroid);
				writer.WriteLine(data);
				writer.Flush();
			}
			catch (Exception)
			{
				MessageBox.Show("Không kết nối được cổng Android. Vui lòng kiểm tra lại!");
			}

		}
		//============================================End=======================================================/


		//=================================Listen server pass customer=========================================================/
		static TcpListener listenerServer;
		static Socket serverClient;
		static NetworkStream streamServer;

		[Obsolete]
		public static void listenServer()
		{
			string ipadress = Dns.GetHostEntry(Dns.GetHostName())
.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
.ToString();
			IPAddress address = IPAddress.Parse(ipadress);
			//MessageBox.Show(address + "");
			listenerServer = new TcpListener(PORT_NUMBER_CLIENT);
			listenerServer.Start();

			Thread t = new Thread((obj) =>
			{

				ListenServer();

			});
			t.Start();

		}

		public static void ListenServer()
		{
			//MessageBox.Show("listten");
			while (true)
			{
				serverClient = listenerServer.AcceptSocket();

				Thread t = new Thread((obj) =>
				{
					getDataServer();
				});
				t.Start();
			}
		}
		private static void getDataServer()
		{
			//MessageBox.Show("get data");
			//Byte[] inputByte = new Byte[1024];
			//BufferedStream strd = new BufferedStream(new NetworkStream(serverClient));
			//strd.Read(inputByte, 0, inputByte.Length);
			//string processStr = Encoding.UTF8.GetString(inputByte);

			streamServer = new NetworkStream(serverClient);
			BinaryReader reader = new BinaryReader(streamServer);
			string processStr = reader.ReadString();
			//MessageBox.Show("get data from server : " + processStr);
			Delegate a = new Action<String>(Client.processDataAsync);
			fm.Invoke(a, processStr);
			stream.Close();

		}
		//=================================End=========================================================/




		public static void SocketClose()
		{
			//loadConfig();
			client.Close();
		}
		//get response from server
		private static void getData()
		{

			//BinaryReader reader = new BinaryReader(stream);
			//string processStr = reader.ReadString();
			//MessageBox.Show("client "+id+" nhan duoc tin nhan tu server: " + processStr);
			
			//MessageBox.Show("get data: " + processStr);
			Delegate a = new Action<String>(Client.processDataAsync);
			fm.Invoke(a, decoding());
			stream.Close();

		}
		public static string decoding()
		{
			Byte[] inputByte = new Byte[1024];
			BufferedStream strd = new BufferedStream(stream);
			int read = strd.Read(inputByte, 0, inputByte.Length);
			return Encoding.UTF8.GetString(inputByte,0,read);
		}
		//chyển mã tring sang byte
		public static void encoding(String utf8String)
		{
			byte[] outbyte = new byte[1024];			
			BufferedStream bfStream = new BufferedStream(stream);
			bfStream.Write(Encoding.UTF8.GetBytes(utf8String), 0, Encoding.UTF8.GetBytes(utf8String).Length);
			bfStream.Flush();
		}
		public static void sendData(string method, int service)
		{			
			
			//BinaryWriter writer = new BinaryWriter(stream);
			if (method.Equals("login"))
			{
				encoding(method + "|" + ipcas + "|" + service);
				//writer.Write((method + "|" + ipcas + "|" + service));
			}
			else
			{
				encoding(method + "|" + client_id + "|" + service);
			}

			if (!method.Equals("logout"))
			{
				Thread thr = new Thread(getData);
				thr.Start();
			}

		}
		public static void sendDataSwitch(string method, int service_id, int receive_id, int customer)
		{
			//BinaryWriter writer = new BinaryWriter(stream);
			encoding(method + "|" + client_id + "|" + service_id + "|" + receive_id + "|" + customer + "|" + gate);
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
					serverIp = node.SelectSingleNode("serverIp").InnerText;
				}
				catch (Exception) { }
				try
				{
					androidIp = (node.SelectSingleNode("androidIp").InnerText);
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
