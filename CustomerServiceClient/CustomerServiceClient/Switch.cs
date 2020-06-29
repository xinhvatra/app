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
		{
			comboBox1.Items.Add(Client.dt_service.Rows[0][1]);
		}
	}
}
