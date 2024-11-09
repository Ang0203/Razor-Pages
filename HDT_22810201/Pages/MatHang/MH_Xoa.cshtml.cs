using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class MH_XoaModel : PageModel
	{
		private readonly IXuLySanPham _xuLySanPham;

		public SanPham? SanPham { get; set; }
		public KeyValuePair<string, string> TongLuongTien { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? Memento { get; init; }
		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty(SupportsGet = true)] public string? Ma { get; init; }
		public MH_XoaModel(IXuLySanPham xuLySanPham)
		{
			_xuLySanPham = xuLySanPham;

			BaoLoi = "";
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				SanPham = _xuLySanPham.DocDanhSach().Find(c => c.Ma == Ma);
				TongLuongTien = (SanPham is null) ?
					new KeyValuePair<string, string>("", "") :
					SanPham.TongLuongTien();
			}
		}
		public void OnPost()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				ServiceResult kq = _xuLySanPham.Xoa(Ma!);
				if (kq.IsSuccess)
				{
					if (Memento is null) Response.Redirect("/mat-hang/danh-sach/?trang=" + Trang);
					else Response.Redirect("/mat-hang/tim-danh-sach/?memento=" + Memento + "&trang=" + Trang);
				}
				else
				{
					BaoLoi = kq.Message;
					OnGet();
				}
			}
		}
	}
}
