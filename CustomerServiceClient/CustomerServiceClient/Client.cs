using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CustomerServiceClient
{
	public partial class Client : Form
	{
		public static Label lbTop,lbCenter;
		public static Button bt1,bt2,bt3;
		static bool inGate = false,online=true;
		static int customer;
		//public static string stringTop;
		public Client()
		{
			InitializeComponent();
			this.FormClosing += Client_FormClosing;
			Graphics g = this.CreateGraphics();
			var px = SystemInformation.VirtualScreen.Width / g.DpiX * 72;
			var py = SystemInformation.VirtualScreen.Height / g.DpiY * 72;
			this.Location = new Point(Convert.ToInt32(px) - 10, Convert.ToInt32(py) - 170);
			this.Size = new Size(310, 300);
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
			this.groupBox3.Controls.Add(bt1);

			lbTop = new Label();
			lbTop.Size = new Size(this.Width, 20);
			lbTop.Location = new Point(3, 1);
			lbTop.TextAlign = ContentAlignment.MiddleLeft;
			lbTop.Text = "1000";
			lbTop.Font = new Font("Timesnewroman", 10, FontStyle.Bold);
			lbTop.ForeColor = Color.Blue;
			this.groupBox1.Controls.Add(lbTop);


			lbCenter = new Label();
			lbCenter.AutoSize = true;
			lbCenter.Location = new Point(13, 10);
			lbCenter.TextAlign = ContentAlignment.MiddleCenter;
			//lbCenter.Text = "1000";
			lbCenter.Font = new Font("Timesnewroman", 70, FontStyle.Bold);
			lbCenter.ForeColor = Color.Red;
			//lbCenter.Left = groupBox2.Left + (groupBox2.Width - lbCenter.Width) / 2;
			//lbCenter.Top = groupBox2.Top - 20;
			this.groupBox2.Controls.Add(lbCenter);
			

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
			//button3.BackgroundImage = Properties.Resources.on;
			SocketRun.fm = this;
			SocketRun.SocketCreate();
		}

		private void button1_Click(object sender, EventArgs e)
		{

			if (inGate)
			{
				bt1.Text = "Nhận khách";
				inGate = false;
				if (SocketRun.android == 1)
				{
					SocketRun.sendDataAndroid(SocketRun.gate + ",0");
				}
			}
			else
			{
				SocketRun.connect();
				SocketRun.sendData("data");
			}
		}
		public static void processData(string st)
		{

			//try
			//{
			String[] arrRs = st.Split('|');
			if (arrRs[0] == "login")
			{
				lbTop.Text = arrRs[3] + " - Cổng số " + arrRs[4];
				if (SocketRun.android == 1)
				{
					SocketRun.gate = arrRs[4];
					SocketRun.sendDataAndroid(SocketRun.gate + ",0");
				}
			}
			else if (arrRs[0] == "data")
			{
				customer = Convert.ToInt32(arrRs[5]);
				lbCenter.Text = arrRs[5];
				inGate = true;
				if (SocketRun.android == 1)
				{
					Thread t = new Thread((obj) =>
					{						
						SocketRun.sendDataAndroid(SocketRun.gate + "," + arrRs[5]);
					});
					t.Start();


				}
			}
			else if (arrRs[0] == "call")
			{
				//MessageBox.Show(st);
				customer = Convert.ToInt32(arrRs[5]);
				//bt1.Text = arrRs[5];
				//inGate = true;
				if (SocketRun.android == 1)
				{
					Thread t = new Thread((obj) =>
					{
						Thread.Sleep(30000);
						SocketRun.sendDataAndroid(SocketRun.gate + "," + arrRs[5]);
					});
					t.Start();


				}
			}
			//}
			//catch { }

		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (inGate)
			{
				bt1.Text = "Nhận khách";
				inGate = false;
				if (SocketRun.android == 1)
				{
					SocketRun.sendDataAndroid(SocketRun.gate + ",0");
				}
			}
			else
			{
				SocketRun.connect();
				SocketRun.sendData("data");
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			if (online)
			{
				button1.BackgroundImage = Properties.Resources.on;
				online = false;
				button2.Visible = false;
				button3.Visible = false;
			}
			else
			{
				button1.BackgroundImage = Properties.Resources.off;
				online = true;
				button2.Visible = true;
				button3.Visible = true;
			}
		}
	}
}
