using System.Globalization;
using HDT_22810201_Entities;
using HDT_22810201_XuLyLuuTru;

namespace HDT_22810201_XuLyNghiepVu
{
	public class XuLySanPham : IXuLySanPham
	{
		private readonly ILuuTruNguoiDung<NguoiDung> _luuTruNguoiDung;
		private readonly ILuuTruSanPham<SanPham> _luuTruSanPham;
		public XuLySanPham(ILuuTruNguoiDung<NguoiDung> luuTruNguoiDung, ILuuTruSanPham<SanPham> luuTruSanPham)
		{
			_luuTruNguoiDung = luuTruNguoiDung;
			_luuTruSanPham = luuTruSanPham;
		}

		public List<SanPham> DocDanhSach()
		{
			try
			{
				return _luuTruSanPham.DocDanhSach();
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				throw;
			}
		}
		// code ác mộng :(
		public List<SanPham> Tim(List<SanPham> danhSach, string tenNguoiDung, string memento)
		{
			try
			{
				CaiDat caiDat = _luuTruNguoiDung.LayCaiDat(tenNguoiDung);
				Memento_SP mementoTemp = caiDat.LichSuTim_SP[caiDat.LichSuTim_SP["0"].IDHienTai];
				while (1 == 1)
				{
					if (!string.IsNullOrEmpty(mementoTemp.Ma))
						danhSach.RemoveAll(c => !c.Ma.ToLower().Contains(mementoTemp.Ma.ToLower()));
					if (!string.IsNullOrEmpty(mementoTemp.Ten))
						danhSach.RemoveAll(c => !c.Ten.ToLower().Contains(mementoTemp.Ten.ToLower()));
					if (!string.IsNullOrEmpty(mementoTemp.Loai))
						danhSach.RemoveAll(c => !c.Loai.ToLower().Contains(mementoTemp.Loai.ToLower()));
					if (!string.IsNullOrEmpty(mementoTemp.CongTy))
						danhSach.RemoveAll(c => !c.CongTy.ToLower().Contains(mementoTemp.CongTy.ToLower()));

					// Ngày SX
					List<SanPham> dsTemp;
					SanPham mhTemp;
					DateTime val, tar1, tar2;
					string del;
					string indicator = mementoTemp.NgaySX[0].ToLower();
					if (indicator != "0")
					{
						dsTemp = new List<SanPham>();
						if (indicator != "tk")
						{
							tar1 = DateTime.Parse(mementoTemp.NgaySX[1], new CultureInfo("vi-VN"));

							foreach (var matHang in danhSach)
							{
								mhTemp = matHang;
								foreach (var x in matHang.NgaySX)
								{
									val = DateTime.Parse(x.Value, new CultureInfo("vi-VN"));
									del = "";

									if (indicator == "cx" && val != tar1) del = x.Key;
									else if (indicator == "truoc" && val > tar1) del = x.Key;
									else if (indicator == "sau" && val < tar1) del = x.Key;

									if (del != "")
									{
										mhTemp.NgaySX.Remove(del);
										mhTemp.HanDung.Remove(del);
										mhTemp.SoLuong.Remove(del);
										mhTemp.Gia.Remove(del);
									}
								}
								if (mhTemp.NgaySX.Count != 0) dsTemp.Add(mhTemp);
							}
						}
						else
						{
							tar1 = DateTime.Parse(mementoTemp.NgaySX[2], new CultureInfo("vi-VN"));
							tar2 = DateTime.Parse(mementoTemp.NgaySX[3], new CultureInfo("vi-VN"));

							foreach (var matHang in danhSach)
							{
								mhTemp = matHang;
								foreach (var x in matHang.NgaySX)
								{
									val = DateTime.Parse(x.Value, new CultureInfo("vi-VN"));
									del = "";

									if (tar1 == tar2 && val != tar1) del = x.Key;
									else if (tar1 < tar2 && (val < tar1 || val > tar2)) del = x.Key;
									else if (tar1 > tar2 && (val > tar1 || val < tar2)) del = x.Key;

									if (del != "")
									{
										mhTemp.NgaySX.Remove(del);
										mhTemp.HanDung.Remove(del);
										mhTemp.SoLuong.Remove(del);
										mhTemp.Gia.Remove(del);
									}
								}
								if (mhTemp.NgaySX.Count != 0) dsTemp.Add(mhTemp);
							}
						}
						danhSach = dsTemp;
					}

					// Hạn dùng
					indicator = mementoTemp.HanDung[0].ToLower();
					if (indicator != "0")
					{
						dsTemp = new List<SanPham>();
						if (indicator != "tk")
						{
							tar1 = DateTime.Parse(mementoTemp.HanDung[1], new CultureInfo("vi-VN"));

							foreach (var matHang in danhSach)
							{
								mhTemp = matHang;
								foreach (var x in matHang.HanDung)
								{
									val = DateTime.Parse(x.Value, new CultureInfo("vi-VN"));
									del = "";

									if (indicator == "cx" && val != tar1) del = x.Key;
									else if (indicator == "truoc" && val > tar1) del = x.Key;
									else if (indicator == "sau" && val < tar1) del = x.Key;

									if (del != "")
									{
										mhTemp.NgaySX.Remove(del);
										mhTemp.HanDung.Remove(del);
										mhTemp.SoLuong.Remove(del);
										mhTemp.Gia.Remove(del);
									}
								}
								if (mhTemp.HanDung.Count != 0) dsTemp.Add(mhTemp);
							}
						}
						else
						{
							tar1 = DateTime.Parse(mementoTemp.HanDung[2], new CultureInfo("vi-VN"));
							tar2 = DateTime.Parse(mementoTemp.HanDung[3], new CultureInfo("vi-VN"));

							foreach (var matHang in danhSach)
							{
								mhTemp = matHang;
								foreach (var x in matHang.HanDung)
								{
									val = DateTime.Parse(x.Value, new CultureInfo("vi-VN"));
									del = "";

									if (tar1 == tar2 && val != tar1) del = x.Key;
									else if (tar1 < tar2 && (val < tar1 || val > tar2)) del = x.Key;
									else if (tar1 > tar2 && (val > tar1 || val < tar2)) del = x.Key;

									if (del != "")
									{
										mhTemp.NgaySX.Remove(del);
										mhTemp.HanDung.Remove(del);
										mhTemp.SoLuong.Remove(del);
										mhTemp.Gia.Remove(del);
									}
								}
								if (mhTemp.HanDung.Count != 0) dsTemp.Add(mhTemp);
							}
						}
						danhSach = dsTemp;
					}

					// Số lượng
					int valInt, tarInt1, tarInt2;
					indicator = mementoTemp.SoLuong[0].ToLower();
					if (indicator != "0")
					{
						dsTemp = new List<SanPham>();
						if (indicator != "tk")
						{
							tarInt1 = int.Parse(mementoTemp.SoLuong[1], new CultureInfo("vi-VN"));

							foreach (var matHang in danhSach)
							{
								mhTemp = matHang;
								foreach (var x in matHang.SoLuong)
								{
									valInt = int.Parse(x.Value, new CultureInfo("vi-VN"));
									del = "";

									if (indicator == "cx" && valInt != tarInt1) del = x.Key;
									else if (indicator == "truoc" && valInt > tarInt1) del = x.Key;
									else if (indicator == "sau" && valInt < tarInt1) del = x.Key;

									if (del != "")
									{
										mhTemp.NgaySX.Remove(del);
										mhTemp.HanDung.Remove(del);
										mhTemp.SoLuong.Remove(del);
										mhTemp.Gia.Remove(del);
									}
								}
								if (mhTemp.SoLuong.Count != 0) dsTemp.Add(mhTemp);
							}
						}
						else
						{
							tarInt1 = int.Parse(mementoTemp.SoLuong[2], new CultureInfo("vi-VN"));
							tarInt2 = int.Parse(mementoTemp.SoLuong[3], new CultureInfo("vi-VN"));

							foreach (var matHang in danhSach)
							{
								mhTemp = matHang;
								foreach (var x in matHang.SoLuong)
								{
									valInt = int.Parse(x.Value, new CultureInfo("vi-VN"));
									del = "";

									if (tarInt1 == tarInt2 && valInt != tarInt1) del = x.Key;
									else if (tarInt1 < tarInt2 && (valInt < tarInt1 || valInt > tarInt2)) del = x.Key;
									else if (tarInt1 > tarInt2 && (valInt > tarInt1 || valInt < tarInt2)) del = x.Key;

									if (del != "")
									{
										mhTemp.NgaySX.Remove(del);
										mhTemp.HanDung.Remove(del);
										mhTemp.SoLuong.Remove(del);
										mhTemp.Gia.Remove(del);
									}
								}
								if (mhTemp.SoLuong.Count != 0) dsTemp.Add(mhTemp);
							}
						}
						danhSach = dsTemp;
					}

					// Giá
					decimal valDec, tarDec1, tarDec2;
					indicator = mementoTemp.Gia[0].ToLower();
					if (indicator != "0")
					{
						dsTemp = new List<SanPham>();
						if (indicator != "tk")
						{
							tarDec1 = decimal.Parse(mementoTemp.Gia[1], new CultureInfo("vi-VN"));

							foreach (var matHang in danhSach)
							{
								mhTemp = matHang;
								foreach (var x in matHang.Gia)
								{
									valDec = decimal.Parse(x.Value, new CultureInfo("vi-VN"));
									del = "";

									if (indicator == "cx" && valDec != tarDec1) del = x.Key;
									else if (indicator == "truoc" && valDec > tarDec1) del = x.Key;
									else if (indicator == "sau" && valDec < tarDec1) del = x.Key;

									if (del != "")
									{
										mhTemp.NgaySX.Remove(del);
										mhTemp.HanDung.Remove(del);
										mhTemp.SoLuong.Remove(del);
										mhTemp.Gia.Remove(del);
									}
								}
								if (mhTemp.Gia.Count != 0) dsTemp.Add(mhTemp);
							}
						}
						else
						{
							tarDec1 = decimal.Parse(mementoTemp.Gia[2], new CultureInfo("vi-VN"));
							tarDec2 = decimal.Parse(mementoTemp.Gia[3], new CultureInfo("vi-VN"));

							foreach (var matHang in danhSach)
							{
								mhTemp = matHang;
								foreach (var x in matHang.Gia)
								{
									valDec = decimal.Parse(x.Value, new CultureInfo("vi-VN"));
									del = "";

									if (tarDec1 == tarDec2 && valDec != tarDec1) del = x.Key;
									else if (tarDec1 < tarDec2 && (valDec < tarDec1 || valDec > tarDec2)) del = x.Key;
									else if (tarDec1 > tarDec2 && (valDec > tarDec1 || valDec < tarDec2)) del = x.Key;

									if (del != "")
									{
										mhTemp.NgaySX.Remove(del);
										mhTemp.HanDung.Remove(del);
										mhTemp.SoLuong.Remove(del);
										mhTemp.Gia.Remove(del);
									}
								}
								if (mhTemp.Gia.Count != 0) dsTemp.Add(mhTemp);
							}
						}
						danhSach = dsTemp;
					}

					if (!danhSach.Any() || mementoTemp.IDHienTai == memento) break;
					else mementoTemp = caiDat.LichSuTim_SP[mementoTemp.IDSau];
				}

				return danhSach;
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				throw;
			}
		}
		public ServiceResult SapXepDanhSach(string tenNguoiDung, string? memento, int spMoiTrang, string[] sapXep_SP, Action<List<SanPham>, int> traVe)
		{
			try
			{
				List<SanPham> danhSach = DocDanhSach();
				if (danhSach.Any())
				{
					if (memento is not null) danhSach = Tim(danhSach, tenNguoiDung, memento);
					// danh sách
					danhSach = SanPham.SapXepHienThi(danhSach, sapXep_SP);
					// tổng trang
					spMoiTrang = (spMoiTrang <= 0) ? 7 : spMoiTrang;
					int tongTrang = (int)Math.Ceiling((decimal)danhSach.Count / spMoiTrang);
					tongTrang = (tongTrang == 0) ? 1 : tongTrang;

					traVe(danhSach, tongTrang);

				}
				else traVe(danhSach, 1);

				return new ServiceResult(true, "* Sắp xếp thành công!");
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult Them(SanPham sanPham)
		{
			try
			{
				List<SanPham> danhSach = DocDanhSach();
				if (danhSach.Any(c => c.Ma == sanPham.Ma))
					return new ServiceResult(false, "* Mã sản phẩm đã tồn tại.");

				_luuTruSanPham.ThemSanPham(sanPham);
				return new ServiceResult(true, "* Đã thêm sản phẩm thành công!");
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}

		}
		public ServiceResult Sua(string maCu, SanPham sanPham)
		{
			try
			{
				List<SanPham> danhSach = DocDanhSach();
				if (maCu != sanPham.Ma && danhSach.Any(c => c.Ma == sanPham.Ma))
					return new ServiceResult(false, "* Mã sản phẩm đã tồn tại.");

				_luuTruSanPham.SuaSanPham(maCu, sanPham);
				return new ServiceResult(true, "* Đã sửa sản phẩm thành công!");
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult Xoa(string ma)
		{
			try
			{
				_luuTruSanPham.XoaSanPham(ma);
				return new ServiceResult(true, "* Đã xóa sản phẩm thành công!");
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult KiemTraLichSu(string tenNguoiDung, string? memento)
		{
			try
			{
				if (memento == "0")
					return new ServiceResult(true, "* Memento có tồn tại!");
				else if (string.IsNullOrEmpty(memento) || memento == "curr" || !_luuTruNguoiDung.LayCaiDat(tenNguoiDung).LichSuTim_SP.ContainsKey(memento))
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