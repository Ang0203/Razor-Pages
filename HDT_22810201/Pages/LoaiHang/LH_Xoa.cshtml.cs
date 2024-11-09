using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class LH_XoaModel : PageModel
	{
		private readonly IXuLyLoaiHang _xuLyLoaiHang;

		public HDT_22810201_Entities.LoaiHang LoaiHang { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty(SupportsGet = true)] public string? Loai { get; set; }
		public LH_XoaModel(IXuLyLoaiHang xuLyLoaiHang)
		{
			_xuLyLoaiHang = xuLyLoaiHang;

			LoaiHang = new HDT_22810201_Entities.LoaiHang();
			BaoLoi = "";
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				if (string.IsNullOrEmpty(Loai) || _xuLyLoaiHang.DocDanhSach().Find(c => c.Loai == Loai) is null) Loai = null;
				else LoaiHang = _xuLyLoaiHang.LayLoaiHang(Loai);
			}
		}
		public void OnPost()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				ServiceResult kq = _xuLyLoaiHang.Xoa(Loai!);
				if (kq.IsSuccess) Response.Redirect("/loai-hang/danh-sach/?trang=" + Trang);
				else
				{
					BaoLoi = kq.Message;
					_xuLyLoaiHang.LayLoaiHang(Loai!);
				}
			}
		}
	}
}
