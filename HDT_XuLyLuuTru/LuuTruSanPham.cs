using HDT_22810201_Entities;

namespace HDT_22810201_XuLyLuuTru
{
	public class LuuTruSanPham<T> : LuuTruCoBan<T>, ILuuTruSanPham<T>
		where T : SanPham
	{
		public LuuTruSanPham() { }

		public void ThemSanPham(T sanPham)
		{
			List<T> danhSach = DocDanhSach();
			danhSach.Add(sanPham);
			LuuDanhSach(danhSach);
		}
		public void SuaSanPham(string maCu, T sanPham)
		{
			List<T> danhSach = DocDanhSach();
			T spTemp = danhSach.Find(c => c.Ma == maCu)!;
			danhSach.Remove(spTemp);

			spTemp.Ma = sanPham.Ma;
			spTemp.Ten = sanPham.Ten;
			spTemp.Loai = sanPham.Loai;
			spTemp.CongTy = sanPham.CongTy;

			danhSach.Add(spTemp);
			LuuDanhSach(danhSach);
		}
		public void XoaSanPham(string ma)
		{
			List<T> danhSach = DocDanhSach();
			danhSach.Remove(danhSach.Find(c => c.Ma == ma)!);
			LuuDanhSach(danhSach);
		}
		public void SuaLoaiHang(string loaiCu, string loaiMoi)
		{
			List<T> danhSach = DocDanhSach();
			foreach (var x in danhSach)
				if (x.Loai == loaiCu) x.Loai = loaiMoi;
			LuuDanhSach(danhSach);
		}
		public void XoaLoaiHang(string loai)
		{
			List<T> danhSach = DocDanhSach();
			danhSach.RemoveAll(c => c.Loai == loai);
			LuuDanhSach(danhSach);
		}
	}
}