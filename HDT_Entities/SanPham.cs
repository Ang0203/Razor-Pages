using System.Globalization;
using System.Reflection;

namespace HDT_22810201_Entities
{
	public class SanPham
	{
		public string Ma { get; set; } = string.Empty;
		public string Ten { get; set; } = string.Empty;
		public string Loai { get; set; } = string.Empty;
		public string CongTy { get; set; } = string.Empty;
		public Dictionary<string, string> NgaySX { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string> HanDung { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string> SoLuong { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string> Gia { get; set; } = new Dictionary<string, string>();

		private class SoSanhHelper : IComparer<SanPham>
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

				// kiểm tra string
				if (a is string && _opt == "tang") return string.Compare(a, b);
				else if (a is string) return string.Compare(b, a);

				// kiểm tra loại khác
				if (_opt == "tang") return (a < b) ? -1 : (a > b) ? 1 : 0;
				else return (a > b) ? -1 : (a < b) ? 1 : 0;
			}
			public int Compare(SanPham? x, SanPham? y)
			{
				if (x is null && y is null) return 0;
				else if (x is null) return -1;
				else if (y is null) return 1;

				int ss = 0;
				PropertyInfo prop = typeof(SanPham).GetProperties().ElementAt(_index);
				switch (_index)
				{
					case 0 or 1 or 2 or 3:
						string a0 = (string)prop.GetValue(x)!;
						string b0 = (string)prop.GetValue(y)!;
						ss = SoSanhAB(a0, b0);
						break;
					case 4 or 5:
						DateTime a1 = ((Dictionary<string, string>)prop.GetValue(x)!)
							.Values
							.Select(c => DateTime.Parse(c, new CultureInfo("vi-VN")))
							.Max();
						DateTime b1 = ((Dictionary<string, string>)prop.GetValue(y)!)
							.Values
							.Select(c => DateTime.Parse(c, new CultureInfo("vi-VN")))
							.Max();
						ss = SoSanhAB(a1, b1);
						break;
					case 6:
						int a2 = ((Dictionary<string, string>)prop.GetValue(x)!)
							.Values.Select(int.Parse).Max();
						int b2 = ((Dictionary<string, string>)prop.GetValue(y)!)
							.Values.Select(int.Parse).Max();
						ss = SoSanhAB(a2, b2);
						break;
					case 7:
						decimal a3 = ((Dictionary<string, string>)prop.GetValue(x)!)
							.Values.Select(c => decimal.Parse(c, new CultureInfo("vi-VN"))).Max();
						decimal b3 = ((Dictionary<string, string>)prop.GetValue(y)!)
							.Values.Select(c => decimal.Parse(c, new CultureInfo("vi-VN"))).Max();
						ss = SoSanhAB(a3, b3);
						break;
				}
				return (ss == 0) ? string.Compare(x.Ma, y.Ma) : ss;
			}
		}
		public static List<SanPham> TaoThuNghiem()
		{
			List<SanPham> kq = new List<SanPham>
			{
				new SanPham()
				{
					Ma = "1SKR",
					Ten = "Y.S",
					Loai = "Thử Nghiệm",
					CongTy = "Y.V",
					NgaySX = new Dictionary<string, string> { { "HDNSKR", "22/07/0" } },
					HanDung = new Dictionary<string, string> { { "HDNSKR", "22/07/0" } },
					SoLuong = new Dictionary<string, string> { { "HDNSKR", "1" } },
					Gia = new Dictionary<string, string> { { "HDNSKR", "0" } }
				},
				new SanPham()
				{
					Ma = "2ANG",
					Ten = "Ang",
					Loai = "Thử Nghiệm",
					CongTy = "A.T",
					NgaySX = new Dictionary<string, string> { { "HDNANG", "02/03/2003" } },
					HanDung = new Dictionary<string, string> { { "HDNANG", "02/03/2103" } },
					SoLuong = new Dictionary<string, string> { { "HDNANG", "1" } },
					Gia = new Dictionary<string, string> { { "HDNANG", "0" } }
				},
				new SanPham()
				{
					Ma = "ABOX",
					Ten = "Hộp A",
					Loai = "Hộp",
					CongTy = "Hộp Các-tông",
					NgaySX = new Dictionary<string, string> { { "HDNBOX", "15/10/2023" }, { "HDNBOX2", "16/10/2023" } },
					HanDung = new Dictionary<string, string> { { "HDNBOX", "15/10/2024" }, { "HDNBOX2", "16/10/2024" } },
					SoLuong = new Dictionary<string, string> { { "HDNBOX", "1" }, { "HDNBOX2", "2" } },
					Gia = new Dictionary<string, string> { { "HDNBOX", "1.000" }, { "HDNBOX2", "1.000" } }
				},
				new SanPham()
				{
					Ma = "BBOX",
					Ten = "Hộp B",
					Loai = "Hộp",
					CongTy = "Hộp Các-tông",
					NgaySX = new Dictionary<string, string> { { "HDNBOX", "15/10/2023" }, { "HDNBOX2", "16/10/2023" } },
					HanDung = new Dictionary<string, string> { { "HDNBOX", "15/10/2024" }, { "HDNBOX2", "16/10/2024" } },
					SoLuong = new Dictionary<string, string> { { "HDNBOX", "3" }, { "HDNBOX2", "4" } },
					Gia = new Dictionary<string, string> { { "HDNBOX", "10.000" }, { "HDNBOX2", "5.000" } }
				},
				new SanPham()
				{
					Ma = "CBOX",
					Ten = "Hộp C",
					Loai = "Hộp",
					CongTy = "Hộp Các-tông",
					NgaySX = new Dictionary<string, string> { { "HDNBOX", "15/10/2023" }, { "HDNBOX2", "16/10/2023" } },
					HanDung = new Dictionary<string, string> { { "HDNBOX", "15/10/2024" }, { "HDNBOX2", "16/10/2024" } },
					SoLuong = new Dictionary<string, string> { { "HDNBOX", "2" }, { "HDNBOX2", "6" } },
					Gia = new Dictionary<string, string> { { "HDNBOX", "5.000" }, { "HDNBOX2", "100.000" } }
				},
				new SanPham()
				{
					Ma = "nuocngot1",
					Ten = "Coca Cola",
					Loai = "Nước ngọt",
					CongTy = "Hộp Các-tông",
					NgaySX = new Dictionary<string, string> { { "HDNNUOCNGOT", "27/10/2023" }, { "HDNNUOCNGOT2", "18/10/2023" } },
					HanDung = new Dictionary<string, string> { { "HDNNUOCNGOT", "27/10/2024" }, { "HDNNUOCNGOT2", "18/10/2024" } },
					SoLuong = new Dictionary<string, string> { { "HDNNUOCNGOT", "200" }, { "HDNNUOCNGOT2", "150" } },
					Gia = new Dictionary<string, string> { { "HDNNUOCNGOT", "15.000" }, { "HDNNUOCNGOT2", "15.500" } }
				},
				new SanPham()
				{
					Ma = "nuocngot2",
					Ten = "Pepsi",
					Loai = "Nước ngọt",
					CongTy = "PepsiCo",
					NgaySX = new Dictionary<string, string> { { "HDNNUOCNGOT", "15/10/2023" }, { "HDNNUOCNGOT2", "16/10/2023" } },
					HanDung = new Dictionary<string, string> { { "HDNNUOCNGOT", "15/10/2024" }, { "HDNNUOCNGOT2", "16/10/2024" } },
					SoLuong = new Dictionary<string, string> { { "HDNNUOCNGOT", "300" }, { "HDNNUOCNGOT2", "100" } },
					Gia = new Dictionary<string, string> { { "HDNNUOCNGOT", "20.000" }, { "HDNNUOCNGOT2", "15.000" } }
				},
				new SanPham()
				{
					Ma = "BANHQUY",
					Ten = "Bánh quy",
					Loai = "Bánh kẹo",
					CongTy = "ABCXYZ",
					NgaySX = new Dictionary<string, string> { { "HDNBANHKEO", "11/10/2023" } },
					HanDung = new Dictionary<string, string> { { "HDNBANHKEO", "11/10/2024" } },
					SoLuong = new Dictionary<string, string> { { "HDNBANHKEO", "1000" } },
					Gia = new Dictionary<string, string> { { "HDNBANHKEO", "5.000" } }
				},
				new SanPham()
				{
					Ma = "NIEMVUI",
					Ten = "Niềm vui",
					Loai = "Bánh kẹo",
					CongTy = "Luôn vui tươi :D",
					NgaySX = new Dictionary<string, string> { { "HDNBANHKEO", "01/10/2023" }, { "HDNBANHKEO2", "02/10/2023" }, { "HDNBANHKEO3", "02/03/2003" } },
					HanDung = new Dictionary<string, string> { { "HDNBANHKEO", "01/10/2024" }, { "HDNBANHKEO2", "02/10/2024" }, { "HDNBANHKEO3", "10/10/9999" } },
					SoLuong = new Dictionary<string, string> { { "HDNBANHKEO", "1" }, { "HDNBANHKEO2", "152" }, { "HDNBANHKEO3", "8" } },
					Gia = new Dictionary<string, string> { { "HDNBANHKEO", "1.000.000" }, { "HDNBANHKEO2", "199.999" }, { "HDNBANHKEO3", "0" } }
				},
				new SanPham()
				{
					Ma = "KEO",
					Ten = "Kẹo ngọt",
					Loai = "Bánh kẹo",
					CongTy = "Ngon lành",
					NgaySX = new Dictionary<string, string> { { "HDNBANHKEO", "15/10/2023" }, { "HDNBANHKEO2", "16/10/2023" } },
					HanDung = new Dictionary<string, string> { { "HDNBANHKEO", "15/10/2024" }, { "HDNBANHKEO2", "16/10/2024" } },
					SoLuong = new Dictionary<string, string> { { "HDNBANHKEO", "620" }, { "HDNBANHKEO2", "230" } },
					Gia = new Dictionary<string, string> { { "HDNBANHKEO", "3.000" }, { "HDNBANHKEO2", "1.500" } }
				}
			};

			return kq;
		}
		public static List<SanPham> SapXepHienThi(List<SanPham> danhSach, string[] sapXep_SP)
		{
			// lấy vị trí và loại sắp xếp
			int index = 0;
			foreach (var s in sapXep_SP)
			{
				if (s != "0") break;
				index++;
			}
			string opt = sapXep_SP[index];

			// sắp xếp danh sách
			danhSach.Sort(new SoSanhHelper(index, opt));

			// sắp xếp hiển thị
			index = (index >= 4) ? index : 5;
			PropertyInfo prop = typeof(SanPham).GetProperties().ElementAt(index);
			List<SanPham> dsTemp = new List<SanPham>();
			foreach (var matHang in danhSach)
			{
				SanPham spTemp = new SanPham()
				{
					Ma = matHang.Ma,
					Ten = matHang.Ten,
					Loai = matHang.Loai,
					CongTy = matHang.CongTy
				};
				List<string> keyID = new List<string>();
				switch (index)
				{
					case 4 or 5:
						if (opt == "tang")
							keyID = ((Dictionary<string, string>)prop.GetValue(matHang)!)
								.OrderBy(c => DateTime.Parse(c.Value, new CultureInfo("vi-VN")))
								.ToDictionary(c => c.Key, c => c.Value)
								.Keys.ToList();
						else
							keyID = ((Dictionary<string, string>)prop.GetValue(matHang)!)
								.OrderByDescending(c => DateTime.Parse(c.Value, new CultureInfo("vi-VN")))
								.ToDictionary(c => c.Key, c => c.Value)
								.Keys.ToList();
						break;
					case 6:
						if (opt == "tang")
							keyID = ((Dictionary<string, string>)prop.GetValue(matHang)!)
								.OrderBy(c => int.Parse(c.Value))
								.ToDictionary(c => c.Key, c => c.Value)
								.Keys.ToList();
						else
							keyID = ((Dictionary<string, string>)prop.GetValue(matHang)!)
								.OrderByDescending(c => int.Parse(c.Value))
								.ToDictionary(c => c.Key, c => c.Value)
								.Keys.ToList();
						break;
					case 7:
						if (opt == "tang")
							keyID = ((Dictionary<string, string>)prop.GetValue(matHang)!)
								.OrderBy(c => decimal.Parse(c.Value, new CultureInfo("vi-VN")))
								.ToDictionary(c => c.Key, c => c.Value)
								.Keys.ToList();
						else
							keyID = ((Dictionary<string, string>)prop.GetValue(matHang)!)
								.OrderByDescending(c => decimal.Parse(c.Value, new CultureInfo("vi-VN")))
								.ToDictionary(c => c.Key, c => c.Value)
								.Keys.ToList();
						break;
				}
				foreach (var key in keyID)
				{
					spTemp.NgaySX.Add(key, matHang.NgaySX[key]);
					spTemp.HanDung.Add(key, matHang.HanDung[key]);
					spTemp.SoLuong.Add(key, matHang.SoLuong[key]);
					spTemp.Gia.Add(key, matHang.Gia[key]);
				}
				dsTemp.Add(spTemp);
			}

			return dsTemp;
		}
		public static IComparer<SanPham> SoSanh(string[] sapXep_SP)
		{
			int index = 0;
			foreach (var s in sapXep_SP)
			{
				if (s != "0") break;
				index++;
			}
			string opt = sapXep_SP[index];

			return new SoSanhHelper(index, opt);
		}
		public KeyValuePair<string, string> TongLuongTien()
		{
			DateTime dtNow = DateTime.UtcNow.AddHours(7);
			ulong slSum = 0;
			decimal giaSum = 0;
			foreach (var x in SoLuong)
			{
				// kiểm tra hạn dùng
				DateTime dtTemp = DateTime.Parse(HanDung[x.Key]);
				if (dtTemp <= dtNow) continue;

				// tính toán
				ulong uTemp = ulong.Parse(x.Value);
				slSum += uTemp;
				giaSum += uTemp * decimal.Parse(Gia[x.Key], new CultureInfo("vi-VN"));
			}
			string slSumString = slSum.ToString();
			string giaSumSring = giaSum.ToString("N0");
			return new KeyValuePair<string, string>(slSumString, giaSumSring);
		}
	}
}