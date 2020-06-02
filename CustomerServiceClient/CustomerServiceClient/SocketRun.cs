using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerServiceClient
{
	class SocketRun
	{
		
		private const int PORT_NUMBER = 9999;
		private static TcpClient client;
		private static Stream stream;
		
		public static void SocketCreate()
		{
			client = new TcpClient();
			client.Connect("127.0.0.1", PORT_NUMBER);
			stream = client.GetStream();
			//while (true)
			//{

			//	BinaryReader reader = new BinaryReader(stream);
				//var writer = new StreamWriter(stream);
				//writer.AutoFlush = true;
				//string str = reader.res();
			//	MessageBox.Show(reader.ReadString());
			//}
		Thread thr = new Thread( new ThreadStart(getData));
			thr.Start();
		}
		private static void getData()
		{
			while (true)
			{
				BinaryReader reader = new BinaryReader(stream);
				//var writer = new StreamWriter(stream);
				//writer.AutoFlush = true;
				//string str = reader.res();
				MessageBox.Show(reader.ReadString());
			}
			//stream.Close();
		}
	}
}
