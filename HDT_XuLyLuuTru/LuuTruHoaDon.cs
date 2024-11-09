using HDT_22810201_Entities;

namespace HDT_22810201_XuLyLuuTru
{
	public class LuuTruHoaDon<T> : LuuTruCoBan<T>, ILuuTruHoaDon<T>
		where T : HoaDon
	{
		public LuuTruHoaDon() { }

		public void ThemHoaDon(T hoaDon)
		{
			List<T> danhSach = DocDanhSach();
			danhSach.Add(hoaDon);
			LuuDanhSach(danhSach);
		}
		public bool KiemTraHoaDon(string ma)
		{
			List<T> danhSach = DocDanhSach();
			return danhSach.Find(c => c.Ma == ma) is not null;
		}
	}
}