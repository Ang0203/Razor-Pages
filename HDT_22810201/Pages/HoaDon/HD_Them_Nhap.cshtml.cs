using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class HD_ThemNhapModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;
		private readonly IXuLyHoaDon _xuLyHoaDon;

		public List<SanPham> DanhSach { get; set; }
		public List<string> DLMa { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? XacNhan { get; init; }
		[BindProperty] public List<string?[]> DLVao { get; set; }
		[BindProperty] public string[] SapXep_HD_ThemNhap { get; set; }
		public HD_ThemNhapModel(IXuLyNguoiDung xuLyNguoiDung, IXuLyHoaDon xuLyHoaDon)
		{
			_xuLyNguoiDung = xuLyNguoiDung;
			_xuLyHoaDon = xuLyHoaDon;

			DanhSach = new List<SanPham>();
			DLMa = new List<string>();
			BaoLoi = "";

			DLVao = new List<string?[]>();
			SapXep_HD_ThemNhap = new string[4];
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				CaiDat caiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
				SapXep_HD_ThemNhap = caiDat.SapXep_HD_ThemNhap;

				ServiceResult kq = _xuLyHoaDon.ThemNhapDS(SapXep_HD_ThemNhap, (x, y) => { DanhSach = x; DLMa = y; });
				if (!kq.IsSuccess) BaoLoi = kq.Message;
				else if (XacNhan == "0")
				{
					foreach (var x in DanhSach) DLVao.Add(new string?[4] { null, null, null, null });
					_xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, new KeyValuePair<List<string>, List<string?[]>>(DLMa, DLVao), "lt_hd_tn");
				}
				else DLVao = caiDat.ListTemp2_HD_ThemNhap;
			}
		}
		public void OnPost()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				ServiceResult sxhdtn = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, SapXep_HD_ThemNhap, "sapxep_hd_tn");
				ServiceResult kq = _xuLyHoaDon.ThemNhapDS(SapXep_HD_ThemNhap, (x, y) => { DanhSach = x; DLMa = y; });
				ServiceResult lthdtn = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, new KeyValuePair<List<string>, List<string?[]>>(DLMa, DLVao), "lt_hd_tn");
				if (sxhdtn.IsSuccess && kq.IsSuccess && lthdtn.IsSuccess)
				{
					if (XacNhan == "xacnhan") Response.Redirect("/hoa-don/them-nhapxn/?trang=1");
					DLVao = _xuLyNguoiDung.LayCaiDat(tenNguoiDung).ListTemp2_HD_ThemNhap;
				}
				else
				{
					BaoLoi = (!sxhdtn.IsSuccess) ? sxhdtn.Message :
						(!kq.IsSuccess) ? kq.Message : lthdtn.Message;
					OnGet();
				}
			}
		}
	}
}
