using HDT_22810201_Entities;

namespace HDT_22810201_XuLyLuuTru
{
	public class LuuTruThongKe<T> : LuuTruCoBan<T>, ILuuTruThongKe<T>
		where T : SanPham
	{
		public LuuTruThongKe() { }

		public void SanPhamHetHan(List<T> danhSach)
		{
			// tạo giá trị
			DateTime dt = DateTime.UtcNow.AddHours(7);
			string dtString = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() +
							  dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

			// lưu kết quả
			LuuKetQua(danhSach, dtString);
		}
	}
}