using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class ThongTinModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;

		public string BaoLoi { get; set; }

		[BindProperty] public string[] ThayDoi { get; set; }
		public ThongTinModel(IXuLyNguoiDung xuLyNguoiDung)
		{
			_xuLyNguoiDung = xuLyNguoiDung;

			BaoLoi = "";

			ThayDoi = new string[3];
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				ThayDoi = _xuLyNguoiDung.LayThongTin(tenNguoiDung);
			}
		}
		public void OnPost()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				ServiceResult kq = _xuLyNguoiDung.ThayDoiThongTin(tenNguoiDung, ThayDoi);
				if (kq.IsSuccess) Response.Redirect("/trang-chu");
				else
				{
					BaoLoi = kq.Message;
					ThayDoi = _xuLyNguoiDung.LayThongTin(tenNguoiDung);
				}
			}
		}
	}
}