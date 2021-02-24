using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace QuesttionRender
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			button1.Visible = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			int roww = 20;
			DateTime dt = new DateTime();
			List<string> tempLav =new List<string>();
			List<string> tempLavDuno =new List<string>();
			List<string> tempLavSdctd =new List<string>();
			
			for (int j = 0; j < dtName.Rows.Count; j++) // chạy bảng khách hàng đã phân công theo cán bộ
			{
				label2.Text = "Đang chạy khách hàng thứ: " + (j + 1);
				Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
				Document doc = new Document();
				string filename = "";
				string pa = System.Environment.CurrentDirectory;

				filename = (pa + "\\TBB DN.docx");
				if (j > roww) filename = (pa + "\\TBB CN.docx");
				//MessageBox.Show(filename);
				object missing = System.Type.Missing;
				doc = word.Documents.Open(Path.GetFullPath(filename),
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing);
				for (int i = 1; i <= doc.Paragraphs.Count; i++)
				{
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[cán bộ quản lý]"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString())) //so sánh mã KH ở bảng phân công cán bộ với dữ liệu lấy từ ipcas
							{
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "\\[cán bộ quản lý]", dtData.Rows[k][32].ToString(), RegexOptions.IgnoreCase);
								//MessageBox.Show(dtData.Rows[k][32].ToString());
								break;
							}
						}
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[mục đích vay]"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "\\[mục đích vay]", dtData.Rows[k][61].ToString(), RegexOptions.IgnoreCase);
								//MessageBox.Show(dtData.Rows[k][32].ToString());
								break;
							}
						}
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[số dư cấp tín dụng]"))
					{
						long sum = 0;
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{
								if (tempLavSdctd.Contains(dtData.Rows[k][3].ToString())) {
									//MessageBox.Show("LAV số dư cấp tín dụng: " + dtData.Rows[k][3].ToString()+"-"+ Convert.ToInt64(dtData.Rows[k][7].ToString()).ToString("N0"));
									continue; }
								else
								{
									doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "\\[số dư cấp tín dụng]", Convert.ToInt64(dtData.Rows[k][7].ToString()).ToString("N0"), RegexOptions.IgnoreCase);
									tempLavSdctd.Add(dtData.Rows[k][3].ToString());
									break;
								}
								//MessageBox.Show(dtData.Rows[k][32].ToString());
								//sum = long.Parse(dtData.Rows[k][7].ToString());
							}
						}
						//doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "\\[số dư cấp tín dụng] ", Convert.ToInt64(dtData.Rows[k][7].ToString()).ToString("N0"), RegexOptions.IgnoreCase);
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[dư nợ]"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{
								if (tempLavDuno.Contains(dtData.Rows[k][3].ToString()))
								{
									//MessageBox.Show("LAV dư nợ tín dụng: " + dtData.Rows[k][3].ToString() + "-" + Convert.ToInt64(dtName.Rows[j][4].ToString()).ToString("N0"));
									continue;
								}
								else
								{
									doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "\\[dư nợ]", Convert.ToInt64(dtName.Rows[j][4].ToString()).ToString("N0"), RegexOptions.IgnoreCase);
									//MessageBox.Show(dtData.Rows[k][32].ToString());
									tempLavDuno.Add(dtData.Rows[k][3].ToString());
									break;
								}
							}
						}
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[số hợp đồng tín dụng]"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{
								if (tempLav.Contains(dtData.Rows[k][3].ToString()))
								{
									//MessageBox.Show("LAV số hd tín dụng: " + dtData.Rows[k][3].ToString() );
									continue;
								}
								else
								{
									doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "\\[số hợp đồng tín dụng]", dtData.Rows[k][3].ToString(), RegexOptions.IgnoreCase);
									//MessageBox.Show(dtData.Rows[k][32].ToString());
									tempLav.Add(dtData.Rows[k][3].ToString());
									break;
								}
							}
						}
					}
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[dư nợ hđ]"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "\\[dư nợ hđ]", dtData.Rows[k][17].ToString(), RegexOptions.IgnoreCase);
								//MessageBox.Show(dtData.Rows[k][32].ToString());
								break;
							}
						}
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[số tiền giải ngân]"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{
								
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "\\[số tiền giải ngân]", Convert.ToInt64(dtData.Rows[k][13].ToString()).ToString("N0"), RegexOptions.IgnoreCase);
								//MessageBox.Show(dtData.Rows[k][32].ToString());
								
								break;
							}
						}
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[tên khách hàng]"))
					{
						//MessageBox.Show(dtName.Rows[j][2].ToString());
						doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "\\[tên khách hàng]", dtName.Rows[j][2].ToString(), RegexOptions.IgnoreCase);
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "canbokiemtra"))
					{
						//MessageBox.Show(dtName.Rows[j][2].ToString());
						doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "canbokiemtra", dtName.Rows[j][5].ToString(), RegexOptions.IgnoreCase);
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[mã khách hàng]"))
					{
						//MessageBox.Show(dtName.Rows[j][2].ToString());
						doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "\\[mã khách hàng]", dtName.Rows[j][1].ToString(), RegexOptions.IgnoreCase);
					}
				}
				//MessageBox.Show("OK XONG!");
				if (j > roww) ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\CN\\" + dtName.Rows[j][2].ToString() + "_" + dtName.Rows[j][1].ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".docx");
				else ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\DN\\" + dtName.Rows[j][2].ToString() + "_" + dtName.Rows[j][1].ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".docx");
				((_Document)doc).Close();
				((_Application)word).Quit();
			}
			MessageBox.Show("OK XONG!");
		}

		DataTable dtData, dtName;
		private void button2_Click(object sender, EventArgs e)
		{
			DataSet myDataSet = new DataSet();
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = "Excell File |*.xlsx;*.xls";
			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				string extn = Path.GetExtension(openDialog.FileName).ToLower();
				if (extn.Equals(".xls") || extn.Equals(".xlsx"))
				{
					//filename = Path.GetFileNameWithoutExtension(openDialog.FileName);

					string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath(openDialog.FileName) + ";Extended Properties=Excel 12.0";

					OleDbConnection conn = new OleDbConnection(connString);
					conn.Open();
					System.Data.DataTable dbSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
					if (dbSchema != null && dbSchema.Rows.Count >= 1)
					{
						//string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
						string sql = "SELECT * from [Sheet2$]"; //dữ liệu lấy từ ipcas
						using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql, conn))
						{
							cmd.Fill(myDataSet);
						}
						dtData = myDataSet.Tables[0];
						myDataSet = new DataSet();
						sql = "SELECT * from [Sheet1$]";//bảng chọn mẫu
						using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql, conn))
						{
							cmd.Fill(myDataSet);
						}
						dtName = myDataSet.Tables[0];
						//tring str1 = "insert into Nhansu  values(?, ?, ?)";						
						conn.Close();

					}
					button1.Visible = true;
					label1.Text = "Số khách hàng: " + dtName.Rows.Count;
				}
			}
		}
	}
}
