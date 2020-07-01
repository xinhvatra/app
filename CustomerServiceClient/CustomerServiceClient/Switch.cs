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
		public static DataTable data;
		public static DataGridView dtgrid;
		static int service_id;
		public Switch()
		{
			InitializeComponent();
			dtgrid = new DataGridView();
			dtgrid.Size = new Size(340, 190);
			dtgrid.Location = new Point(25, 50);
			this.Controls.Add(dtgrid);
			data = new DataTable();
			data.Columns.Add("Giao dịch viên");
			data.Columns.Add("Cửa số");
			data.Columns.Add("Tình trạng");
			dtgrid.DataSource = data;
			dtgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dtgrid.CellClick += new DataGridViewCellEventHandler(dtgrid_OnCellValueChanged);
		}

		private void Switch_Load(object sender, EventArgs e)
		{	for (int i = 0; i < Client.dt_service.Length; i++)
			{
				comboBox1.Items.Add(Client.dt_service[i]);
			}
			//Client.sw = this;
			
		}
		private void dtgrid_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 4 && e.RowIndex != -1)
			{	
				foreach(DataGridViewRow r in dtgrid.Rows)
				{
					DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)r.Cells[4];
					
					if (chk.Value == chk.TrueValue)
					{
						chk.Value = chk.FalseValue;
					}
					else
					{
						chk.Value = chk.TrueValue;
					}				
				}
			}
		}
		public static string gate, gdv;
		private void button1_Click(object sender, EventArgs e)
		{	bool check = false;			
			int receive_id = 0;
			foreach (DataGridViewRow r in dtgrid.Rows)
			{
				DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)r.Cells[4];
				string vl = chk.Value+"";
				if (vl.Equals("True"))
				{
					check = true;
					receive_id= Int32.Parse(r.Cells[0].Value + "");
					gdv = r.Cells[1].Value + "";
					gate = r.Cells[2].Value + "";
					break;
				}
				
			}
			if (check) {
			//	MessageBox.Show("Bạn đã chuyển tiếp khách hàng số "+Client.customer+" cho GVD "+gdv+" tại cửa số "+gate);
				SocketRun.connect();
				SocketRun.sendDataSwitch("pass",service_id, receive_id, Client.customer);
				button1.Visible = false;
			}
			else
			{
				MessageBox.Show("Bạn chưa chọn giao dịch viên nào!");
			}
			//this.Close();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			SocketRun.connect();
			SocketRun.sendData("switch",comboBox1.SelectedIndex+1);
			service_id = comboBox1.SelectedIndex + 1;
			//MessageBox.Show((comboBox1.SelectedIndex + 1)+"");
			//dtgrid.DataSource = data.Tables[0];
		}
	}
}
