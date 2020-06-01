using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerServiceClient
{
	public partial class Client : Form
	{
		public Client()
		{
			InitializeComponent();
		}
		private const int BUFFER_SIZE = 1024;
		private const int PORT_NUMBER = 9999;

		static ASCIIEncoding encoding = new ASCIIEncoding();

		private void Client_Load(object sender, EventArgs e)
		{


			



		}

		private void button1_Click(object sender, EventArgs e)
		{
			//try
			//{
				TcpClient client = new TcpClient();

				// 1. connect
				client.Connect("127.0.0.1", PORT_NUMBER);
				Stream stream = client.GetStream();

				//while (true)
				//{

					var reader = new StreamReader(stream);
					var writer = new StreamWriter(stream);
					writer.AutoFlush = true;

					// 2. send
					writer.WriteLine("1");

					// 3. receive
					string str = reader.ReadLine();
					MessageBox.Show(str);
						
			//	}
				// 4. close
				stream.Close();
				client.Close();
			//}

			//catch (Exception ex)
			//{

			//}
		}
	}
}
