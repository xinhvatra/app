using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
		int[] num;
		private void Form1_Load(object sender, EventArgs e)
		{
			num = new int[150];
			for (int i = 0; i < num.Length; i++)
			{
				num[i] = i + 1;
			}
			label1.Font = new Font("Showcard Gothic", 300);
			radioButton1.Checked = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{	
			Random rd = new Random();
			int rdn = rd.Next(num.Length);
			label1.Text = num[rdn].ToString();
		}
	}
}
