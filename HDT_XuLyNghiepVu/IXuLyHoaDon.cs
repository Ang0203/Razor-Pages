using HDT_22810201_Entities;

namespace HDT_22810201_XuLyNghiepVu
{
	public interface IXuLyHoaDon
	{
		List<HoaDon> DocDanhSach();
		List<HoaDon> Tim(List<HoaDon> danhSach, string tenNguoiDung, string memento);
		ServiceResult SapXepDanhSach(string tenNguoiDung, string? memento, string? loai, int hdMoiTrang, string[] sapXep_HD, Action<List<HoaDon>, int> traVe);
		ServiceResult ChiTiet(string ma, int spMoiTrang, string[] sapXep_SP, Action<List<SanPham>, int> traVe);
		ServiceResult ThemNhapDS(string[] sapXep_HD_ThemNhap, Action<List<SanPham>, List<string>> traVe);
		ServiceResult ThemNhapXN(int spMoiTrang, string[] sapXep_SP, List<string> dlMa, List<string?[]> dlVao, Action<List<SanPham>, int> traVe);
		ServiceResult ThemNhap(List<string> dlMa, List<string?[]> dlVao);
		ServiceResult ThemBanDS(string[] sapXep_HD_ThemBan, Action<List<SanPham>, List<string>> traVe);
		ServiceResult ThemBanXN(int spMoiTrang, string[] sapXep_HD_ThemBan, List<string> dlMa, List<string?> dlVao, Action<List<SanPham>, int> traVe);
		ServiceResult ThemBan(List<string> dlMa, List<string?> dlVao);
		ServiceResult KiemTraHoaDon(string? ma);
		ServiceResult KiemTraLichSu(string tenNguoiDung, string? memento);
	}
}