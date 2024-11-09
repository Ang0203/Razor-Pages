using HDT_22810201_Entities;
using HDT_22810201_XuLyLuuTru;

namespace HDT_22810201_XuLyNghiepVu
{
	public class XuLyNguoiDung : IXuLyNguoiDung
	{
		private readonly ILuuTruNguoiDung<NguoiDung> _luuTruNguoiDung;
		public XuLyNguoiDung(ILuuTruNguoiDung<NguoiDung> luuTruNguoiDung)
		{
			_luuTruNguoiDung = luuTruNguoiDung;
		}

		public List<NguoiDung> DocDanhSach()
		{
			try
			{
				return _luuTruNguoiDung.DocDanhSach();
			}
			catch (Exception ex)
			{
				_luuTruNguoiDung.LuuLoi(ex);
				throw;
			}
		}
		public string[] LayThongTin(string tenNguoiDung)
		{
			try
			{
				return _luuTruNguoiDung.LayThongTin(tenNguoiDung);
			}
			catch (Exception ex)
			{
				_luuTruNguoiDung.LuuLoi(ex);
				throw;
			}
		}
		public ServiceResult ThayDoiThongTin(string tenNguoiDung, string[] thayDoi)
		{
			try
			{
				List<NguoiDung> danhSach = DocDanhSach();
				NguoiDung nguoiDung = danhSach.Find(c => c.TenNguoiDung == tenNguoiDung)!;

				nguoiDung.HoTen = thayDoi[0];
				nguoiDung.Email = thayDoi[1];
				nguoiDung.SDT = thayDoi[2];

				_luuTruNguoiDung.ThayDoiThongTin(tenNguoiDung, nguoiDung);

				return new ServiceResult(true, "* Thay đổi thông tin thành công!");
			}
			catch (Exception ex)
			{
				_luuTruNguoiDung.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public CaiDat LayCaiDat(string tenNguoiDung)
		{
			try
			{
				return _luuTruNguoiDung.LayCaiDat(tenNguoiDung);
			}
			catch (Exception ex)
			{
				_luuTruNguoiDung.LuuLoi(ex);
				throw;
			}
		}
		public ServiceResult ThayDoiCaiDat(string tenNguoiDung, dynamic thayDoi, string prop)
		{
			try
			{
				List<NguoiDung> danhSach = DocDanhSach();
				CaiDat caiDat = danhSach.Find(c => c.TenNguoiDung == tenNguoiDung)!.CaiDat;
				switch (prop.ToLower())
				{
					case "spmt":
						thayDoi = (thayDoi <= 0) ? 7 : thayDoi;
						caiDat.SPMoiTrang = thayDoi;
						break;
					case "lhmt":
						thayDoi = (thayDoi <= 0) ? 7 : thayDoi;
						caiDat.LHMoiTrang = thayDoi;
						break;
					case "hdmt":
						thayDoi = (thayDoi <= 0) ? 7 : thayDoi;
						caiDat.HDMoiTrang = thayDoi;
						break;
					case "sapxep_sp":
						caiDat.SapXep_SP = thayDoi;
						break;
					case "sapxep_lh":
						caiDat.SapXep_LH = thayDoi;
						break;
					case "sapxep_hd":
						caiDat.SapXep_HD = thayDoi;
						break;
					case "sapxep_hd_tn":
						caiDat.SapXep_HD_ThemNhap = thayDoi;
						break;
					case "sapxep_hd_tb":
						caiDat.SapXep_HD_ThemBan = thayDoi;
						break;
					case "lt_hd_tn":
						List<string> temp1 = thayDoi.Key;
						List<string?[]> temp2 = thayDoi.Value;
						List<string> tempCD1 = caiDat.ListTemp1_HD_ThemNhap;

						if (!temp1.SequenceEqual(tempCD1))
						{
							List<string?[]> tempNew = new List<string?[]>();
							foreach (var x in temp1)
							{
								int index = tempCD1.IndexOf(x);
								if (index > -1) tempNew.Add(temp2[index]);
							}

							caiDat.ListTemp1_HD_ThemNhap = temp1;
							temp2 = tempNew;
						}
						caiDat.ListTemp2_HD_ThemNhap = temp2;
						break;
					case "lt_hd_tb":
						List<string> temp3 = thayDoi.Key;
						List<string?> temp4 = thayDoi.Value;
						List<string> tempCD3 = caiDat.ListTemp1_HD_ThemBan;

						if (!temp3.SequenceEqual(tempCD3))
						{
							List<string?> tempNew = new List<string?>();
							foreach (var x in temp3)
							{
								int index = tempCD3.IndexOf(x);
								if (index > -1)
								{
									if (temp4[index] is null) tempNew.Add("");
									else tempNew.Add(temp4[index]);
								}
							}

							caiDat.ListTemp1_HD_ThemBan = temp3;
							temp4 = tempNew;
						}
						caiDat.ListTemp2_HD_ThemBan = temp4;
						break;
					case "lst_sp":
						string memento = thayDoi.Key;
						Memento_SP memento_sp = thayDoi.Value;

						if (memento == "0") caiDat.DatLaiLST_SP(memento_sp);
						else
						{
							// trùng với gần nhất
							bool testTemp = false;
							if (caiDat.LichSuTim_SP[memento].Ma != memento_sp.Ma) testTemp = true;
							else if (caiDat.LichSuTim_SP[memento].Ten != memento_sp.Ten) testTemp = true;
							else if (caiDat.LichSuTim_SP[memento].Loai != memento_sp.Loai) testTemp = true;
							else if (caiDat.LichSuTim_SP[memento].CongTy != memento_sp.CongTy) testTemp = true;
							else if (!caiDat.LichSuTim_SP[memento].NgaySX.SequenceEqual(memento_sp.NgaySX)) testTemp = true;
							else if (!caiDat.LichSuTim_SP[memento].HanDung.SequenceEqual(memento_sp.HanDung)) testTemp = true;
							else if (!caiDat.LichSuTim_SP[memento].SoLuong.SequenceEqual(memento_sp.SoLuong)) testTemp = true;
							else if (!caiDat.LichSuTim_SP[memento].Gia.SequenceEqual(memento_sp.Gia)) testTemp = true;
							if (!testTemp)
							{
								memento_sp.IDTruoc = caiDat.LichSuTim_SP[memento].IDTruoc;
								memento_sp.IDSau = caiDat.LichSuTim_SP[memento].IDSau;
								caiDat.LichSuTim_SP.Add(memento_sp.IDHienTai, memento_sp);
								caiDat.LichSuTim_SP.Remove(memento);
								if (memento_sp.IDTruoc == "0") caiDat.LichSuTim_SP["0"] = memento_sp;
								else caiDat.LichSuTim_SP[memento_sp.IDTruoc].IDSau = memento_sp.IDHienTai;
								if (memento_sp.IDSau == "curr") caiDat.LichSuTim_SP["curr"] = memento_sp;
								else caiDat.LichSuTim_SP[memento_sp.IDSau].IDTruoc = memento_sp.IDHienTai;
								break;
							}

							// tìm từ giữa
							string memTemp = caiDat.LichSuTim_SP[memento].IDSau;
							bool cutHistory = false;
							while (memTemp != "curr")
							{
								string memDel = memTemp;
								memTemp = caiDat.LichSuTim_SP[memTemp].IDSau;
								caiDat.LichSuTim_SP.Remove(memDel);
								cutHistory = true;
							}
							if (cutHistory)
							{
								caiDat.LichSuTim_SP[memento].IDSau = "curr";
								caiDat.LichSuTim_SP["curr"] = caiDat.LichSuTim_SP[memento];
							}

							// tìm tiếp
							Memento_SP mementoTaiCurr = caiDat.LichSuTim_SP["curr"];
							mementoTaiCurr.IDSau = memento_sp.IDHienTai;
							memento_sp.IDTruoc = mementoTaiCurr.IDHienTai;
							caiDat.LichSuTim_SP["curr"] = memento_sp;
							caiDat.LichSuTim_SP.Add(memento_sp.IDHienTai, memento_sp);
							if (caiDat.LichSuTim_SP.ContainsKey(mementoTaiCurr.IDHienTai)) caiDat.LichSuTim_SP[mementoTaiCurr.IDHienTai] = mementoTaiCurr;
							else caiDat.LichSuTim_SP.Add(mementoTaiCurr.IDHienTai, mementoTaiCurr);
						}
						break;
					case "lst_lh":
						caiDat.LichSuTim_LH = thayDoi;
						break;
					case "lst_hd":
						string memento2 = thayDoi.Key;
						Memento_HD memento_hd = thayDoi.Value;

						if (memento2 == "0") caiDat.DatLaiLST_HD(memento_hd);
						else
						{
							// trùng với gần nhất
							bool testTemp = false;
							if (caiDat.LichSuTim_HD[memento2].Loai != memento_hd.Loai) testTemp = true;
							else if (!caiDat.LichSuTim_HD[memento2].Ngay.SequenceEqual(memento_hd.Ngay)) testTemp = true;
							else if (!caiDat.LichSuTim_HD[memento2].TongSanPham.SequenceEqual(memento_hd.TongSanPham)) testTemp = true;
							else if (!caiDat.LichSuTim_HD[memento2].TongTien.SequenceEqual(memento_hd.TongTien)) testTemp = true;
							if (!testTemp)
							{
								memento_hd.IDTruoc = caiDat.LichSuTim_HD[memento2].IDTruoc;
								memento_hd.IDSau = caiDat.LichSuTim_HD[memento2].IDSau;
								caiDat.LichSuTim_HD.Add(memento_hd.IDHienTai, memento_hd);
								caiDat.LichSuTim_HD.Remove(memento2);
								if (memento_hd.IDTruoc == "0") caiDat.LichSuTim_HD["0"] = memento_hd;
								else caiDat.LichSuTim_HD[memento_hd.IDTruoc].IDSau = memento_hd.IDHienTai;
								if (memento_hd.IDSau == "curr") caiDat.LichSuTim_HD["curr"] = memento_hd;
								else caiDat.LichSuTim_HD[memento_hd.IDSau].IDTruoc = memento_hd.IDHienTai;
								break;
							}

							// tìm từ giữa
							string memTemp = caiDat.LichSuTim_HD[memento2].IDSau;
							bool cutHistory = false;
							while (memTemp != "curr")
							{
								string memDel = memTemp;
								memTemp = caiDat.LichSuTim_HD[memTemp].IDSau;
								caiDat.LichSuTim_HD.Remove(memDel);
								cutHistory = true;
							}
							if (cutHistory)
							{
								caiDat.LichSuTim_HD[memento2].IDSau = "curr";
								caiDat.LichSuTim_HD["curr"] = caiDat.LichSuTim_HD[memento2];
							}

							// tìm tiếp
							Memento_HD mementoTaiCurr = caiDat.LichSuTim_HD["curr"];
							mementoTaiCurr.IDSau = memento_hd.IDHienTai;
							memento_hd.IDTruoc = mementoTaiCurr.IDHienTai;
							caiDat.LichSuTim_HD["curr"] = memento_hd;
							caiDat.LichSuTim_HD.Add(memento_hd.IDHienTai, memento_hd);
							if (caiDat.LichSuTim_HD.ContainsKey(mementoTaiCurr.IDHienTai)) caiDat.LichSuTim_HD[mementoTaiCurr.IDHienTai] = mementoTaiCurr;
							else caiDat.LichSuTim_HD.Add(mementoTaiCurr.IDHienTai, mementoTaiCurr);
						}
						break;
				}

				_luuTruNguoiDung.ThayDoiCaiDat(tenNguoiDung, caiDat);

				return new ServiceResult(true, "* Thay đổi cài đặt thành công!");
			}
			catch (Exception ex)
			{
				_luuTruNguoiDung.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult KiemTraNguoiDung(KeyValuePair<string, string> nguoiDung)
		{
			try
			{
				List<NguoiDung> danhSach = DocDanhSach();
				foreach (var x in danhSach)
				{
					if (x.TenNguoiDung == nguoiDung.Key && x.MatKhau == nguoiDung.Value)
						return new ServiceResult(true, "* Đăng nhập thành công!");
				}

				return new ServiceResult(false, "* Tên người dùng hoặc mật khẩu sai, hoặc tài khoản không tồn tại. Xin vui lòng kiểm tra lại.");
			}
			catch (Exception ex)
			{
				_luuTruNguoiDung.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
	}
}