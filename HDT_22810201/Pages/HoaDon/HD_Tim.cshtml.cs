using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class HD_TimModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;
		private readonly IXuLyHoaDon _xuLyHoaDon;

		public string LichSu { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? Memento { get; set; }
		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty] public Memento_HD Memento_HD { get; set; }
		public HD_TimModel(IXuLyNguoiDung xuLyNguoiDung, IXuLyHoaDon xuLyHoaDon)
		{
			_xuLyNguoiDung = xuLyNguoiDung;
			_xuLyHoaDon = xuLyHoaDon;

			LichSu = "";
			BaoLoi = "";

			Memento_HD = new Memento_HD();
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
					CaiDat caiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
					if (caiDat.LichSuTim_HD.Any()) LichSu = caiDat.LichSuTim_HD["0"].IDHienTai;
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
				ServiceResult kq = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, new KeyValuePair<string, Memento_HD>(Memento!, Memento_HD), "lst_hd");
				if (kq.IsSuccess) Response.Redirect("/hoa-don/tim-danh-sach/?memento=" + Memento_HD.IDHienTai + "&trang=1");
				else
				{
					BaoLoi = kq.Message;
					OnGet();
				}
			}
		}
	}
}
