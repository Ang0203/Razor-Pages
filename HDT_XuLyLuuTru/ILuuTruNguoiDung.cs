using HDT_22810201_Entities;

namespace HDT_22810201_XuLyLuuTru
{
	public interface ILuuTruNguoiDung<T> : ILuuTruCoBan<T>
		where T : NguoiDung
	{
		string[] LayThongTin(string tenNguoiDung);
		void ThayDoiThongTin(string tenNguoiDung, T nguoiDung);
		CaiDat LayCaiDat(string tenNguoiDung);
		void ThayDoiCaiDat(string tenNguoiDung, CaiDat caiDat);
	}
}
