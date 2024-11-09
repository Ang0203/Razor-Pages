using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class HD_ThemBanXNModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;
		private readonly IXuLyHoaDon _xuLyHoaDon;
		private List<string> _dlMa;
		private List<string?> _dlVao;

		public List<SanPham> DanhSach { get; set; }
		public int TrangInt { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? XacNhan { get; init; }
		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty] public int TongTrang { get; set; }
		[BindProperty] public int SPMoiTrang { get; set; }
		[BindProperty] public string[] SapXep_HD_ThemBan { get; set; }
		public HD_ThemBanXNModel(IXuLyNguoiDung xuLyNguoiDung, IXuLyHoaDon xuLyHoaDon)
		{
			_xuLyNguoiDung = xuLyNguoiDung;
			_xuLyHoaDon = xuLyHoaDon;
			_dlMa = new List<string>();
			_dlVao = new List<string?>();

			DanhSach = new List<SanPham>();
			TrangInt = 1;
			BaoLoi = "";

			TongTrang = 1;
			SPMoiTrang = 7;
			SapXep_HD_ThemBan = new string[7];
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				CaiDat caiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
				_dlMa = caiDat.ListTemp1_HD_ThemBan;
				_dlVao = caiDat.ListTemp2_HD_ThemBan;
				SPMoiTrang = caiDat.SPMoiTrang;
				SapXep_HD_ThemBan = caiDat.SapXep_HD_ThemBan;

				ServiceResult kq = _xuLyHoaDon.ThemBanXN(SPMoiTrang, SapXep_HD_ThemBan, _dlMa, _dlVao, (x, y) => { DanhSach = x; TongTrang = y; });
				if (!kq.IsSuccess) BaoLoi = kq.Message;
				else
				{
					TrangInt = (!string.IsNullOrEmpty(Trang) && Trang.All(char.IsDigit)) ? Convert.ToInt32(Trang) : 1;
					TrangInt = (TrangInt <= 0) ? 1 : (TrangInt > TongTrang) ? TongTrang : TrangInt;
				}
			}
		}
		public void OnPost()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				ServiceResult spmt = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, SPMoiTrang, "spmt");
				ServiceResult sxsp = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, SapXep_HD_ThemBan, "sapxep_hd_tb");
				ServiceResult kq = new ServiceResult(true, "");
				if (XacNhan == "xacnhan")
				{
					CaiDat caiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
					_dlMa = caiDat.ListTemp1_HD_ThemBan;
					_dlVao = caiDat.ListTemp2_HD_ThemBan;
					kq = _xuLyHoaDon.ThemBan(_dlMa, _dlVao);
				}
				if (spmt.IsSuccess && sxsp.IsSuccess && kq.IsSuccess)
				{
					if (XacNhan == "xacnhan")
					{
						_xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung,
							new KeyValuePair<List<string>, List<string>>(new List<string>(), new List<string>()), "lt_hd_tb");
						Response.Redirect("/hoa-don/danh-sach/?loai=ban&trang=1");
					}
					else
					{
						TrangInt = (!string.IsNullOrEmpty(Trang) && Trang.All(char.IsDigit)) ? Convert.ToInt32(Trang) : 1;
						TrangInt = (TrangInt <= 0) ? 1 : (TrangInt > TongTrang) ? TongTrang : TrangInt;
						Response.Redirect("/hoa-don/them-banxn/?trang=" + TrangInt);
					}
				}
				else
				{
					BaoLoi = (!spmt.IsSuccess) ? spmt.Message :
						 (!sxsp.IsSuccess) ? sxsp.Message : kq.Message;
					OnGet();
				}
			}
		}
	}
}
