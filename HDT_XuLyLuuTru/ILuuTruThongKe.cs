using HDT_22810201_Entities;

namespace HDT_22810201_XuLyLuuTru
{
	public interface ILuuTruThongKe<T> : ILuuTruCoBan<T>
		where T : SanPham
	{
		void SanPhamHetHan(List<T> danhSach);
	}
}
