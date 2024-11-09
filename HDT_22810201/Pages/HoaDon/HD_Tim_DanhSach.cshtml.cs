using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class HD_TimDanhSachModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;
		private readonly IXuLyHoaDon _xuLyHoaDon;

		public List<HDT_22810201_Entities.HoaDon> DanhSach { get; set; }
		public CaiDat CaiDat { get; set; }
		public int TrangInt { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? Memento { get; set; }
		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty] public int TongTrang { get; set; }
		[BindProperty] public int HDMoiTrang { get; set; }
		[BindProperty] public string[] SapXep_HD { get; set; }
		public HD_TimDanhSachModel(IXuLyNguoiDung xuLyNguoiDung, IXuLyHoaDon xuLyHoaDon)
		{
			_xuLyNguoiDung = xuLyNguoiDung;
			_xuLyHoaDon = xuLyHoaDon;

			DanhSach = new List<HDT_22810201_Entities.HoaDon>();
			CaiDat = new CaiDat();
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
				ServiceResult menull = _xuLyHoaDon.KiemTraLichSu(tenNguoiDung, Memento);
				if (menull.IsSuccess)
				{
					CaiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
					HDMoiTrang = CaiDat.HDMoiTrang;
					SapXep_HD = CaiDat.SapXep_HD;

					ServiceResult kq = _xuLyHoaDon.SapXepDanhSach(tenNguoiDung, Memento, null, HDMoiTrang, SapXep_HD, (x, y) => { DanhSach = x; TongTrang = y; });
					if (!kq.IsSuccess) BaoLoi = kq.Message;
					else
					{
						TrangInt = (!string.IsNullOrEmpty(Trang) && Trang.All(char.IsDigit)) ? Convert.ToInt32(Trang) : 1;
						TrangInt = (TrangInt <= 0) ? 1 : (TrangInt > TongTrang) ? TongTrang : TrangInt;
					}
				}
				else
				{
					Memento = null;
					BaoLoi = menull.Message;
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
					CaiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
					TrangInt = (!string.IsNullOrEmpty(Trang) && Trang.All(char.IsDigit)) ? Convert.ToInt32(Trang) : 1;
					TrangInt = (TrangInt <= 0) ? 1 : (TrangInt > TongTrang) ? TongTrang : TrangInt;
					Response.Redirect("/hoa-don/tim-danh-sach/?memento=" + Memento + "&trang=" + TrangInt);
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
