namespace HDT_22810201_XuLyLuuTru
{
	public interface ILuuTruCoBan<T>
	{
		List<T> DocDanhSach();
		void LuuLoi(Exception ex);
		void LuuDanhSach(List<T> danhSach);
		void LuuKetQua(List<T> danhSach, string dt);
	}
}
