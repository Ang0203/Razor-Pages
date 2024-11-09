using System.Globalization;
using HDT_22810201_Entities;
using HDT_22810201_XuLyLuuTru;

namespace HDT_22810201_XuLyNghiepVu
{
	public class XuLyLoaiHang : IXuLyLoaiHang
	{
		private readonly ILuuTruSanPham<SanPham> _luuTruSanPham;
		public XuLyLoaiHang(ILuuTruSanPham<SanPham> luuTruSanPham)
		{
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
		public LoaiHang LayLoaiHang(string loai)
		{
			try
			{
				LoaiHang loaiHang = new LoaiHang();
				List<SanPham> dsTemp = DocDanhSach().Where(c => c.Loai == loai).ToList();
				loaiHang.Loai = loai;
				loaiHang.TongSP = dsTemp.Count.ToString();

				decimal giaSum = 0;
				foreach (var x in dsTemp)
				{
					foreach (var y in x.SoLuong)
					{
						giaSum += int.Parse(y.Value) * decimal.Parse(x.Gia[y.Key], new CultureInfo("vi-VN"));
					}
				}
				loaiHang.TongTien = giaSum.ToString("N0");

				return loaiHang;
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				throw;
			}
		}
		public ServiceResult SapXepDanhSach(string? tim, int lhMoiTrang, string[] sapXep_LH, Action<List<LoaiHang>, int> traVe)
		{
			try
			{
				List<LoaiHang> danhSach = new List<LoaiHang>();
				List<string> loaiTemp = DocDanhSach().Select(c => c.Loai).Distinct().ToList();
				if (!string.IsNullOrEmpty(tim)) loaiTemp.RemoveAll(c => !c.Contains(tim));

				if (loaiTemp.Any())
				{
					// danh sách
					foreach (var x in loaiTemp)
					{
						LoaiHang loaiHang = LayLoaiHang(x);
						danhSach.Add(loaiHang);
					}
					danhSach.Sort(LoaiHang.SoSanh(sapXep_LH));
					// tổng trang
					lhMoiTrang = (lhMoiTrang <= 0) ? 7 : lhMoiTrang;
					int tongTrang = (int)Math.Ceiling((decimal)danhSach.Count / lhMoiTrang);
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
		public ServiceResult Sua(string loaiCu, string loaiMoi)
		{
			try
			{
				List<SanPham> danhSach = DocDanhSach();
				if (loaiCu != loaiMoi && danhSach.Any(c => c.Loai == loaiMoi))
					return new ServiceResult(false, "* Mã loại hàng đã tồn tại.");

				_luuTruSanPham.SuaLoaiHang(loaiCu, loaiMoi);
				return new ServiceResult(true, "* Đã sửa loại hàng thành công!");
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
		public ServiceResult Xoa(string loai)
		{
			try
			{
				_luuTruSanPham.XoaLoaiHang(loai);
				return new ServiceResult(true, "* Đã xóa loại hàng thành công!");
			}
			catch (Exception ex)
			{
				_luuTruSanPham.LuuLoi(ex);
				return new ServiceResult(false, "* Lỗi: " + ex.Message);
			}
		}
	}
}