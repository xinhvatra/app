using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerService
{
	public partial class Layso : Form
	{
		public static Label lb;
		public Layso()
		{
			InitializeComponent();
			lb = new Label();
			button1.Location = new Point(this.Width / 2-button1.Width/4, this.Height/6*4);
			lb.Location = new Point(5, this.Height / 5);

			button2.Location = new Point(this.Width / 2 - button1.Width / 4, this.Height/6*5+30 );
			
			
			if (Function.fmName == 1)
			{
				lb.Text = Function.sttKetoan + "";
			}
			else if (Function.fmName == 2)
			{
				lb.Text = Function.sttDichvu + "";
			}
			//lb.Text = Function.sttKetoan + "";
			lb.Size = new Size(600,200);
			lb.Font = new Font("Arial", 99, FontStyle.Bold);
			lb.ForeColor = Color.Red;
			lb.TextAlign = ContentAlignment.MiddleCenter;
			this.Controls.Add(lb);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//MessageBox.Show(label1.Width+"");
			if (Function.fmName == 1)
			{
				Function.sttKetoan++;
			}else if(Function.fmName == 2)
			{
				Function.sttDichvu++;
			}
			this.Close();
			Main.bt1.Show();
			Main.bt2.Show();
			Main.bt3.Show();
			Main.bt4.Show();
			Function.fmName = 0;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
			Main.bt1.Show();
			Main.bt2.Show();
			Main.bt3.Show();
			Main.bt4.Show();
			Function.fmName = 0;
		}
	}
}
