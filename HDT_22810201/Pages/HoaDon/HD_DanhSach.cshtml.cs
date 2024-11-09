using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class HD_DanhSachModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;
		private readonly IXuLyHoaDon _xuLyHoaDon;

		public List<HDT_22810201_Entities.HoaDon> DanhSach { get; set; }
		public int TrangInt { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? Loai { get; set; }
		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty] public int TongTrang { get; set; }
		[BindProperty] public int HDMoiTrang { get; set; }
		[BindProperty] public string[] SapXep_HD { get; set; }
		public HD_DanhSachModel(IXuLyNguoiDung xuLyNguoiDung, IXuLyHoaDon xuLyHoaDon)
		{
			_xuLyNguoiDung = xuLyNguoiDung;
			_xuLyHoaDon = xuLyHoaDon;

			DanhSach = new List<HDT_22810201_Entities.HoaDon>();
			TrangInt = 1;
			BaoLoi = "";

			TongTrang = 1;
			HDMoiTrang = 7;
			SapXep_HD = new string[4];
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				Loai = (!string.IsNullOrEmpty(Loai)) ? Loai.ToLower() : "nhap";
				Loai = (Loai == "nhap" || Loai == "ban") ? Loai : "nhap";
				CaiDat caiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
				HDMoiTrang = caiDat.HDMoiTrang;
				SapXep_HD = caiDat.SapXep_HD;

				ServiceResult kq = _xuLyHoaDon.SapXepDanhSach(tenNguoiDung, null, Loai, HDMoiTrang, SapXep_HD, (x, y) => { DanhSach = x; TongTrang = y; });
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
				ServiceResult hdmt = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, HDMoiTrang, "hdmt");
				ServiceResult sxhd = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, SapXep_HD, "sapxep_hd");
				if (hdmt.IsSuccess && sxhd.IsSuccess)
				{
					TrangInt = (!string.IsNullOrEmpty(Trang) && Trang.All(char.IsDigit)) ? Convert.ToInt32(Trang) : 1;
					TrangInt = (TrangInt <= 0) ? 1 : (TrangInt > TongTrang) ? TongTrang : TrangInt;
					Response.Redirect("/hoa-don/danh-sach/?loai=" + Loai + "&trang=" + TrangInt);
				}
				else
				{
					BaoLoi = (!hdmt.IsSuccess) ? hdmt.Message : sxhd.Message;
					OnGet();
				}
			}
		}
	}
}
