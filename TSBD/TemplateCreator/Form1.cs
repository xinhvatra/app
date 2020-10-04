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

namespace TSBD
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			button1.Visible = false;
		}


		int count = 0;
		int dunohientai = 0;
		string[] LDS, giatri, giatritt, mucdichvay;
		private void button1_Click(object sender, EventArgs e)
		{
			for (int j = 0; j < dtDuno.Rows.Count - 1; j++) // chạy bảng khách hàng đã phân công theo cán bộ
			{
				
				Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
				Document doc = new Document();
				string filename = "";
				string pa = System.Environment.CurrentDirectory;

				filename = (pa + "\\12A.docx");
				//if (j > 7) filename = (pa + "\\TBB CN.docx");
				//MessageBox.Show(filename);
				object missing = System.Type.Missing;
				doc = word.Documents.Open(Path.GetFullPath(filename),
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing);
				count = 0;
				LDS = new string[5] { "", "", "", "", "" };
				 giatri  = new string[5] { "", "", "", "", "" };
				 mucdichvay = new string[5] { "", "", "", "", "" };
				 giatritt = new string[5] { "", "", "", "", "" };

				for (int i = 1; i <= doc.Paragraphs.Count; i++)
				{ int dem = j;
					//label2.Text = dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString();
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "tenkhachhang"))
					{
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("tenkhachhang", dtDuno.Rows[j][2].ToString());
					}

					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "diachi"))
					{
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace( "diachi", dtDuno.Rows[j][52].ToString() + ", " + dtDuno.Rows[j][50].ToString() + ", " + dtDuno.Rows[j][48].ToString());
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "LAV"))
					{
						if (dtDuno.Rows[j][17].ToString().Trim() == "") break;
						dunohientai = Int32.Parse(dtDuno.Rows[j][17].ToString());                      
						LDS[0] = dtDuno.Rows[j][8].ToString();
						giatri[0] = dtDuno.Rows[j][13].ToString();
						mucdichvay[0] = dtDuno.Rows[j][58].ToString();
						giatritt[0] = dtDuno.Rows[j][17].ToString();
						for (int z = j + 1; z < dtDuno.Rows.Count-1; z++)
						{	if (dtDuno.Rows[z][3].ToString().Trim() == "") break;
							if (dtDuno.Rows[j][3].ToString() == dtDuno.Rows[z][3].ToString())
							{

								count++;
								//MessageBox.Show(" dtDuno.Rows[j][3].ToString(): " + dtDuno.Rows[j][3].ToString() + " dtDuno.Rows[z][3].ToString(): " + dtDuno.Rows[z][3].ToString());
								try
								{
									dunohientai += Int32.Parse(dtDuno.Rows[j][17].ToString());
								}
								catch (Exception )
								{
									dunohientai = 0;

								}
								LDS[count] = dtDuno.Rows[z][8].ToString();
								giatri[count] = dtDuno.Rows[z][13].ToString();
								mucdichvay[count] = dtDuno.Rows[z][58].ToString();
								giatritt[count] = dtDuno.Rows[z][17].ToString();
								label2.Text = "LDS0: " + LDS[0] + " - giatri0: " + giatri[0] + " - " + " mucdichvay0: " + mucdichvay[0] + " - giatritt[0]: " + giatritt[0] + "\n";
								label2.Text += "LDS1: " + LDS[count] + " - giatri1: " + giatri[count] + " - " + " mucdichvay1: " + mucdichvay[count] + " - giatritt[1]: " + giatritt[count] + "\n";

							}
							else break;
							
						}
					
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("LAV", dtDuno.Rows[j][3].ToString());
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "ngayvay"))
					{
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("ngayvay", dtDuno.Rows[j][4].ToString());

					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "dunohientai"))
					{
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("dunohientai", dunohientai.ToString("N0"));

					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "sotiengiaingan"))
					{
						int dnhientai = Int32.Parse(dtDuno.Rows[j][7].ToString());
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("sotiengiaingan", dnhientai.ToString("N0"));
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "mucdichvay"))
					{
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("mucdichvay", dtDuno.Rows[j][58].ToString());
					}
					else
					{
						for (int k = 0; k < 5; k++)
						{

							if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "LDS" + (k + 1).ToString()))
							{	
								label1.Text += "LDS" + (k + 1).ToString() + ": " + LDS[k].ToString();
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("LDS" + (k + 1).ToString(), LDS[k].ToString());
							}
							else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "Mucdichvay" + (k + 1).ToString()))
							{
								
								label1.Text += "Mucdichvay" + (k + 1).ToString() + ": " + mucdichvay[k].ToString();
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Mucdichvay" + (k + 1).ToString(), mucdichvay[k].ToString());
							}
							else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "Giatri" + (k + 1).ToString()))
							{
								if (giatri[k].ToString() == "")
								{
									doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Giatri" + (k + 1).ToString(), giatri[k].ToString());
									continue;
								}
								label1.Text += "Giatri" + (k + 1).ToString() + ": " + giatri[k].ToString();
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Giatri" + (k + 1).ToString(),Int32.Parse(giatri[k].ToString()).ToString("N0"));
							}
							else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "Dno" + (k + 1).ToString()))
							{
								if (giatritt[k].ToString() == "")
								{
									doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Dno" + (k + 1).ToString(), giatritt[k].ToString());
									continue;
									
								}
									label1.Text += "Du no " + (k + 1).ToString() + ": " + giatritt[k].ToString()+"\n";
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Dno" + (k + 1).ToString(), Int32.Parse(giatritt[k].ToString()).ToString("N0"));
							}
						}
					}
				}
				 //MessageBox.Show("OK XONG!");
				 ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\12A\\" + dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString() + ".docx");
				//	else ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\DN\\" + dtTaisan.Rows[j][2].ToString() + "_" + dtTaisan.Rows[j][1].ToString() + "_" + dtTaisan.Rows[j][5].ToString() + ".docx");
				((_Document)doc).Close();
				((_Application)word).Quit();			
				j = j + count;				
				if (j >= dtDuno.Rows.Count-1) break;
			}
			MessageBox.Show("OK XONG!");
		}

		DataTable dtDuno, dtTaisan;
		//lấy dư nợ ở sheet1, tài sản ở sheet2
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
						string sql = "SELECT * from [Sheet1$]"; //dữ liệu lấy từ ipcas
						using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql, conn))
						{
							cmd.Fill(myDataSet);
						}
						dtDuno = myDataSet.Tables[0];
						//myDataSet = new DataSet();
						//sql = "SELECT * from [Sheet2$]";//bảng chọn mẫu
						//using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql, conn))
						//{
						//	cmd.Fill(myDataSet);
						//}
						//dtTaisan = myDataSet.Tables[0];
						//tring str1 = "insert into Nhansu  values(?, ?, ?)";						
						conn.Close();

					}
					button1.Visible = true;
				}
			}
		}
	}
}
