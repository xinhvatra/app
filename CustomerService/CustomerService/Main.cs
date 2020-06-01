using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerService
{
	public partial class Main : Form
	{
		public static Button bt1;
		public static Button bt2;
		public static Button bt3;
		public static Button bt4;
		public const int MAX_CONNECTION = 2;
		public const int PORT_NUMBER = 9999;
		public static TcpListener listener;
		public Main()
		{
			InitializeComponent();
			SocketRun.SocketCreate();
			//this.TopMost = true;
			Function.sttKetoan = 999;
			Function.sttDichvu = 10;
			bt1 = new Button();
			bt2 = new Button();
			bt3 = new Button();
			bt4 = new Button();

			bt1.Click += new EventHandler(bt1_click);
			bt1.MouseHover += new EventHandler(bt1_hover);
			bt1.MouseLeave += new EventHandler(bt1_leave);

			bt2.Click += new EventHandler(bt2_click);
			bt2.MouseHover += new EventHandler(bt2_hover);
			bt2.MouseLeave += new EventHandler(bt2_leave);

			bt1.Location = new Point(5, 5);
			bt1.Size = new Size(300, 300);
			bt1.BackgroundImage = CustomerService.Properties.Resources.guitien;
			bt1.BackgroundImageLayout = ImageLayout.Zoom;
			bt1.Text = "Tiền gửi";
			bt1.Font = new Font("Timesnewroman", 30, FontStyle.Italic);
			bt1.ForeColor = Color.Purple;
			bt1.TextAlign = ContentAlignment.BottomCenter;
			this.Controls.Add(bt1);

			bt2.Location = new Point(310, 5);
			bt2.Size = new Size(300, 300);
			bt2.BackgroundImage = CustomerService.Properties.Resources.dichvu;
			bt2.BackgroundImageLayout = ImageLayout.Zoom;
			bt2.Text = "Mở tài khoản";
			bt2.Font = new Font("Timesnewroman", 30, FontStyle.Italic);
			bt2.ForeColor = Color.Purple;
			bt2.TextAlign = ContentAlignment.BottomCenter;
			this.Controls.Add(bt2);

			bt3.Location = new Point(5, 310);
			bt3.Size = new Size(300, 300);
			this.Controls.Add(bt3);

			bt4.Location = new Point(310, 310);
			bt4.Size = new Size(300, 300);
			this.Controls.Add(bt4);
		}
		private void bt1_hover(object sender, EventArgs e)
		{
			bt1.ForeColor = Color.Blue;
		}
		private void bt1_leave(object sender, EventArgs e)
		{
			bt1.ForeColor = Color.Purple;
		}
		private void bt2_hover(object sender, EventArgs e)
		{
			bt2.ForeColor = Color.Blue;
		}
		private void bt2_leave(object sender, EventArgs e)
		{
			bt2.ForeColor = Color.Purple;
		}
		private void bt1_click(object sender, EventArgs e)
		{
			Function.fmName = 1;
			Layso layso = new Layso();

			layso.MdiParent = this;
			layso.Text = "LẤY SỐ THỨ TỰ GIAO DỊCH TIỀN GỬI";

			layso.Dock = DockStyle.Fill;
			layso.ControlBox = false;
			layso.AutoSize = false;

			layso.Show();

			bt1.Hide();
			bt2.Hide();
			bt3.Hide();
			bt4.Hide();
			//this.Hide();
		}

		private void bt2_click(object sender, EventArgs e)
		{
			Function.fmName = 2;
			Layso layso = new Layso();

			layso.MdiParent = this;
			layso.Text = "LẤY SỐ THỨ TỰ GIAO DỊCH MỞ TÀI KHOẢN";

			layso.Dock = DockStyle.Fill;
			layso.ControlBox = false;
			layso.AutoSize = false;

			layso.Show();

			bt1.Hide();
			bt2.Hide();
			bt3.Hide();
			bt4.Hide();
		}

		private void Main_Load(object sender, EventArgs e)
		{

		}

				
		
	}
}
