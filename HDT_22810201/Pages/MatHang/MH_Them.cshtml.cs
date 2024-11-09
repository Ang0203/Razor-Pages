using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class MH_ThemModel : PageModel
	{
		private readonly IXuLySanPham _xuLySanPham;
		private List<SanPham> _danhSach;

		public List<string> LoaiTemp { get; set; }
		public List<string> CTyTemp { get; set; }
		public string[] BaoLoi { get; set; }

		[BindProperty] public SanPham SanPham { get; set; }
		public MH_ThemModel(IXuLySanPham xuLySanPham)
		{
			_xuLySanPham = xuLySanPham;
			_danhSach = new List<SanPham>();

			LoaiTemp = new List<string>();
			CTyTemp = new List<string>();
			BaoLoi = new string[2] { "", "" };

			SanPham = new SanPham();
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
			}
		}
		public void OnPost()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				ServiceResult kq = _xuLySanPham.Them(SanPham);
				if (kq.IsSuccess)
				{
					SanPham = new SanPham();
					BaoLoi[0] = kq.Message;
					BaoLoi[1] = "";
				}
				else
				{
					BaoLoi[0] = "";
					BaoLoi[1] = kq.Message;
				}
				OnGet();
			}
		}
	}
}
