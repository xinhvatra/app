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


		int count = 0,nextrow=0,countts=0;
		int dunohientai = 0;
		string[] LDS, giatri, giatritt, mucdichvay;
		string[] ngayvay, LAV;
		string[] diachits, dientich, loaidat, sothua, tobando, soqsdđ, noicap, ngaycapsdđ, sovaoso, tsganlien,
			dientichxaydung, dientichsudung;
		int[] sotiengiaingan, dunoht, giatritsbđ, phamvibđ, tongts, tongbaodam;
		private void button3_Click(object sender, EventArgs e)
		{
			for (int j = 0; j < dtDuno.Rows.Count; j++) // chạy bảng khách hàng đã phân công theo cán bộ
			{
				count = 0;
				nextrow = 0;
				LAV = new string[5] { "", "", "", "", "" };
				ngayvay = new string[5] { "", "", "", "", "" };
				mucdichvay = new string[5] { "", "", "", "", "" };
				diachits = new string[5] { "", "", "", "", ""};
				dientich = new string[5] { "", "", "", "", ""};
				loaidat = new string[5] { "", "", "", "", "" };
				sothua = new string[5] { "", "", "", "", "" };
				tobando = new string[5] { "", "", "", "", ""};
				soqsdđ = new string[5] { "", "", "", "", "" };
				noicap = new string[5] { "", "", "", "", "" };
				ngaycapsdđ = new string[5] { "", "", "", "", "" };
				sovaoso = new string[5] { "", "", "", "", "" };
				tsganlien = new string[5] { "", "", "", "", "" };
				dientichxaydung = new string[5] { "", "", "", "", "" };
				dientichsudung = new string[5] { "", "", "", "", "" };
				dunoht = new int[5] { 0, 0, 0, 0, 0 };
				sotiengiaingan = new int[5] { 0, 0, 0, 0, 0 };
				giatritsbđ = new int[5] { 0, 0, 0, 0, 0};
				phamvibđ = new int[5] { 0, 0, 0, 0, 0 };
				tongts = new int[5] { 0, 0, 0, 0, 0};
				tongbaodam = new int[5] { 0, 0, 0, 0, 0 };


				//thêm dữ liệu mới cho khách hàng mới
				LAV[count] = dtDuno.Rows[j][3].ToString();
				ngayvay[count] = dtDuno.Rows[j][4].ToString();
				mucdichvay[count] = dtDuno.Rows[j][58].ToString();
				sotiengiaingan[count] = Int32.Parse(dtDuno.Rows[j][7].ToString());
				dunoht[count] = Int32.Parse(dtDuno.Rows[j][17].ToString());

				for (int l = j + 1; l < dtDuno.Rows.Count; l++)
				{
					// nếu trùng mã KH 
					if (dtDuno.Rows[j][0].ToString() == dtDuno.Rows[l][0].ToString())
					{
						nextrow++;
						//nếu trùng LAV
						if (dtDuno.Rows[j][3].ToString() == dtDuno.Rows[l][3].ToString())
						{
							dunoht[count] += Int32.Parse(dtDuno.Rows[l][17].ToString());// cộng dồn dư nợ hiện tại
						}
						else //nếu không trùng LAV thì thêm vào mảng
						{
							count++;
							LAV[count] = dtDuno.Rows[l][3].ToString();
							ngayvay[count] = dtDuno.Rows[l][4].ToString();
							mucdichvay[count] = dtDuno.Rows[l][58].ToString();
							sotiengiaingan[count] = Int32.Parse(dtDuno.Rows[l][7].ToString());
							dunoht[count] = Int32.Parse(dtDuno.Rows[l][17].ToString());
						}
					}
					else break; // không trùng mã Kh thì bỏ qua
				}

				//for (int m = 0; m < dtTaisan.Rows.Count; m++)// chạy bảng tài sản để nhặt dữ liệu
				//{	if (dtTaisan.Rows[m][0].ToString() == "") break;
				//	if (dtDuno.Rows[j][0].ToString() == dtTaisan.Rows[m][2].ToString())// trùng mã KH với bên tài sản
				//	{
				//		if (dtTaisan.Rows[m][7].ToString() == "1") // có thế chấp tại ngân hàng
				//		{	
				//			diachits[countts] = "";							
				//			dientich[countts] = dtTaisan.Rows[m][24].ToString();						
				//			loaidat[countts] = "";
				//			sothua[countts] = "";
				//			tobando[countts] = "";
							
				//			soqsdđ[countts] = dtTaisan.Rows[m][59].ToString();
					
				//		noicap[countts] = "";
				//			ngaycapsdđ[countts] = "";
				//			sovaoso[countts] = "";
				//			tsganlien[countts] = "";
				//			dientichxaydung[countts] = "";
				//			dientichsudung[countts] = "";
							
				//			giatritsbđ[countts] = 0;
				//			phamvibđ[countts] = 0;
				//			tongts[countts] = 0;
				//			tongbaodam[countts] = 0;
				//			countts++;
				//			if (countts >= 5) break;
				//		}
				//	}
				//}

				Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
				Document doc = new Document();
				string filename = "";
				string pa = System.Environment.CurrentDirectory;

				filename = (pa + "\\12B.docx");
				//if (j > 7) filename = (pa + "\\TBB CN.docx");
				//MessageBox.Show(filename);
				object missing = System.Type.Missing;
				doc = word.Documents.Open(Path.GetFullPath(filename),
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing);

				for (int i = 1; i <= doc.Paragraphs.Count; i++)
				{
					label2.Text = dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString();

					int dem = j;
					//label2.Text = dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString();
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[tenkhachhang]"))
					{
						//MessageBox.Show(doc.Paragraphs[i].Range.Text);
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[tenkhachhang]", dtDuno.Rows[j][2].ToString());
					}
					else
						if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[diachi]"))
					{

						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[diachi]", dtDuno.Rows[j][52].ToString() + ", " + dtDuno.Rows[j][50].ToString() + ", " + dtDuno.Rows[j][48].ToString());
					}					
					else
					{
						for (int k = 0; k < 5; k++)
						{
							if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[LAV" + (k + 1).ToString() + "]"))
							{
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[LAV" + (k + 1).ToString() + "]", LAV[k].ToString());
							}
							else
							if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[Ngayvay" + (k + 1).ToString() + "]"))
							{
								if (ngayvay[k].ToString() == "")
								{
									doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Ngayvay" + (k + 1).ToString() + "]", "");
									continue;
								}
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Ngayvay" + (k + 1).ToString() + "]", Convert.ToDateTime(ngayvay[k].ToString()).ToString("dd/MM/yyyy"));
							}
							else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[Mucdichvay" + (k + 1).ToString() + "]"))
							{								
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Mucdichvay" + (k + 1).ToString() + "]", mucdichvay[k].ToString());
							}
							else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[Sotiengiaingan" + (k + 1).ToString() + "]"))
							{
								if (sotiengiaingan[k].ToString() == "0")
								{
									doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Sotiengiaingan" + (k + 1).ToString() + "]", "");
									continue;
								}
								
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Sotiengiaingan" + (k + 1).ToString() + "]", Int32.Parse(sotiengiaingan[k].ToString()).ToString("N0"));
							}
							else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[Dunohientai" + (k + 1).ToString() + "]"))
							{
								if (dunoht[k].ToString() == "0")
								{
									doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Dunohientai" + (k + 1).ToString() + "]", "");
									continue;

								}								
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Dunohientai" + (k + 1).ToString() + "]", Int32.Parse(dunoht[k].ToString()).ToString("N0"));

							}else
							if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[dientich" + (k + 1).ToString() + "]"))
							{
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[dientich" + (k + 1).ToString() + "]", dientich[k].ToString());
							}
							else
							if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[soqsdđ" + (k + 1).ToString() + "]"))
							{
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[soqsdđ" + (k + 1).ToString() + "]", soqsdđ[k].ToString());
							}
							else
							if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[diachits" + (k + 1).ToString() + "]"))
							{
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[diachits" + (k + 1).ToString() + "]", diachits[k].ToString());
							}
							else
							if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[loaidat" + (k + 1).ToString() + "]"))
							{
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[loaidat" + (k + 1).ToString() + "]", loaidat[k].ToString());
							}
							else
							if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[sothua" + (k + 1).ToString() + "]"))
							{
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[sothua" + (k + 1).ToString() + "]", sothua[k].ToString());
							}
							else
							if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[tobando" + (k + 1).ToString() + "]"))
							{
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[tobando" + (k + 1).ToString() + "]", tobando[k].ToString());
							}
						}
					
					}



				}
					 ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\12B\\" + dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString() + ".docx");
				//	else ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\DN\\" + dtTaisan.Rows[j][2].ToString() + "_" + dtTaisan.Rows[j][1].ToString() + "_" + dtTaisan.Rows[j][5].ToString() + ".docx");
				((_Document)doc).Close();
				((_Application)word).Quit();

				j = j + nextrow;
				if (j >= dtDuno.Rows.Count) break;
			}
			MessageBox.Show("OK XONG BB TS!");
		}

		private void button1_Click(object sender, EventArgs e)
		{

			for (int j = 0; j < dtDuno.Rows.Count; j++) // chạy bảng khách hàng đã phân công theo cán bộ
			{

				Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
				Document doc = new Document();
				string filename = "";
				string pa = System.Environment.CurrentDirectory;

				filename = (pa + "\\12A.docx");
				object missing = System.Type.Missing;
				doc = word.Documents.Open(Path.GetFullPath(filename),
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing);
				count = 0;
				LDS = new string[5] { "", "", "", "", "" };
				giatri = new string[5] { "", "", "", "", "" };
				mucdichvay = new string[5] { "", "", "", "", "" };
				giatritt = new string[5] { "", "", "", "", "" };
				dunohientai = 0;
				bool matchLav = false;
				for (int i = 1; i <= doc.Paragraphs.Count; i++)// mở chạy các đoạn trong word
				{
					label2.Text = dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString();

					int dem = j;
					//label2.Text = dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString();
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[tenkhachhang]"))
					{
						//MessageBox.Show(doc.Paragraphs[i].Range.Text);
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[tenkhachhang]", dtDuno.Rows[j][2].ToString());
					}



					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[LAV]"))
					{

						if (dtDuno.Rows[j][17].ToString().Trim() == "") break;
						if (!matchLav)//chưa gặp LAV thì tính 
						{
							dunohientai = Int32.Parse(dtDuno.Rows[j][17].ToString());
							LDS[0] = dtDuno.Rows[j][8].ToString();
							giatri[0] = dtDuno.Rows[j][13].ToString();
							mucdichvay[0] = dtDuno.Rows[j][58].ToString();
							giatritt[0] = dtDuno.Rows[j][17].ToString();

							matchLav = true; //set chạy rồi lần sau gặp LAV bỏ qua
							for (int z = j + 1; z <= dtDuno.Rows.Count - 1; z++)
							{
								if (dtDuno.Rows[z][3].ToString().Trim() == "") break;
								if (dtDuno.Rows[j][3].ToString() == dtDuno.Rows[z][3].ToString())
								{

									count++;
									dunohientai += Int32.Parse(dtDuno.Rows[z][17].ToString());

									LDS[count] = dtDuno.Rows[z][8].ToString();
									giatri[count] = dtDuno.Rows[z][13].ToString();
									mucdichvay[count] = dtDuno.Rows[z][58].ToString();
									giatritt[count] = dtDuno.Rows[z][17].ToString();

								}
								else break;

							}

						}
						label2.Text = "count: " + count + "-" + dtDuno.Rows[j][8].ToString() + " - dunohientai: " + giatritt[0].ToString();
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[LAV]", dtDuno.Rows[j][3].ToString());

					}

					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[ngayvay]"))
					{
						//  doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "ngayvay", dtDuno.Rows[j][4].ToString(), RegexOptions.IgnoreCase);
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[ngayvay]", Convert.ToDateTime(dtDuno.Rows[j][4].ToString()).ToString("dd/MM/yyyy"));

					}
					else
						if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[diachi]"))
					{

						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[diachi]", dtDuno.Rows[j][52].ToString() + ", " + dtDuno.Rows[j][50].ToString() + ", " + dtDuno.Rows[j][48].ToString());
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[dunohientai]"))
					{
						// doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "dunohientai", dunohientai.ToString("N0"), RegexOptions.IgnoreCase);
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[dunohientai]", dunohientai.ToString("N0"));

					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[sotiengiaingan]"))
					{
						int dnhientai = Int32.Parse(dtDuno.Rows[j][7].ToString());
						//  doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "sotiengiaingan", dnhientai.ToString("N0"), RegexOptions.IgnoreCase);
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[sotiengiaingan]", dnhientai.ToString("N0"));
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[mucdichvay]"))
					{
						//  doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "mucdichvay", dtDuno.Rows[j][58].ToString(), RegexOptions.IgnoreCase);
						doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[mucdichvay]", dtDuno.Rows[j][58].ToString());
					}
					else
					{
						for (int k = 0; k < 5; k++)
						{

							if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[LDS" + (k + 1).ToString() + "]"))
							{
								label1.Text += "LDS" + (k + 1).ToString() + ": " + LDS[k].ToString();
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[LDS" + (k + 1).ToString() + "]", LDS[k].ToString());
							}
							else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[Mucdichvay" + (k + 1).ToString() + "]"))
							{

								label1.Text += "Mucdichvay" + (k + 1).ToString() + ": " + mucdichvay[k].ToString();
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Mucdichvay" + (k + 1).ToString() + "]", mucdichvay[k].ToString());
							}
							else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[Giatri" + (k + 1).ToString() + "]"))
							{
								if (giatri[k].ToString() == "")
								{
									doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Giatri" + (k + 1).ToString() + "]", giatri[k].ToString());
									continue;
								}
								label1.Text += "Giatri" + (k + 1).ToString() + ": " + giatri[k].ToString();
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Giatri" + (k + 1).ToString() + "]", Int32.Parse(giatri[k].ToString()).ToString("N0"));
							}
							else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "\\[Dno" + (k + 1).ToString() + "]"))
							{
								if (giatritt[k].ToString() == "")
								{
									doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Dno" + (k + 1).ToString() + "]", giatritt[k].ToString());
									continue;

								}
								label1.Text += "Du no " + (k + 1).ToString() + ": " + giatritt[k].ToString() + "\n";
								doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("[Dno" + (k + 1).ToString() + "]", Int32.Parse(giatritt[k].ToString()).ToString("N0"));

							}
						}
					}



				}
					 ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\12A\\" + dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString() + ".docx");
				//	else ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\DN\\" + dtTaisan.Rows[j][2].ToString() + "_" + dtTaisan.Rows[j][1].ToString() + "_" + dtTaisan.Rows[j][5].ToString() + ".docx");
				((_Document)doc).Close();
				((_Application)word).Quit();

				j = j + count;
				if (j >= dtDuno.Rows.Count) break;

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
						myDataSet = new DataSet();
						sql = "SELECT * from [Sheet2$]";//bảng chọn mẫu
						using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql, conn))
						{
							cmd.Fill(myDataSet);
						}
						dtTaisan = myDataSet.Tables[0];
						conn.Close();

					}
					button1.Visible = true;
				}
			}
		}
	}

}
