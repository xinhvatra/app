using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomerServiceClient
{
	public partial class Client : Form
	{
		public static Label lb;
		//public static Button btClose;
		//public static string stringTop;
		public Client()
		{
			InitializeComponent();
			this.FormClosing += Client_FormClosing;
			Graphics g = this.CreateGraphics();			
			var px = SystemInformation.VirtualScreen.Width / g.DpiX * 72;
			var py = SystemInformation.VirtualScreen.Height / g.DpiY * 72;			
			this.Location = new Point(Convert.ToInt32(px)+50 , Convert.ToInt32(py)-80);
			g.Dispose();
			
			//btClose = new Button();
			//btClose.Text = "X";
			//btClose.Size = new Size(20, 20);
			//btClose.Location = new Point(this.Width-40, 1);
			//btClose.Click += new EventHandler(btClose_click);
			//btClose.ForeColor = Color.Red;
			//this.Controls.Add(btClose);

			lb = new Label();
			lb.Size = new Size(this.Width,20);
			lb.Location = new Point(3, 1);
			lb.TextAlign = ContentAlignment.MiddleLeft;
			lb.Text = "1000";
			lb.Font = new Font("Timesnewroman", 10, FontStyle.Bold);
			lb.ForeColor = Color.Blue;
			this.Controls.Add(lb);
			SocketRun.SocketCreate();
		}
		private void Client_FormClosing(object sender, FormClosingEventArgs e)
		{
			//You may decide to prompt to user
			//else just kill
			System.Environment.Exit(1);
		}
		private void btClose_click(object sender, EventArgs e)
		{
			System.Environment.Exit(1);
		}
		private void Client_Load(object sender, EventArgs e)
		{
			SocketRun.fm = this;
			SocketRun.SocketCreate();
		}
		
		private void button1_Click(object sender, EventArgs e)
		{
			SocketRun.sendData("data");
		}
		public static void processData(string st)
		{			
			String[] arrRs = st.Split('|');
			if (arrRs[0]== "login")
			{				
				lb.Text = arrRs[3]+ " - Cổng số "+arrRs[4];					
			}
			else
			{

			}
			
		}
	}
}
