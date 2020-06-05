using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		private static TcpClient client;
		private static Stream stream;
		public static Form fm;
		public static int id;
		public static string name,gate,ip;
		public static void SocketCreate()
		{
			loadConfig();
			connect();
			sendData("login");

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
