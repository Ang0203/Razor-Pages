using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class MH_TimModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;
		private readonly IXuLySanPham _xuLySanPham;

		public string LichSu { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? Memento { get; set; }
		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty] public Memento_SP Memento_SP { get; set; }
		public MH_TimModel(IXuLyNguoiDung xuLyNguoiDung, IXuLySanPham xuLySanPham)
		{
			_xuLyNguoiDung = xuLyNguoiDung;
			_xuLySanPham = xuLySanPham;

			LichSu = "";
			BaoLoi = "";

			Memento_SP = new Memento_SP();
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				ServiceResult menull = _xuLySanPham.KiemTraLichSu(tenNguoiDung, Memento);
				if (menull.IsSuccess)
				{
					CaiDat caiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
					if (caiDat.LichSuTim_SP.Any()) LichSu = caiDat.LichSuTim_SP["0"].IDHienTai;
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
				ServiceResult kq = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, new KeyValuePair<string, Memento_SP>(Memento!, Memento_SP), "lst_sp");
				if (kq.IsSuccess) Response.Redirect("/mat-hang/tim-danh-sach/?memento=" + Memento_SP.IDHienTai + "&trang=1");
				else
				{
					BaoLoi = kq.Message;
					OnGet();
				}
			}
		}
	}
}
