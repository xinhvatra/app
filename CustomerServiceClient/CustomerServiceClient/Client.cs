using System;

using System.Windows.Forms;

namespace CustomerServiceClient
{
	public partial class Client : Form
	{
		public Client()
		{
			InitializeComponent();
			SocketRun.SocketCreate();
		}
		

		private void Client_Load(object sender, EventArgs e)
		{

			

		}
		
		private void button1_Click(object sender, EventArgs e)
		{

		}
	}
}
