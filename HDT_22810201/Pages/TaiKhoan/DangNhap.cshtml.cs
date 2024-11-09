using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class DangNhapModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;

		public string BaoLoi { get; set; }

		[BindProperty] public string TenNguoiDung { get; set; }
		[BindProperty] public string MatKhau { get; set; }
		public DangNhapModel(IXuLyNguoiDung xuLyNguoiDung)
		{
			_xuLyNguoiDung = xuLyNguoiDung;

			BaoLoi = "";

			TenNguoiDung = "";
			MatKhau = "";
		}

		public void OnGet()
		{
			if (!string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
				Response.Redirect("/trang-chu");
		}
		public void OnPost()
		{
			KeyValuePair<string, string> nd = new KeyValuePair<string, string>(TenNguoiDung, MatKhau);
			ServiceResult kq = _xuLyNguoiDung.KiemTraNguoiDung(nd);

			if (kq.IsSuccess)
			{
				HttpContext.Session.SetString("username", TenNguoiDung);
				Response.Redirect("/trang-chu");
			}
			else BaoLoi = kq.Message;
		}
	}
}