using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class HD_ThemBanModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;
		private readonly IXuLyHoaDon _xuLyHoaDon;

		public List<SanPham> DanhSach { get; set; }
		public List<string> DLMa { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? XacNhan { get; init; }
		[BindProperty] public List<string?> DLVao { get; set; }
		[BindProperty] public string[] SapXep_HD_ThemBan { get; set; }
		public HD_ThemBanModel(IXuLyNguoiDung xuLyNguoiDung, IXuLyHoaDon xuLyHoaDon)
		{
			_xuLyNguoiDung = xuLyNguoiDung;
			_xuLyHoaDon = xuLyHoaDon;

			DanhSach = new List<SanPham>();
			DLMa = new List<string>();
			BaoLoi = "";

			DLVao = new List<string?>();
			SapXep_HD_ThemBan = new string[7];
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				CaiDat caiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
				SapXep_HD_ThemBan = caiDat.SapXep_HD_ThemBan;

				ServiceResult kq = _xuLyHoaDon.ThemBanDS(SapXep_HD_ThemBan, (x, y) => { DanhSach = x; DLMa = y; });
				if (!kq.IsSuccess) BaoLoi = kq.Message;
				else if (XacNhan == "0")
				{
					foreach (var x in DanhSach) DLVao.Add(null);
					_xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, new KeyValuePair<List<string>, List<string?>>(DLMa, DLVao), "lt_hd_tb");
				}
				else DLVao = caiDat.ListTemp2_HD_ThemBan;
			}
		}
		public void OnPost()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				ServiceResult sxhdtb = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, SapXep_HD_ThemBan, "sapxep_hd_tb");
				ServiceResult kq = _xuLyHoaDon.ThemBanDS(SapXep_HD_ThemBan, (x, y) => { DanhSach = x; DLMa = y; });
				ServiceResult lthdtb = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, new KeyValuePair<List<string>, List<string?>>(DLMa, DLVao), "lt_hd_tb");
				if (sxhdtb.IsSuccess && kq.IsSuccess && lthdtb.IsSuccess)
				{
					if (XacNhan == "xacnhan") Response.Redirect("/hoa-don/them-banxn/?trang=1");
					DLVao = _xuLyNguoiDung.LayCaiDat(tenNguoiDung).ListTemp2_HD_ThemBan;
				}
				else
				{
					BaoLoi = (!sxhdtb.IsSuccess) ? sxhdtb.Message :
						(!kq.IsSuccess) ? kq.Message : lthdtb.Message;
					OnGet();
				}
			}
		}
	}
}
