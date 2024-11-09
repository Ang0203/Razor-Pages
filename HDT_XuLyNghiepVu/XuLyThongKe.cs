using System.Globalization;
using HDT_22810201_Entities;
using HDT_22810201_XuLyLuuTru;

namespace HDT_22810201_XuLyNghiepVu
{
	public class XuLyThongKe : IXuLyThongKe
	{
		private readonly ILuuTruSanPham<SanPham> _luuTruSanPham;
		private readonly ILuuTruThongKe<SanPham> _luuTruThongKe;
		public XuLyThongKe(ILuuTruSanPham<SanPham> luuTruSanPham, ILuuTruThongKe<SanPham> luuTruThongKe)
		{
			_luuTruSanPham = luuTruSanPham;
			_luuTruThongKe = luuTruThongKe;
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
		public List<SanPham> DanhSachHetHan(string loai)
		{
			try
			{
				List<SanPham> danhSach = DocDanhSach();
				if (danhSach.Any())
				{
					// tạo danh sách
					DateTime dt = DateTime.UtcNow.AddHours(7);
					List<SanPham> dsTemp = new List<SanPham>();
					foreach (var sanPham in danhSach)
					{
						SanPham spTemp = sanPham;
						foreach (var x in sanPham.HanDung)
						{
							bool doIt = false;
							if (loai == "ds-con" && dt >= DateTime.Parse(x.Value, new CultureInfo("vi-VN")))
								doIt = true;
							if (loai == "ds-het" && dt < DateTime.Parse(x.Value, new CultureInfo("vi-VN")))
								doIt = true;
							if (doIt)
							{
								spTemp.NgaySX.Remove(x.Key);
								spTemp.HanDung.Remove(x.Key);
								spTemp.SoLuong.Remove(x.Key);
								spTemp.Gia.Remove(x.Key);
							}
						}
						if (loai == "ds-con" || (loai == "ds-het" && spTemp.SoLuong.Any()))
							dsTemp.Add(spTemp);
					}
					danhSach = dsTemp;
				}
				return danhSach;
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				throw;
			}
		}
		public ServiceResult SanPhamHetHan(int spMoiTrang, string[] sapXep_SP, Action<List<SanPham>, int> traVe)
		{
			try
			{
				List<SanPham> danhSach = DanhSachHetHan("ds-het");
				if (danhSach.Any())
				{
					// danh sách
					danhSach = SanPham.SapXepHienThi(danhSach, sapXep_SP);
					// tổng trang
					spMoiTrang = (spMoiTrang <= 0) ? 7 : spMoiTrang;
					int tongTrang = (int)Math.Ceiling((decimal)danhSach.Count / spMoiTrang);
					tongTrang = (tongTrang == 0) ? 1 : tongTrang;

					traVe(danhSach, tongTrang);

				}
				else traVe(danhSach, 1);

				return new ServiceResult(true, "* Sắp xếp thành công danh sách sản phẩm hết hạn!");
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult SanPhamHetHanXN()
		{
			try
			{
				List<SanPham> danhSach_xoa = DanhSachHetHan("ds-het");
				List<SanPham> danhSach = DanhSachHetHan("ds-con");
				_luuTruSanPham.LuuDanhSach(danhSach);
				_luuTruThongKe.SanPhamHetHan(danhSach_xoa);

				return new ServiceResult(true, "* Xóa thành công các sản phẩm hết hạn!");
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
	}
}