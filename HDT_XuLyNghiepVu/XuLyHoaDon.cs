using System.Globalization;
using HDT_22810201_Entities;
using HDT_22810201_XuLyLuuTru;

namespace HDT_22810201_XuLyNghiepVu
{
	public class XuLyHoaDon : IXuLyHoaDon
	{
		private readonly ILuuTruNguoiDung<NguoiDung> _luuTruNguoiDung;
		private readonly ILuuTruSanPham<SanPham> _luuTruSanPham;
		private readonly ILuuTruHoaDon<HoaDon> _luuTruHoaDon;
		public XuLyHoaDon(ILuuTruNguoiDung<NguoiDung> luuTruNguoiDung, ILuuTruSanPham<SanPham> luuTruSanPham, ILuuTruHoaDon<HoaDon> luuTruHoaDon)
		{
			_luuTruNguoiDung = luuTruNguoiDung;
			_luuTruSanPham = luuTruSanPham;
			_luuTruHoaDon = luuTruHoaDon;
		}

		public List<HoaDon> DocDanhSach()
		{
			try
			{
				return _luuTruHoaDon.DocDanhSach();
			}
			catch (Exception ex)
			{
				_luuTruHoaDon.LuuLoi(ex);
				throw;
			}
		}
		public List<HoaDon> Tim(List<HoaDon> danhSach, string tenNguoiDung, string memento)
		{
			try
			{
				CaiDat caiDat = _luuTruNguoiDung.LayCaiDat(tenNguoiDung);
				Memento_HD mementoTemp = caiDat.LichSuTim_HD[caiDat.LichSuTim_HD["0"].IDHienTai];
				while (1 == 1)
				{
					if (mementoTemp.Loai == "nhap") danhSach.RemoveAll(c => c.Ma[2] == 'B');
					else danhSach.RemoveAll(c => c.Ma[2] == 'N');

					// Ngày 
					List<HoaDon> dsTemp;
					DateTime val, tar1, tar2;
					string del;
					string indicator = mementoTemp.Ngay[0].ToLower();
					if (indicator != "0")
					{
						dsTemp = new List<HoaDon>();
						if (indicator != "tk")
						{
							tar1 = DateTime.Parse(mementoTemp.Ngay[1], new CultureInfo("vi-VN"));

							foreach (var hoaDon in danhSach)
							{
								val = DateTime.Parse(hoaDon.Ngay.Substring(0, 10), new CultureInfo("vi-VN"));
								del = "";

								if (indicator == "cx" && val != tar1) del = "0";
								else if (indicator == "truoc" && val > tar1) del = "0";
								else if (indicator == "sau" && val < tar1) del = "0";

								if (del == "") dsTemp.Add(hoaDon);
							}
						}
						else
						{
							tar1 = DateTime.Parse(mementoTemp.Ngay[2], new CultureInfo("vi-VN"));
							tar2 = DateTime.Parse(mementoTemp.Ngay[3], new CultureInfo("vi-VN"));

							foreach (var hoaDon in danhSach)
							{
								val = DateTime.Parse(hoaDon.Ngay.Substring(0, 10), new CultureInfo("vi-VN"));
								del = "";

								if (tar1 == tar2 && val != tar1) del = "0";
								else if (tar1 < tar2 && (val < tar1 || val > tar2)) del = "0";
								else if (tar1 > tar2 && (val > tar1 || val < tar2)) del = "0";

								if (del == "") dsTemp.Add(hoaDon);
							}
						}
						danhSach = dsTemp;
					}

					// Tổng sản phẩm
					ulong valUlong, tarUlong1, tarUlong2;
					indicator = mementoTemp.TongSanPham[0].ToLower();
					if (indicator != "0")
					{
						dsTemp = new List<HoaDon>();
						if (indicator != "tk")
						{
							tarUlong1 = ulong.Parse(mementoTemp.TongSanPham[1]);

							foreach (var hoaDon in danhSach)
							{
								valUlong = ulong.Parse(hoaDon.TongSoLuong);
								del = "";

								if (indicator == "cx" && valUlong != tarUlong1) del = "0";
								else if (indicator == "truoc" && valUlong > tarUlong1) del = "0";
								else if (indicator == "sau" && valUlong < tarUlong1) del = "0";

								if (del == "") dsTemp.Add(hoaDon);
							}
						}
						else
						{
							tarUlong1 = ulong.Parse(mementoTemp.TongSanPham[2]);
							tarUlong2 = ulong.Parse(mementoTemp.TongSanPham[3]);

							foreach (var hoaDon in danhSach)
							{
								valUlong = ulong.Parse(hoaDon.TongSoLuong);
								del = "";

								if (tarUlong1 == tarUlong2 && valUlong != tarUlong1) del = "0";
								else if (tarUlong1 < tarUlong2 && (valUlong < tarUlong1 || valUlong > tarUlong2)) del = "0";
								else if (tarUlong1 > tarUlong2 && (valUlong > tarUlong1 || valUlong < tarUlong2)) del = "0";

								if (del == "") dsTemp.Add(hoaDon);
							}
						}
						danhSach = dsTemp;
					}

					// Tổng tiền
					decimal valDec, tarDec1, tarDec2;
					indicator = mementoTemp.TongTien[0].ToLower();
					if (indicator != "0")
					{
						dsTemp = new List<HoaDon>();
						if (indicator != "tk")
						{
							tarDec1 = decimal.Parse(mementoTemp.TongTien[1], new CultureInfo("vi-VN"));

							foreach (var hoaDon in danhSach)
							{
								valDec = decimal.Parse(hoaDon.TongTien, new CultureInfo("vi-VN"));
								del = "";

								if (indicator == "cx" && valDec != tarDec1) del = "0";
								else if (indicator == "truoc" && valDec > tarDec1) del = "0";
								else if (indicator == "sau" && valDec < tarDec1) del = "0";

								if (del == "") dsTemp.Add(hoaDon);
							}
						}
						else
						{
							tarDec1 = decimal.Parse(mementoTemp.TongTien[2], new CultureInfo("vi-VN"));
							tarDec2 = decimal.Parse(mementoTemp.TongTien[3], new CultureInfo("vi-VN"));

							foreach (var hoaDon in danhSach)
							{
								valDec = decimal.Parse(hoaDon.TongTien, new CultureInfo("vi-VN"));
								del = "";

								if (tarDec1 == tarDec2 && valDec != tarDec1) del = "0";
								else if (tarDec1 < tarDec2 && (valDec < tarDec1 || valDec > tarDec2)) del = "0";
								else if (tarDec1 > tarDec2 && (valDec > tarDec1 || valDec < tarDec2)) del = "0";

								if (del == "") dsTemp.Add(hoaDon);
							}
						}
						danhSach = dsTemp;
					}

					if (!danhSach.Any() || mementoTemp.IDHienTai == memento) break;
					else mementoTemp = caiDat.LichSuTim_HD[mementoTemp.IDSau];
				}

				return danhSach;
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				throw;
			}
		}
		public ServiceResult SapXepDanhSach(string tenNguoiDung, string? memento, string? loai, int hdMoiTrang, string[] sapXep_HD, Action<List<HoaDon>, int> traVe)
		{
			try
			{
				List<HoaDon> danhSach = DocDanhSach();
				if (danhSach.Any())
				{
					if (memento is not null && loai is null) danhSach = Tim(danhSach, tenNguoiDung, memento);
					// danh sách
					else if (loai == "nhap") danhSach.RemoveAll(c => c.Ma[2] == 'B');
					else danhSach.RemoveAll(c => c.Ma[2] == 'N');
					danhSach.Sort(HoaDon.SoSanh(sapXep_HD));
					// tổng trang
					hdMoiTrang = (hdMoiTrang <= 0) ? 7 : hdMoiTrang;
					int tongTrang = (int)Math.Ceiling((decimal)danhSach.Count / hdMoiTrang);
					tongTrang = (tongTrang == 0) ? 1 : tongTrang;

					traVe(danhSach, tongTrang);

				}
				else traVe(danhSach, 1);

				return new ServiceResult(true, "* Sắp xếp thành công!");
			}
			catch (Exception ex)
			{
				_luuTruHoaDon.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult ChiTiet(string ma, int spMoiTrang, string[] sapXep_SP, Action<List<SanPham>, int> traVe)
		{
			try
			{
				List<SanPham> dsSP = _luuTruSanPham.DocDanhSach();
				List<HoaDon> dsHD = DocDanhSach();
				List<SanPham> dsRes = new List<SanPham>();
				HoaDon hoaDon = dsHD.Find(c => c.Ma == ma)!;

				// danh sách
				foreach (var x in hoaDon.DanhSach)
				{
					SanPham sanPham = new SanPham();
					SanPham spTemp = dsSP.Find(c => c.Ma == x[0])!;
					sanPham.Ma = spTemp.Ma;
					sanPham.Ten = spTemp.Ten;
					sanPham.Loai = spTemp.Loai;
					sanPham.CongTy = spTemp.CongTy;
					sanPham.NgaySX.Add(ma, x[1]);
					sanPham.HanDung.Add(ma, x[2]);
					sanPham.SoLuong.Add(ma, x[3]);
					sanPham.Gia.Add(ma, x[4]);

					dsRes.Add(sanPham);
				}
				dsRes.Sort(SanPham.SoSanh(sapXep_SP));
				// tổng trang
				spMoiTrang = (spMoiTrang <= 0) ? 7 : spMoiTrang;
				int tongTrang = (int)Math.Ceiling((decimal)dsRes.Count / spMoiTrang);
				tongTrang = (tongTrang == 0) ? 1 : tongTrang;

				traVe(dsRes, tongTrang);

				return new ServiceResult(true, "* Lấy chi tiết thành công!");
			}
			catch (Exception ex)
			{
				_luuTruHoaDon.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult ThemNhapDS(string[] sapXep_HD_ThemNhap, Action<List<SanPham>, List<string>> traVe)
		{
			try
			{
				List<SanPham> danhSach = _luuTruSanPham.DocDanhSach();
				List<string> danhSach2 = new List<string>();

				if (danhSach.Any())
				{
					// xử lí danh sách
					foreach (var x in danhSach)
					{
						x.NgaySX.Clear();
						x.HanDung.Clear();
						x.SoLuong.Clear();
						x.Gia.Clear();
					}
					// sắp xếp và kết quả
					danhSach.Sort(SanPham.SoSanh(sapXep_HD_ThemNhap));
					danhSach2 = danhSach.Select(c => c.Ma).ToList();
				}
				traVe(danhSach, danhSach2);

				return new ServiceResult(true, "* Đã tạo danh sách thêm nhập thành công!");
			}
			catch (Exception ex)
			{
				_luuTruHoaDon.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		// DRY
		public ServiceResult ThemNhapXN(int spMoiTrang, string[] sapXep_SP, List<string> dlMa, List<string?[]> dlVao, Action<List<SanPham>, int> traVe)
		{
			try
			{
				List<SanPham> danhSach = _luuTruSanPham.DocDanhSach();
				List<SanPham> dsRes = new List<SanPham>();
				if (danhSach.Any() && dlVao.Any())
				{
					DateTime dt = DateTime.UtcNow.AddHours(7);
					// danh sách
					for (int i = 0; i < dlMa.Count; i++)
					{
						// kiểm tra null và lỗi
						if (dlVao[i].All(string.IsNullOrEmpty)) continue;
						if (dlVao[i].Any(string.IsNullOrEmpty))
							return new ServiceResult(false, $"* Vui lòng nhập đủ 4 mục cho mã \"{dlMa[i]}\"");
						else if (!dlVao[i][2]!.All(char.IsDigit) || !dlVao[i][3]!.All(char.IsDigit))
							return new ServiceResult(false, $"* Vui lòng chỉ nhập số cho số lượng và giá của mã \"{dlMa[i]}\"");
						// xử lí sản phẩm
						SanPham? sanPham = danhSach.Find(c => c.Ma == dlMa[i]);
						if (sanPham is not null)
						{
							// kiểm tra nsx và hd
							string[] dlVaoTemp = new string[4]
							{
								DateTime
								.Parse(dlVao[i][0]!, new CultureInfo("vi-VN"))
								.ToString(new CultureInfo("vi-VN"))
								.Substring(0, 10),
								DateTime
								.Parse(dlVao[i][1]!, new CultureInfo("vi-VN"))
								.ToString(new CultureInfo("vi-VN"))
								.Substring(0, 10),
								dlVao[i][2]!,
								decimal.Parse(dlVao[i][3]!, new CultureInfo("vi-VN")).ToString("N0")
							};
							if (DateTime.Parse(dlVaoTemp[0], new CultureInfo("vi-VN")) > dt)
								return new ServiceResult(false, $"* Ngày sản xuất: {dlVaoTemp[0]} là không hợp lệ!");
							else if (DateTime.Parse(dlVaoTemp[1], new CultureInfo("vi-VN")) <= dt)
								return new ServiceResult(false, $"* Hạn dùng: {dlVaoTemp[1]} là không hợp lệ!");
							// thực thi
							sanPham.NgaySX.Clear();
							sanPham.NgaySX.Add("", dlVaoTemp[0]);
							sanPham.HanDung.Clear();
							sanPham.HanDung.Add("", dlVaoTemp[1]);
							sanPham.SoLuong.Clear();
							sanPham.SoLuong.Add("", dlVaoTemp[2]);
							sanPham.Gia.Clear();
							sanPham.Gia.Add("", dlVaoTemp[3]);

							dsRes.Add(sanPham);
						}
					}
					dsRes.Sort(SanPham.SoSanh(sapXep_SP));
					// tổng trang
					spMoiTrang = (spMoiTrang <= 0) ? 7 : spMoiTrang;
					int tongTrang = (int)Math.Ceiling((decimal)dsRes.Count / spMoiTrang);
					tongTrang = (tongTrang == 0) ? 1 : tongTrang;

					traVe(dsRes, tongTrang);
				}
				else traVe(dsRes, 1);

				return new ServiceResult(true, "* Đã xác nhận thêm nhập thành công!");
			}
			catch (Exception ex)
			{
				_luuTruHoaDon.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult ThemNhap(List<string> dlMa, List<string?[]> dlVao)
		{
			try
			{
				List<SanPham> danhSach = _luuTruSanPham.DocDanhSach();
				if (danhSach.Any() && dlVao.Any())
				{
					DateTime dt = DateTime.UtcNow.AddHours(7);
					HoaDon hoaDon = new HoaDon();
					hoaDon.TaoMaVaNgay('N');
					for (int i = 0; i < dlMa.Count; i++)
					{
						if (dlVao[i].All(string.IsNullOrEmpty)) continue;
						if (dlVao[i].Any(string.IsNullOrEmpty))
							return new ServiceResult(false, $"* Vui lòng nhập đủ 4 mục cho mã \"{dlMa[i]}\"");
						else if (!dlVao[i][2]!.All(char.IsDigit) || !dlVao[i][3]!.All(char.IsDigit))
							return new ServiceResult(false, $"* Vui lòng chỉ nhập số cho số lượng và giá của mã \"{dlMa[i]}\"");
						// xử lí sản phẩm
						SanPham? sanPham = danhSach.Find(c => c.Ma == dlMa[i]);
						if (sanPham is not null)
						{
							string[] dlVaoTemp = new string[4]
							{
								DateTime
								.Parse(dlVao[i][0]!, new CultureInfo("vi-VN"))
								.ToString(new CultureInfo("vi-VN"))
								.Substring(0, 10),
								DateTime
								.Parse(dlVao[i][1]!, new CultureInfo("vi-VN"))
								.ToString(new CultureInfo("vi-VN"))
								.Substring(0, 10),
								dlVao[i][2]!,
								decimal.Parse(dlVao[i][3]!, new CultureInfo("vi-VN")).ToString("N0")
							};
							if (DateTime.Parse(dlVaoTemp[0], new CultureInfo("vi-VN")) > dt)
								return new ServiceResult(false, $"* Ngày sản xuất: {dlVaoTemp[0]} là không hợp lệ!");
							else if (DateTime.Parse(dlVaoTemp[1], new CultureInfo("vi-VN")) <= dt)
								return new ServiceResult(false, $"* Hạn dùng: {dlVaoTemp[1]} là không hợp lệ!");
							danhSach.Remove(sanPham);
							sanPham.NgaySX.Add(hoaDon.Ma, dlVaoTemp[0]);
							sanPham.HanDung.Add(hoaDon.Ma, dlVaoTemp[1]);
							sanPham.SoLuong.Add(hoaDon.Ma, dlVaoTemp[2]);
							sanPham.Gia.Add(hoaDon.Ma, dlVaoTemp[3]);
							danhSach.Add(sanPham);
							hoaDon.DanhSach.Add(new string[5] { dlMa[i], dlVaoTemp[0], dlVaoTemp[1], dlVaoTemp[2], dlVaoTemp[3] });
						}
					}
					hoaDon.TongLuongTien();
					_luuTruSanPham.LuuDanhSach(danhSach);
					_luuTruHoaDon.ThemHoaDon(hoaDon);
				}

				return new ServiceResult(true, "* Đã thêm hóa đơn nhập thành công!");
			}
			catch (Exception ex)
			{
				_luuTruHoaDon.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult ThemBanDS(string[] sapXep_HD_ThemBan, Action<List<SanPham>, List<string>> traVe)
		{
			try
			{
				List<SanPham> danhSach = _luuTruSanPham.DocDanhSach();
				List<string> danhSach2 = new List<string>();
				List<SanPham> dsRes = new List<SanPham>();

				if (danhSach.Any())
				{
					// xử lí danh sách
					foreach (var x in danhSach)
					{
						KeyValuePair<string, string> kvp = x.TongLuongTien();
						if (kvp.Key == "0") continue;
						x.NgaySX.Clear();
						x.HanDung.Clear();
						x.SoLuong.Clear();
						x.Gia.Clear();
						x.SoLuong.Add("", kvp.Key);
						dsRes.Add(x);
					}
					// sắp xếp và kết quả
					dsRes.Sort(SanPham.SoSanh(sapXep_HD_ThemBan));
					danhSach2 = dsRes.Select(c => c.Ma).ToList();
				}
				traVe(dsRes, danhSach2);

				return new ServiceResult(true, "* Đã tạo danh sách thêm bán thành công!");
			}
			catch (Exception ex)
			{
				_luuTruHoaDon.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		// DRY
		public ServiceResult ThemBanXN(int spMoiTrang, string[] sapXep_HD_ThemBan, List<string> dlMa, List<string?> dlVao, Action<List<SanPham>, int> traVe)
		{
			try
			{
				List<SanPham> danhSach = _luuTruSanPham.DocDanhSach();
				List<SanPham> dsRes = new List<SanPham>();
				if (danhSach.Any() && dlVao.Any())
				{
					DateTime dt = DateTime.UtcNow.AddHours(7);
					// danh sách
					for (int i = 0; i < dlMa.Count; i++)
					{
						// kiểm tra null và lỗi
						if (string.IsNullOrEmpty(dlVao[i]) || dlVao[i] == "0") continue;
						else if (!dlVao[i]!.All(char.IsDigit)) continue;
						// xử lí sản phẩm
						SanPham? sanPham = danhSach.Find(c => c.Ma == dlMa[i]);
						if (sanPham is not null && sanPham.SoLuong.Any())
						{
							// kiểm tra số lượng
							KeyValuePair<string, string> kvp = sanPham.TongLuongTien();
							if (ulong.Parse(kvp.Key) < ulong.Parse(dlVao[i]!)) continue;
							sanPham.NgaySX.Clear();
							sanPham.HanDung.Clear();
							sanPham.SoLuong.Clear();
							sanPham.Gia.Clear();
							sanPham.SoLuong.Add("", dlVao[i]!);

							dsRes.Add(sanPham);
						}
					}
					dsRes.Sort(SanPham.SoSanh(sapXep_HD_ThemBan));
					// tổng trang
					spMoiTrang = (spMoiTrang <= 0) ? 7 : spMoiTrang;
					int tongTrang = (int)Math.Ceiling((decimal)dsRes.Count / spMoiTrang);
					tongTrang = (tongTrang == 0) ? 1 : tongTrang;

					traVe(dsRes, tongTrang);
				}
				else traVe(dsRes, 1);

				return new ServiceResult(true, "* Đã xác nhận thêm bán thành công!");
			}
			catch (Exception ex)
			{
				_luuTruHoaDon.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult ThemBan(List<string> dlMa, List<string?> dlVao)
		{
			try
			{
				List<SanPham> danhSach = _luuTruSanPham.DocDanhSach();
				if (danhSach.Any() && dlVao.Any())
				{
					DateTime dt = DateTime.UtcNow.AddHours(7);
					HoaDon hoaDon = new HoaDon();
					hoaDon.TaoMaVaNgay('B');
					for (int i = 0; i < dlMa.Count; i++)
					{
						// kiểm tra null và lỗi
						if (string.IsNullOrEmpty(dlVao[i]) || dlVao[i] == "0") continue;
						else if (!dlVao[i]!.All(char.IsDigit)) continue;
						// xử lí sản phẩm
						SanPham? sanPham = danhSach.Find(c => c.Ma == dlMa[i]);
						if (sanPham is not null && sanPham.SoLuong.Any())
						{
							// khởi tạo
							danhSach.Remove(sanPham);
							SanPham spTemp = sanPham;
							ulong soNhap = ulong.Parse(dlVao[i]!);
							// kiểm tra số lượng
							KeyValuePair<string, string> kvp = sanPham.TongLuongTien();
							if (ulong.Parse(kvp.Key) < ulong.Parse(dlVao[i]!)) continue;
							// kiểm tra hạn dùng
							sanPham.HanDung = sanPham.HanDung
								.OrderBy(c => DateTime.Parse(c.Value, new CultureInfo("vi-VN")))
								.ToDictionary(c => c.Key, c => c.Value);
							foreach (var x in sanPham.HanDung)
							{
								if (dt >= DateTime.Parse(x.Value, new CultureInfo("vi-VN"))) continue;
								// trừ số lượng
								ulong soTru = ulong.Parse(sanPham.SoLuong[x.Key]);
								if (soTru <= soNhap)
								{
									hoaDon.DanhSach.Add(new string[5] { dlMa[i], spTemp.NgaySX[x.Key], spTemp.HanDung[x.Key], soTru.ToString(), spTemp.Gia[x.Key] });
									spTemp.NgaySX.Remove(x.Key);
									spTemp.HanDung.Remove(x.Key);
									spTemp.SoLuong.Remove(x.Key);
									spTemp.Gia.Remove(x.Key);
									soNhap -= soTru;
								}
								else
								{
									hoaDon.DanhSach.Add(new string[5] { dlMa[i], spTemp.NgaySX[x.Key], spTemp.HanDung[x.Key], soNhap.ToString(), spTemp.Gia[x.Key] });
									spTemp.SoLuong.Remove(x.Key);
									spTemp.SoLuong.Add(x.Key, (soTru - soNhap).ToString());
									break;
								}
							}
							danhSach.Add(spTemp);
						}
					}
					hoaDon.TongLuongTien();
					_luuTruSanPham.LuuDanhSach(danhSach);
					_luuTruHoaDon.ThemHoaDon(hoaDon);
				}

				return new ServiceResult(true, "* Đã thêm hóa đơn thành công!");
			}
			catch (Exception ex)
			{
				_luuTruHoaDon.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult KiemTraHoaDon(string? ma)
		{
			try
			{
				if (string.IsNullOrEmpty(ma) || !_luuTruHoaDon.KiemTraHoaDon(ma)) return new ServiceResult(false, "* Mã hóa đơn không tồn tại.");

				return new ServiceResult(true, "* Mã hóa đơn có tồn tại!");
			}
			catch (Exception ex)
			{
				_luuTruHoaDon.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult KiemTraLichSu(string tenNguoiDung, string? memento)
		{
			try
			{
				if (memento == "0")
					return new ServiceResult(true, "* Memento có tồn tại!");
				else if (string.IsNullOrEmpty(memento) || memento == "curr" || !_luuTruNguoiDung.LayCaiDat(tenNguoiDung).LichSuTim_HD.ContainsKey(memento))
					return new ServiceResult(false, "* Memento không tồn tại.");

				return new ServiceResult(true, "* Memento có tồn tại!");
			}
			catch (Exception ex)
			{
				_luuTruNguoiDung.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
	}
}