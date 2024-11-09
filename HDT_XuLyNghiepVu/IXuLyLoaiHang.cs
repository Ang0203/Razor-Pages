using HDT_22810201_Entities;

namespace HDT_22810201_XuLyNghiepVu
{
	public interface IXuLyLoaiHang
	{
		List<SanPham> DocDanhSach();
		LoaiHang LayLoaiHang(string loai);
		ServiceResult SapXepDanhSach(string? tim, int lhMoiTrang, string[] sapXep_LH, Action<List<LoaiHang>, int> traVe);
		ServiceResult Sua(string loaiCu, string loaiMoi);
		ServiceResult Xoa(string loai);
	}
}