using HDT_22810201_Entities;

namespace HDT_22810201_XuLyNghiepVu
{
	public interface IXuLySanPham
	{
		List<SanPham> DocDanhSach();
		List<SanPham> Tim(List<SanPham> danhSach, string tenNguoiDung, string memento);
		ServiceResult SapXepDanhSach(string tenNguoiDung, string? memento, int spMoiTrang, string[] sapXep_SP, Action<List<SanPham>, int> traVe);
		ServiceResult Them(SanPham sanPham);
		ServiceResult Sua(string maCu, SanPham sanPham);
		ServiceResult Xoa(string sanPham);
		ServiceResult KiemTraLichSu(string tenNguoiDung, string? memento);
	}
}
