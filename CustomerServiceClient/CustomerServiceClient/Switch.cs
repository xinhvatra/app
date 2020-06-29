using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerServiceClient
	
{
	public partial class Switch : Form
	{
		
		public Switch()
		{
			InitializeComponent();
		}

		private void Switch_Load(object sender, EventArgs e)
		{	for (int i = 0; i < Client.dt_service.Length; i++)
			{
				comboBox1.Items.Add(Client.dt_service[i]);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			SocketRun.sendData("switch",0);
		}
	}
}
