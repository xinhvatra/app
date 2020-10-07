using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomNumber
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		int leng;
		int num_del;
		List<int> num;
		public event EventHandler ResizeEnd;
		private void Form1_Load(object sender, EventArgs e)
		{;
			this.Resize += Form1_Resize;

			textBox1.Text = "30";
			counter = Int32.Parse(textBox1.Text);
			textBox2.Text = "147";
			leng = Int32.Parse(textBox2.Text);
			num = new List<int>();
			for (int i = 0; i < leng; i++)
			{
				num.Add(i + 1);
			}
			label1.Font = new Font("Showcard Gothic", 300);
			label1.Location = (new Point(this.Width / 5, this.Height / 5));
			radioButton1.Checked = true;
			label2.Font = new Font("Showcard Gothic", 100);
			//label2.Visible = false;
			label2.Text = "00:00";

		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			//MessageBox.Show("You are in the Form.ResizeEnd event.");
			//throw new NotImplementedException();
		}
		bool isClick =false;
		Timer timer1,random_time;
		private void button1_Click(object sender, EventArgs e)
		{
			
			counter = Int32.Parse(textBox1.Text);
			leng = Int32.Parse(textBox2.Text);
			num = new List<int>();
			for (int i = 0; i < leng; i++)
			{
				num.Add(i + 1);
			}
			if (radioButton1.Checked)
			{
				if (isClick)
				{
					button1.Text = "Bắt đầu";
					isClick = false;
					random_time.Stop();					
					Random rd = new Random();
					int rdn = rd.Next(num.Count);
					label1.Text = num[rdn].ToString();
					num_del = num[rdn];
				}
				else
				{
					button1.Text = "Dừng";
					isClick = true;
					random_time = new Timer();
					random_time.Tick += new EventHandler(random_timer_Tick);
					random_time.Interval = 100;
					random_time.Start();
					
				}
				

			}
			else
			{
				button1.Visible = false;
				counter = Int32.Parse(textBox1.Text);
				timer1 = new Timer();
				timer1.Tick += new EventHandler(timer1_Tick);
				timer1.Interval = 1000; // 1 second
				timer1.Start();

				random_time = new Timer();
				random_time.Tick += new EventHandler(random_timer_Tick);
				random_time.Interval = 100;
				random_time.Start();

				if (counter < 10)
				{
					label2.Text = "00:0" + counter.ToString();
				}
				else
				{
					label2.Text = "00:" + counter.ToString();
				}
				
			}
		

		
		}
		int counter = 0;
		private void timer1_Tick(object sender, EventArgs e)
		{
			counter--;
			if (counter <= 5)
			{
				label2.ForeColor = Color.Red;
			}
			if (counter == 0)
			{
				timer1.Stop();
				random_time.Stop();
				button1.Visible = true;
				Random rd = new Random();
				int rdn = rd.Next(num.Count);
				label1.Text = num[rdn].ToString();
				num_del = num[rdn];
			}
			if (counter < 10)
			{
				label2.Text = "00:0" + counter.ToString();
			}
			else
			{
				label2.Text = "00:" + counter.ToString();
			}
			
		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			
			label2.Visible = false;
			label3.Visible = false;
			textBox1.Visible = false;
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			
			label2.Visible = true;
			label3.Visible = true;
			textBox1.Visible = true;
		}

		private void random_timer_Tick(object sender, EventArgs e)
		{		
			
				Random rd = new Random();
				int rdn = rd.Next(num.Count);
				label1.Text = num[rdn].ToString();
				

		}
		
	}
}
