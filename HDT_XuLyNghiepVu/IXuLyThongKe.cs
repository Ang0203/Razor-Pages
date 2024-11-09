using HDT_22810201_Entities;

namespace HDT_22810201_XuLyNghiepVu
{
	public interface IXuLyThongKe
	{
		List<SanPham> DocDanhSach();
		ServiceResult SanPhamHetHan(int spMoiTrang, string[] sapXep_SP, Action<List<SanPham>, int> traVe);
		ServiceResult SanPhamHetHanXN();
	}
}