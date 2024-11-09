using HDT_22810201_Entities;

namespace HDT_22810201_XuLyLuuTru
{
	public interface ILuuTruSanPham<T> : ILuuTruCoBan<T>
		where T : SanPham
	{
		void ThemSanPham(T sanPham);
		void SuaSanPham(string maCu, T sanPham);
		void XoaSanPham(string ma);
		void SuaLoaiHang(string loaiCu, string loaiMoi);
		void XoaLoaiHang(string loai);
	}
}
