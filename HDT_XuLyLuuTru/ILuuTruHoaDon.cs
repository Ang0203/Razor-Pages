using HDT_22810201_Entities;

namespace HDT_22810201_XuLyLuuTru
{
	public interface ILuuTruHoaDon<T> : ILuuTruCoBan<T>
		where T : HoaDon
	{
		void ThemHoaDon(T hoaDon);
		bool KiemTraHoaDon(string ma);
	}
}