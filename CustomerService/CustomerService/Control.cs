using MySql.Data.MySqlClient;
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
	public partial class Control : Form
	{
		int client_id = 0;
		int question_id = 0, vote_id = 0,vote_attr_id=0;
		string votes = "";
		public Control()
		{
			InitializeComponent();

			foreach (DataRow dtrow in Function.services.Rows)
			{
				comboBox1.Items.Add(dtrow[1]);
			}
			dataGridView1.CellClick += new DataGridViewCellEventHandler(dtgrid_OnCellValueChanged);
		}
		private void dtgrid_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (isQuestion)
			{
				if (e.RowIndex != -1)
				{
					question_id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
					string Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();					
					textBox1.Text = Text;
					textBox1.Height = (Text.Split('\n').Length + 4) * textBox1.Font.Height;
					comboBox1.SelectedIndex = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "True" ? 1 : 0;
					
					//textBox3.Text = vote_id.ToString();
					//textBox4.Text = votes = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

					label2.Location = new Point(textBox1.Location.X, textBox1.Location.Y + textBox1.Height + 6);
					comboBox1.Location = new Point(textBox1.Location.X, label2.Location.Y + label2.Height + 5);
					//label3.Location = new Point(textBox1.Location.X, comboBox1.Location.Y + comboBox1.Height + 6);
					//textBox3.Location = new Point(textBox1.Location.X, label3.Location.Y + label3.Height + 5);
					//label4.Location = new Point(textBox1.Location.X, textBox3.Location.Y + textBox3.Height + 6);
					//textBox4.Location = new Point(textBox1.Location.X, label4.Location.Y + label4.Height + 5);
				}
			}
			else if (is_vote)
			{
				if (e.RowIndex != -1)
				{
					string Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
					textBox1.Text = Text;
					textBox1.Height = (Text.Split('\n').Length + 4) * textBox1.Font.Height;
					comboBox1.SelectedIndex = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() == "True" ? 1 : 0;
					vote_attr_id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
					
				}
			}
			else
			{
				if (e.RowIndex != -1)
				{
					if (radioButton1.Checked)
					{
						radioButton4.Checked = true;
						textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
						comboBox1.SelectedItem = (String)dataGridView1.Rows[e.RowIndex].Cells[2].Value;
						textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
						textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
						client_id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
						textBox1.SelectAll();
						textBox1.Focus();

					}
					else if (radioButton2.Checked)
					{
						radioButton4.Checked = true;
						textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
						client_id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
						textBox1.SelectAll();
						textBox1.Focus();
					}
				}
			}
		}
		private void Edit_Load(object sender, EventArgs e)
		{
			//dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			//dataGridView1.AutoSize = true;
			for (int i = 0; i < dataGridView1.Rows.Count; i++)
			{
				dataGridView1.Columns[i].Width = 10;
			}

			radioButton1.Checked = true;
			radioButton2.Checked = false;
			radioButton3.Checked = true;
			radioButton4.Checked = false;
			radioButton5.Checked = true;


			textBox1.Multiline = true;
			isQuestion = false;
			//getData();

		}
		private void getData()
		{

			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "SELECT client.id,client.ipcas,services.`name` AS `Nghiệp vụ`,client.`name` `Tên` ,client.gate AS `Cửa` FROM CLIENT INNER JOIN services WHERE client.`service_id`=services.`id`";
			MySqlDataAdapter cmd = new MySqlDataAdapter(sql, conn);
			DataTable dt = new DataTable();
			cmd.Fill(dt);
			dataGridView1.DataSource = dt;
			conn.Close();
		}
		private void button1_Click(object sender, EventArgs e)
		{
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			if (radioButton5.Checked)
			{
				if (radioButton1.Checked)
				{
					if (radioButton3.Checked)
					{
						
						string sql = "INSERT into client (ipcas,service_id,name,gate,idle,active,ip_address) values(@ipcas,@service_id,@name,@gate,@idle,@active,@ip_address)";
						MySqlCommand cmd = new MySqlCommand();
						cmd.Connection = conn;
						cmd.CommandText = sql;

						cmd.Parameters.Add("@ipcas", MySqlDbType.String).Value = textBox1.Text;
						cmd.Parameters.Add("@service_id", MySqlDbType.Int32).Value = comboBox1.SelectedIndex + 1;
						cmd.Parameters.Add("@name", MySqlDbType.String).Value = textBox3.Text;
						cmd.Parameters.Add("@gate", MySqlDbType.Int32).Value = textBox4.Text;
						cmd.Parameters.Add("@idle", MySqlDbType.Int32).Value = 0;
						cmd.Parameters.Add("@active", MySqlDbType.Int32).Value = 0;
						cmd.Parameters.Add("@ip_address", MySqlDbType.String).Value = "0.0.0.0";
						cmd.ExecuteNonQuery();
						textBox1.Text = "";
						textBox1.Focus();
					}
					else if (radioButton4.Checked)
					{
						try
						{
							
							string sql = "UPDATE client SET  ipcas=@ipcas,service_id=@service_id,name=@name,gate=@gate WHERE  id=@id";
							MySqlCommand cmd = new MySqlCommand();
							cmd.Connection = conn;
							cmd.CommandText = sql;

							cmd.Parameters.Add("@ipcas", MySqlDbType.String).Value = textBox1.Text;
							cmd.Parameters.Add("@service_id", MySqlDbType.Int32).Value = comboBox1.SelectedIndex + 1;
							cmd.Parameters.Add("@name", MySqlDbType.String).Value = textBox3.Text;
							cmd.Parameters.Add("@gate", MySqlDbType.Int32).Value = textBox4.Text;
							cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = client_id;
							cmd.ExecuteNonQuery();
							
						}
						catch (Exception)
						{
							MessageBox.Show("Không tìm thấy thông tin người sử dụng");
						}

					}
					
					getData();
				}
				else if (radioButton2.Checked)
				{
					

					if (radioButton3.Checked)
					{

						string sql = "INSERT into services (name) values(@name)";
						MySqlCommand cmd = new MySqlCommand();
						cmd.Connection = conn;
						cmd.CommandText = sql;
						cmd.Parameters.Add("@name", MySqlDbType.String).Value = textBox1.Text;
						cmd.ExecuteNonQuery();

						long id = cmd.LastInsertedId;
						sql = "update services  set current_cus = @current_cus where id =@id";
						cmd = new MySqlCommand();
						cmd.Connection = conn;
						cmd.CommandText = sql;
						cmd.Parameters.Add("@current_cus", MySqlDbType.Int32).Value = id * 1000;
						cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
						cmd.ExecuteNonQuery();
						textBox1.Text = "";
						textBox1.Focus();
					}
					else if (radioButton4.Checked)
					{
						try
						{
							string sql = sql = "update services  set name = @name where id =@id";
							MySqlCommand cmd = new MySqlCommand();
							cmd.Connection = conn;
							cmd.CommandText = sql;
							cmd.Parameters.Add("@name", MySqlDbType.String).Value = textBox1.Text;
							cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = client_id;
							cmd.ExecuteNonQuery();

						}
						catch (Exception)
						{
							MessageBox.Show("Không tìm thấy thông tin");
						}

					}
					
					getService();
				}



			}
			//thêm sửa các câu hỏi hiển thị trên màn hình android
			else if (radioButton6.Checked)
			{				
				if (is_vote) //các thuộc tính đánh giá
				{
					if (radioButton3.Checked) //thêm mới thuộc tính đánh giá
					{

						string sql = "INSERT into vote_attr (vote_id,value,active) values(@vote_id,@value,@active)";
						MySqlCommand cmd = new MySqlCommand();
						cmd.Connection = conn;
						cmd.CommandText = sql;
						cmd.Parameters.Add("@vote_id", MySqlDbType.Int32).Value = vote_id;
						cmd.Parameters.Add("@value", MySqlDbType.String).Value = textBox1.Text;
						cmd.Parameters.Add("@active", MySqlDbType.String).Value = comboBox1.SelectedIndex;
						cmd.ExecuteNonQuery();
						textBox1.Text = "";
						textBox1.Focus();
					}
					else if (radioButton4.Checked) //chỉnh sửa thuộc tính đánh giá
					{
						
							string sql  = "update vote_attr  set value = @value,active=@active where id =@id";
							MySqlCommand cmd = new MySqlCommand();
							cmd.Connection = conn;
							cmd.CommandText = sql;
							cmd.Parameters.Add("@value", MySqlDbType.String).Value = textBox1.Text;
							cmd.Parameters.Add("@active", MySqlDbType.String).Value = comboBox1.SelectedIndex;
							cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = vote_attr_id;
							cmd.ExecuteNonQuery();					

					}
					
					getVote_attr();
				}
				else //các câu hỏi cho khách hàng
				{
					if (radioButton3.Checked) //thêm mới câu hỏi
					{
						string sql = "INSERT into question(question,active) values(@question,@active)";
						MySqlCommand cmd = new MySqlCommand();
						cmd.Connection = conn;
						cmd.CommandText = sql;
						cmd.Parameters.Add("@question", MySqlDbType.String).Value = textBox1.Text;
						cmd.Parameters.Add("@active", MySqlDbType.Int32).Value = comboBox1.SelectedIndex;
						cmd.ExecuteNonQuery();
						//long id = cmd.LastInsertedId;
						//if (id > 0)
						//{
						//	string[] vt_arr = { "Rất hài lòng", "Hài lòng", "Không hài lòng", "Ý kiến khác" };
						//	for (int i = 0; i < 4; i++)
						//	{
						//		sql = "INSERT into question_attr(question_id,votes) values(" + id + ",@votes)";
						//		cmd = new MySqlCommand();
						//		cmd.Connection = conn;
						//		cmd.CommandText = sql;
						//		cmd.Parameters.Add("@votes", MySqlDbType.String).Value = vt_arr[i];
						//		cmd.ExecuteNonQuery();
						//	}
						//}

						textBox1.Text = "";
						textBox1.Focus();
					}
					else if (radioButton4.Checked) //chỉnh sửa câu hỏi
					{
						string sql = "UPDATE question SET question=@question,active=@active WHERE id=@id";
						MySqlCommand cmd = new MySqlCommand();
						cmd.Connection = conn;
						cmd.CommandText = sql;
						cmd.Parameters.Add("@question", MySqlDbType.String).Value = textBox1.Text;
						cmd.Parameters.Add("@active", MySqlDbType.Int32).Value = comboBox1.SelectedIndex;
						cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = question_id;
						cmd.ExecuteNonQuery();

						//MessageBox.Show("vote:    " + votes+"  question_id  "+question_id);
						//sql = "UPDATE question_attr SET votes=@votes WHERE question_id=@question_id AND votes=@vt";
						//cmd = new MySqlCommand();
						//cmd.Connection = conn;
						//cmd.CommandText = sql;
						//cmd.Parameters.Add("@votes", MySqlDbType.String).Value = textBox4.Text;
						//cmd.Parameters.Add("@question_id", MySqlDbType.Int32).Value = question_id;
						//cmd.Parameters.Add("@vt", MySqlDbType.String).Value = votes;
						//cmd.ExecuteNonQuery();
					}
				
					getQuestion();
				}
			}
			conn.Close();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{

			radioButton1.ForeColor = Color.Blue;
			radioButton2.ForeColor = Color.Black;
			label1.Text = "Mã ipcas";
			textBox1.Clear();
			textBox3.Clear();
			textBox4.Clear();
			comboBox1.SelectedIndex = -1;
			label2.Visible = true;
			label3.Visible = true;
			label4.Visible = true;
			textBox3.Visible = true;
			textBox4.Visible = true;
			comboBox1.Visible = true;
			getData();
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			radioButton2.ForeColor = Color.Blue;
			radioButton1.ForeColor = Color.Black;
			label1.Text = "Nghiệp vụ";
			textBox1.Clear();
			textBox3.Clear();
			textBox4.Clear();
			comboBox1.SelectedIndex = -1;
			label2.Visible = false;
			label3.Visible = false;
			label4.Visible = false;
			textBox3.Visible = false;
			textBox4.Visible = false;
			comboBox1.Visible = false;
			getService();
		}
		private void getService()
		{
			//dataGridView1.DataSource = null;
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "SELECT services.id,services.`name` AS `Nghiệp vụ` from services";
			MySqlDataAdapter cmd = new MySqlDataAdapter(sql, conn);
			DataTable dt = new DataTable();
			cmd.Fill(dt);
			dataGridView1.DataSource = dt;
			conn.Close();
		}
		private void getQuestion()
		{
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "SELECT question.id as 'Mã câu hỏi', question as 'Câu hỏi', question.active as 'Sử dụng' FROM question ";
			MySqlDataAdapter cmd = new MySqlDataAdapter(sql, conn);
			DataTable dt = new DataTable();
			cmd.Fill(dt);
			dataGridView1.DataSource = dt;
			conn.Close();
		}
		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
			if (isQuestion)
			{
				textBox1.Clear();			
				textBox1.Focus();
				textBox1.Height = 50;			
				label2.Location = new Point(textBox1.Location.X, textBox1.Location.Y + textBox1.Height + 6);
				comboBox1.Location = new Point(textBox1.Location.X, label2.Location.Y + label2.Height + 5);				
				comboBox1.SelectedIndex = 1;
			}
			else
			{
				textBox1.Clear();
				textBox3.Clear();
				textBox4.Clear();
				comboBox1.SelectedIndex = 1;
				textBox1.Focus();
				textBox4.Enabled = true;
			}
		}

		private void radioButton4_CheckedChanged(object sender, EventArgs e)
		{
			if (isQuestion)
			{
				textBox1.Clear();				
				textBox1.Focus();
				
				label2.Location = new Point(textBox1.Location.X, textBox1.Location.Y + textBox1.Height + 6);
				comboBox1.Location = new Point(textBox1.Location.X, label2.Location.Y + label2.Height + 5);				
			}
		}
		Boolean isQuestion = true;
		private void radioButton6_CheckedChanged(object sender, EventArgs e)
		{				
			comboBox1.Items.Clear();
			comboBox1.Items.Add("Không");
			comboBox1.Items.Add("Có");
			comboBox1.SelectedIndex = -1;
			isQuestion = true;
			radioButton4.Checked = true;
			radioButton1.Visible = false;
			radioButton2.Visible = false;
			label2.Visible = true;
			comboBox1.Visible = true;
			label3.Visible = false;
			textBox3.Visible = false;
			label4.Visible = false;
			textBox4.Visible = false;
			radioButton6.ForeColor = Color.Red;
			radioButton5.ForeColor = Color.Black;

			textBox1.Text = "";
			textBox3.Text = "";
			textBox3.Enabled = false;
			textBox4.Text = "";
			label1.Text = "Câu hỏi";
			label2.Text = "Sử dụng câu hỏi";
			comboBox1.SelectedIndex = -1;
			label3.Text = "Mã đánh giá";
			label4.Text = "Đánh giá";

			getQuestion();

		}

		private void radioButton5_CheckedChanged(object sender, EventArgs e)
		{
			button2.Visible = false;
			comboBox1.Items.Clear();
			foreach (DataRow dtrow in Function.services.Rows)
			{
				comboBox1.Items.Add(dtrow[1]);
			}
			isQuestion = false;
			radioButton1.Visible = true;
			radioButton2.Visible = true;
			textBox3.Enabled = true;
			textBox4.Enabled = true;
			radioButton5.ForeColor = Color.Red;
			radioButton6.ForeColor = Color.Black;
			textBox1.Text = "";
			textBox1.Height = 22;
			comboBox1.SelectedIndex = -1;
			textBox3.Text = "";
			textBox4.Text = "";
			label1.Text = "Mã ipcas";
			label2.Text = "Nghiệp vụ";
			label3.Text = "Tên";
			label4.Text = "Cửa sổ";
			label2.Location = new Point(textBox1.Location.X, textBox1.Location.Y + textBox1.Height + 6);
			comboBox1.Location = new Point(textBox1.Location.X, label2.Location.Y + label2.Height + 5);
			label3.Location = new Point(textBox1.Location.X, comboBox1.Location.Y + comboBox1.Height + 6);
			textBox3.Location = new Point(textBox1.Location.X, label3.Location.Y + label3.Height + 5);
			label4.Location = new Point(textBox1.Location.X, textBox3.Location.Y + textBox3.Height + 6);
			textBox4.Location = new Point(textBox1.Location.X, label4.Location.Y + label4.Height + 5);
			getData();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (!is_vote)
			{
				is_vote = true;
				isQuestion = false;
				textBox3.Visible = false;
				textBox4.Visible = false;
				label3.Visible = false;
				label4.Visible = false;
				textBox1.Text = "";
				button2.Text = "Quay lại";
				getVote_attr();
			}
			else
			{
				is_vote = false;
				isQuestion = true;
				button2.Visible = false;
				button2.Text = "Sửa";
				comboBox1.Items.Clear();
				comboBox1.Items.Add("Không");
				comboBox1.Items.Add("Có");
				comboBox1.SelectedIndex = -1;
				isQuestion = true;
				radioButton4.Checked = true;
				radioButton1.Visible = false;
				radioButton2.Visible = false;
				label2.Visible = true;
				comboBox1.Visible = true;
				label3.Visible = true;
				textBox3.Visible = true;
				label4.Visible = true;
				textBox4.Visible = true;
				radioButton6.ForeColor = Color.Red;
				radioButton5.ForeColor = Color.Black;

				textBox1.Text = "";
				textBox3.Text = "";
				textBox3.Enabled = false;
				textBox4.Text = "";
				label1.Text = "Câu hỏi";
				label2.Text = "Sử dụng câu hỏi";
				comboBox1.SelectedIndex = -1;
				label3.Text = "Mã đánh giá";
				label4.Text = "Đánh giá";

				getQuestion();
			}

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private bool is_vote = false;
		private void getVote_attr()
		{
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "SELECT vote_attr.id as 'Mã', vote_attr.vote_id as 'Mã câu hỏi phụ', vote_attr.value as 'Câu hỏi phụ'" +
				", active as 'Sử dụng' FROM vote_attr where vote_id =" + vote_id;
			MySqlDataAdapter cmd = new MySqlDataAdapter(sql, conn);
			DataTable dt = new DataTable();
			cmd.Fill(dt);
			dataGridView1.DataSource = dt;
			conn.Close();
		}
	}
}
