using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomerServiceClient
{
	public partial class Client : Form
	{
		public static Label lb;
		public static Button bt1;
		static bool inGate = false;
		static int customer;
		//public static string stringTop;
		public Client()
		{
			InitializeComponent();
			this.FormClosing += Client_FormClosing;
			Graphics g = this.CreateGraphics();
			var px = SystemInformation.VirtualScreen.Width / g.DpiX * 72;
			var py = SystemInformation.VirtualScreen.Height / g.DpiY * 72;
			this.Location = new Point(Convert.ToInt32(px) + 50, Convert.ToInt32(py) - 80);
			this.Size = new Size(250, 250);
			g.Dispose();

			bt1 = new Button();
			bt1.Font = new Font("Timesnewroman", 30, FontStyle.Bold);
			bt1.Text = "Nhận khách";
			bt1.Size = new Size(this.Width - 25, this.Height - 70);
			bt1.Location = new Point(5, 25);
			bt1.Click += new EventHandler(button1_Click);
			bt1.ForeColor = Color.Red;
			bt1.BackgroundImage = Properties.Resources.maunen1;
			bt1.MouseHover += new EventHandler(bt1_hover);
			bt1.MouseLeave += new EventHandler(bt1_leave);
			this.Controls.Add(bt1);

			lb = new Label();
			lb.Size = new Size(this.Width, 20);
			lb.Location = new Point(3, 1);
			lb.TextAlign = ContentAlignment.MiddleLeft;
			lb.Text = "1000";
			lb.Font = new Font("Timesnewroman", 10, FontStyle.Bold);
			lb.ForeColor = Color.Blue;
			this.Controls.Add(lb);

		}
		private void bt1_hover(object sender, EventArgs e)
		{
			if (inGate)
			{
				bt1.Text = "Giải phóng khách";
			}
			bt1.ForeColor = Color.Blue;
		}
		private void bt1_leave(object sender, EventArgs e)
		{
			if (inGate)
			{
				bt1.Text = customer.ToString();
			}
			bt1.ForeColor = Color.Red;
		}
		private void Client_FormClosing(object sender, FormClosingEventArgs e)
		{
			SocketRun.SocketClose();
			System.Environment.Exit(1);
		}
		private void Client_Load(object sender, EventArgs e)
		{
			SocketRun.fm = this;
			SocketRun.SocketCreate();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			SocketRun.send();
			//if (inGate)
			//{
			//	bt1.Text = "Nhận khách";
			//	inGate = false;
				
			//}
			//else
			//{
			//	SocketRun.connect();
			//	SocketRun.sendData("data");
			//}
		}
		public static void processData(string st)
		{
			//try
			//{
			String[] arrRs = st.Split('|');
			if (arrRs[0] == "login")
			{
				lb.Text = arrRs[3] + " - Cổng số " + arrRs[4];
			}
			else if (arrRs[0] == "data")
			{
				customer = Convert.ToInt32(arrRs[5]);
				bt1.Text = arrRs[5];
				inGate = true;
			}
			//}
			//catch { }

		}
	}
}
