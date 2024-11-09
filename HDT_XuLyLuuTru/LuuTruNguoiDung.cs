using HDT_22810201_Entities;

namespace HDT_22810201_XuLyLuuTru
{
	public class LuuTruNguoiDung<T> : LuuTruCoBan<T>, ILuuTruNguoiDung<T>
		where T : NguoiDung
	{
		public LuuTruNguoiDung() { }

		public string[] LayThongTin(string tenNguoiDung)
		{
			List<T> danhSach = DocDanhSach();
			string[] ketQua = new string[3];
			NguoiDung nguoiDung = danhSach.Find(c => c.TenNguoiDung == tenNguoiDung)!;

			ketQua[0] = nguoiDung.HoTen;
			ketQua[1] = nguoiDung.Email;
			ketQua[2] = nguoiDung.SDT;

			return ketQua;
		}
		public void ThayDoiThongTin(string tenNguoiDung, T nguoiDung)
		{
			List<T> danhSach = DocDanhSach();
			danhSach.RemoveAll(c => c.TenNguoiDung == tenNguoiDung);
			danhSach.Add(nguoiDung);
			LuuDanhSach(danhSach);
		}
		public CaiDat LayCaiDat(string tenNguoiDung)
		{
			List<T> danhSach = DocDanhSach();
			CaiDat ketQua = danhSach.Find(c => c.TenNguoiDung == tenNguoiDung)!.CaiDat;
			return ketQua;
		}
		public void ThayDoiCaiDat(string tenNguoiDung, CaiDat caiDat)
		{
			List<T> danhSach = DocDanhSach();
			danhSach.Find(c => c.TenNguoiDung == tenNguoiDung)!.CaiDat = caiDat;
			LuuDanhSach(danhSach);
		}
	}
}