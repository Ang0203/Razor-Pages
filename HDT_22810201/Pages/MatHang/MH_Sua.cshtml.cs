using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class MH_SuaModel : PageModel
	{
		private readonly IXuLySanPham _xuLySanPham;
		private List<SanPham> _danhSach;

		public List<string> LoaiTemp { get; set; }
		public List<string> CTyTemp { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? Memento { get; init; }
		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty(SupportsGet = true)] public string? Ma { get; init; }
		[BindProperty] public SanPham? SanPham { get; set; }
		public MH_SuaModel(IXuLySanPham xuLySanPham)
		{
			_xuLySanPham = xuLySanPham;
			_danhSach = new List<SanPham>();

			LoaiTemp = new List<string>();
			CTyTemp = new List<string>();
			BaoLoi = "";
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				_danhSach = _xuLySanPham.DocDanhSach();
				LoaiTemp = _danhSach.Select(c => c.Loai).Distinct().OrderBy(c => c).ToList();
				CTyTemp = _danhSach.Select(c => c.CongTy).Distinct().OrderBy(c => c).ToList();
				SanPham = _danhSach.Find(c => c.Ma == Ma);
			}
		}
		public void OnPost()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				ServiceResult kq = _xuLySanPham.Sua(Ma!, SanPham!);
				if (kq.IsSuccess)
				{
					if (Memento is null) Response.Redirect("/mat-hang/danh-sach/?trang=" + Trang);
					else Response.Redirect("/mat-hang/tim-danh-sach/?memento=" + Memento + "&trang=" + Trang);
				}
				else
				{
					BaoLoi = kq.Message;
					_danhSach = _xuLySanPham.DocDanhSach();
					LoaiTemp = _danhSach.Select(c => c.Loai).Distinct().OrderBy(c => c).ToList();
					CTyTemp = _danhSach.Select(c => c.CongTy).Distinct().OrderBy(c => c).ToList();
				}
			}
		}
	}
}
