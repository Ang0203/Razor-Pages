using HDT_22810201_Entities;

namespace HDT_22810201_XuLyNghiepVu
{
	public interface IXuLyNguoiDung
	{
		List<NguoiDung> DocDanhSach();
		string[] LayThongTin(string tenNguoiDung);
		ServiceResult ThayDoiThongTin(string tenNguoiDung, string[] thayDoi);
		CaiDat LayCaiDat(string tenNguoiDung);
		ServiceResult ThayDoiCaiDat(string tenNguoiDung, dynamic thayDoi, string prop);
		ServiceResult KiemTraNguoiDung(KeyValuePair<string, string> nguoiDung);
	}
}
