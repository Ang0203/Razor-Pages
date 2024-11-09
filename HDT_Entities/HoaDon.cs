using System.Globalization;
using System.Reflection;

namespace HDT_22810201_Entities
{
	public class HoaDon
	{
		public string Ma { get; set; }
		public string Ngay { get; set; }
		public List<string[]> DanhSach { get; set; }
		public string TongSoLuong { get; set; }
		public string TongTien { get; set; }
		public HoaDon()
		{
			Ma = "";
			Ngay = "";
			TongSoLuong = "0";
			TongTien = "0";
			DanhSach = new List<string[]>();
		}

		private class SoSanhHelper : IComparer<HoaDon>
		{
			private readonly int _index;
			private readonly string _opt;
			public SoSanhHelper(int index, string opt)
			{
				_index = index;
				_opt = opt;
			}

			private int SoSanhAB(dynamic? a, dynamic? b)
			{
				if (a is null && b is null) return 0;
				else if (a is null) return -1;
				else if (b is null) return 1;

				if (_opt == "tang") return (a < b) ? -1 : (a > b) ? 1 : 0;
				else return (a > b) ? -1 : (a < b) ? 1 : 0;
			}
			public int Compare(HoaDon? x, HoaDon? y)
			{
				if (x is null && y is null) return 0;
				else if (x is null) return -1;
				else if (y is null) return 1;

				int ss = 0;
				PropertyInfo prop = typeof(HoaDon).GetProperties().ElementAt(_index);
				switch (_index)
				{
					case 1:
						DateTime a0 = DateTime.Parse((string)prop.GetValue(x)!, new CultureInfo("vi-VN"));
						DateTime b0 = DateTime.Parse((string)prop.GetValue(y)!, new CultureInfo("vi-VN"));
						ss = SoSanhAB(a0, b0);
						break;
					case 2:
						int a1 = ((List<string[]>)prop.GetValue(x)!).Count;
						int b1 = ((List<string[]>)prop.GetValue(y)!).Count;
						ss = SoSanhAB(a1, b1);
						break;
					case 3:
						ulong a2 = ulong.Parse((string)prop.GetValue(x)!);
						ulong b2 = ulong.Parse((string)prop.GetValue(y)!);
						ss = SoSanhAB(a2, b2);
						break;
					case 4:
						decimal a3 = decimal.Parse((string)prop.GetValue(x)!, new CultureInfo("vi-VN"));
						decimal b3 = decimal.Parse((string)prop.GetValue(y)!, new CultureInfo("vi-VN"));
						ss = SoSanhAB(a3, b3);
						break;
				}
				return (ss == 0) ? string.Compare(x.Ma, y.Ma) : ss;
			}
		}
		public static List<HoaDon> TaoThuNghiem()
		{
			string dt = DateTime.UtcNow.AddHours(7).ToString(new CultureInfo("vi-VN"));
			List<HoaDon> kq = new List<HoaDon>
			{
				new HoaDon()
				{
					Ma = "HDNSKR",
					Ngay = dt,
					DanhSach = new List<string[]>()
					{
						new string[5] { "1SKR", "22/07/0", "22/07/0", "1", "0" }
					}
				},
				new HoaDon()
				{
					Ma = "HDNANG",
					Ngay = dt,
					DanhSach = new List<string[]>()
					{
						new string[5] { "2ANG", "02/03/2003", "02/03/2103", "1", "0" }
					}
				},
				new HoaDon()
				{
					Ma = "HDNBOX",
					Ngay = dt,
					DanhSach = new List<string[]>()
					{
						new string[5] { "ABOX", "15/10/2023", "15/10/2024", "1", "1.000" },
						new string[5] { "BBOX", "15/10/2023", "15/10/2024", "3", "10.000" },
						new string[5] { "CBOX", "15/10/2023", "15/10/2024", "2", "5.000" }
					}
				},
				new HoaDon()
				{
					Ma = "HDNBOX2",
					Ngay = dt,
					DanhSach = new List<string[]>()
					{
						new string[5] { "ABOX", "16/10/2023", "16/10/2024", "2", "1.000" },
						new string[5] { "BBOX", "16/10/2023", "16/10/2024", "4", "5.000" },
						new string[5] { "CBOX", "16/10/2023", "16/10/2024", "6", "100.000" }
					}
				},
				new HoaDon()
				{
					Ma = "HDNNUOCNGOT",
					Ngay = dt,
					DanhSach = new List<string[]>()
					{
						new string[5] { "nuocngot1", "27/10/2023", "27/10/2024", "200", "15.000" },
						new string[5] { "nuocngot2", "15/10/2023", "15/10/2024", "300", "20.000" }
					}
				},
				new HoaDon()
				{
					Ma = "HDNNUOCNGOT2",
					Ngay = dt,
					DanhSach = new List<string[]>()
					{
						new string[5] { "nuocngot1", "18/10/2023", "18/10/2024", "150", "15.500" },
						new string[5] { "nuocngot2", "16/10/2023", "16/10/2024", "100", "15.000" }
					}
				},
				new HoaDon()
				{
					Ma = "HDNBANHKEO",
					Ngay = dt,
					DanhSach = new List<string[]>()
					{
						new string[5] { "BANHQUY", "11/10/2023", "11/10/2024", "1000", "5.000" },
						new string[5] { "NIEMVUI", "01/10/2023", "01/10/2024", "1", "1.000.000" },
						new string[5] { "KEO", "15/10/2023", "15/10/2024", "620", "3.000" }
					}
				},
				new HoaDon()
				{
					Ma = "HDNBANHKEO2",
					Ngay = dt,
					DanhSach = new List<string[]>()
					{
						new string[5] { "NIEMVUI", "02/10/2023", "02/10/2024", "152", "199.999" },
						new string[5] { "KEO", "16/10/2023", "16/10/2024", "230", "1.500" }
					}
				},
				new HoaDon()
				{
					Ma = "HDNBANHKEO3",
					Ngay = dt,
					DanhSach = new List<string[]>()
					{
						new string[5] { "NIEMVUI", "02/03/2003", "10/10/9999", "8", "0" }
					}
				}
			};
			foreach (var x in kq) x.TongLuongTien();

			return kq;
		}
		public static IComparer<HoaDon> SoSanh(string[] sapXep_HD)
		{
			int index = 0;
			foreach (var s in sapXep_HD)
			{
				if (s != "0") break;
				index++;
			}
			string opt = sapXep_HD[index];

			return new SoSanhHelper(index + 1, opt);
		}
		public void TaoMaVaNgay(char loai)
		{
			Ma = "HD" + loai;

			// chuỗi thời gian
			DateTime dt = DateTime.UtcNow.AddHours(7);
			Ma += ((DateTimeOffset)dt).ToUnixTimeSeconds().ToString();
			Ma += dt.Millisecond.ToString() + dt.Microsecond.ToString() + dt.Nanosecond.ToString();

			// 5 ký tự ngẫu nhiên
			string mold = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$%^&*()_-=+][}{|;':/.,<>?";
			string code = "";
			for (int i = 0; i < 5; i++) code += mold[new Random().Next(mold.Length)];

			// mã và ngày
			Ma += code;
			Ngay = dt.ToString(new CultureInfo("vi-VN"));
		}
		public void TongLuongTien()
		{
			if (DanhSach.Any())
			{
				ulong slSum = 0;
				decimal giaSum = 0;
				foreach (var x in DanhSach)
				{
					ulong uTemp = ulong.Parse(x[3]);
					slSum += uTemp;
					giaSum += uTemp * decimal.Parse(x[4], new CultureInfo("vi-VN"));
				}
				TongSoLuong = slSum.ToString();
				TongTien = giaSum.ToString("N0");
			}
		}
	}
}