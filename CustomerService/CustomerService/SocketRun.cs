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
namespace CustomerService
{
	class SocketRun


	{
		static List<TcpClient> clients = new List<TcpClient>();
		static int num = 1000;
		const int MAX_CONNECTION = 10;
		const int PORT_NUMBER = 9999;		
		public static TcpListener listener;
		public static TcpClient clientSocket;
		public static  Socket soc;
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
			}
		}

		public static void sendData(int customer_id, int service_id, int client_id)
		{
			//Socket soc = listener.AcceptSocket();
		//	var stream = new NetworkStream(clientSocket.GetStream());
			//var reader = new StreamReader(stream);
			BinaryWriter writer = new BinaryWriter(clientSocket.GetStream());
			//writer.AutoFlush = true;
			//string id = reader.ReadLine();
			//num++;
			writer.Write("customer_id:" + customer_id + "-service_id:" + service_id+ "-client_id:"+ client_id);
			//stream.Close();
			//sound();
		}
		public static void sound()
		{
			string donvi = num.ToString().Substring(3,1);
			string chuc = num.ToString().Substring(2, 1);
			string tram = num.ToString().Substring(1, 1);
			string nghin = num.ToString().Substring(0, 1);
			Random r = new Random();
			int ra = r.Next(1, 10);
			WMPLib.WindowsMediaPlayer mp = new WMPLib.WindowsMediaPlayer();
			string moi = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\moi.mp3";
			mp.URL = moi;
			mp.controls.play();

			
			Thread.Sleep(1600);

			WMPLib.WindowsMediaPlayer mp1 = new WMPLib.WindowsMediaPlayer();
			string sokhachnghin = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + nghin + ".mp3";
			mp1.URL = sokhachnghin;
			mp1.controls.play();
			Thread.Sleep(700);

			WMPLib.WindowsMediaPlayer mp11 = new WMPLib.WindowsMediaPlayer();
			string sokhachtram = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + tram + ".mp3";
			mp11.URL = sokhachtram;
			mp11.controls.play();
			Thread.Sleep(700);

			WMPLib.WindowsMediaPlayer mp12 = new WMPLib.WindowsMediaPlayer();
			string sokhachchuc = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + chuc + ".mp3";
			mp12.URL = sokhachchuc;
			mp12.controls.play();
			Thread.Sleep(700);

			WMPLib.WindowsMediaPlayer mp13 = new WMPLib.WindowsMediaPlayer();
			string sokhachdonvi = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + donvi + ".mp3";
			mp13.URL = sokhachdonvi;
			mp13.controls.play();
			Thread.Sleep(700);

			WMPLib.WindowsMediaPlayer mp2 = new WMPLib.WindowsMediaPlayer();
			string vaocua = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\cuaso.mp3";
			mp2.URL = vaocua;
			mp2.controls.play();
			Thread.Sleep(1100);

			WMPLib.WindowsMediaPlayer mp3 = new WMPLib.WindowsMediaPlayer();
			string socua = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + ra + ".mp3";

			mp3.URL = socua;
			mp3.controls.play();
		}
	}
}
