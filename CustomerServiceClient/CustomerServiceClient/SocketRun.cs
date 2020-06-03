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
		public static string name;
		public static void SocketCreate()
		{
			loadConfig();
			client = new TcpClient();
			client.Connect("127.0.0.1", PORT_NUMBER);
			stream = client.GetStream();

			Thread thr = new Thread(getData);
			thr.Start();
			sendData("login");
		}
		public static void SocketClose()
		{
			//loadConfig();
			client.Close();		
		}
		private static void getData()
		{
			while (true)
			{
				try
				{
					BinaryReader reader = new BinaryReader(stream);
					Delegate a = new Action<String>(Client.processData);
					fm.Invoke(a, reader.ReadString());
				}
				catch (Exception)
				{
					stream.Close();
					client.Close();
				}
			}

		}
		public static void sendData(string method)
		{			
			BinaryWriter writer = new BinaryWriter(stream);
			writer.Write(method+"|"+id);
			//writer.Close();
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
				catch (Exception ) { }
				try
				{
					name = node.SelectSingleNode("name").InnerText;
				}
				catch (Exception ) { }
				try
					{
						name = node.SelectSingleNode("gate").InnerText;
				}
				catch (Exception ) { }
			}
		}
	}
}
