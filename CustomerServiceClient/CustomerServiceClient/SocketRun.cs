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
		public static int id,android;
		public static string name, gate, ipServer,ipAndroid;
		public static void SocketCreate()
		{
			loadConfig();
			connect();
			sendData("login",0);
			//if (android == 1)
			//{
			//	connectAndroid1();
			//}
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
		static TcpListener listener;
		static TcpClient cl;
		public static void connectAndroid1()
		{
			try
			{
				cl = new TcpClient();
				cl.Connect(ipAndroid, PORT_NUMBER_ANDROID);
			}
			catch (Exception)
			{
				MessageBox.Show("Không kết nối được máy chủ Android. Vui lòng kiểm tra lại!");
				Application.Exit();
			}
			Thread t = new Thread((obj) =>
			{
				ListenAndroid1();
			});
			t.Start();

		}
		public static void ListenAndroid1()
		{
			
			nt = cl.GetStream();
			//processDataAndroid();


		}
		public static void connectAndroid()
		{
			IPAddress address = IPAddress.Parse("192.168.1.35");
			listener = new TcpListener(address, PORT_NUMBER_ANDROID);
			listener.Start();

			Thread t = new Thread((obj) =>
			{
				while (true)
				{
					ListenAndroid();
				}
			});
			t.Start();

		}
		static TcpClient clientSocket;
		static NetworkStream nt;
		public static void ListenAndroid()
		{

			clientSocket = listener.AcceptTcpClient();
			clientSocket.NoDelay = true;
			nt = clientSocket.GetStream();
			//processDataAndroid();

		}
		public static void processDataAndroid()
		{
			//getDataAndroid();
			//sendDataAndroid(gate + "," + "0");
		}

		public static void getDataAndroid()
		{

			StreamReader reader = new StreamReader(nt);
			string line;
			line = reader.ReadLine();
		}
		public static void sendDataAndroid(String data)
		{
			try
			{
				StreamWriter writer = new StreamWriter(nt);
				writer.WriteLine(data);
				//MessageBox.Show("send data to android: ");
				writer.Flush();
				//MessageBox.Show("co khach: "+data);
			} catch (Exception e)
			{
				MessageBox.Show("Không kết nối được máy chủ Android. Vui lòng kiểm tra lại!");
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
			Delegate a = new Action<String>(Client.processData);
			fm.Invoke(a, processStr);
			stream.Close();
			
		}
		public static void sendData(string method,int service)
		{
			BinaryWriter writer = new BinaryWriter(stream);		
			writer.Write(method + "|" + id+"|"+service);
			//MessageBox.Show("client " + id + " gui tin nhan den server: " + method + "|" + id + "|" + service);
			if (!method.Equals("logout"))
			{
				//MessageBox.Show("not logout");
				Thread thr = new Thread(getData);
				thr.Start();
			}
			
		}
		public static void sendDataSwitch(string method, int receive_id,int customer)
		{
			BinaryWriter writer = new BinaryWriter(stream);
			writer.Write(method + "|" + id + "|" + service);
			//MessageBox.Show("client " + id + " gui tin nhan den server: " + method + "|" + id + "|" + service);
			if (!method.Equals("logout"))
			{
				//MessageBox.Show("not logout");
				Thread thr = new Thread(getData);
				thr.Start();
			}

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
