using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class HD_ThemNhapXNModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;
		private readonly IXuLyHoaDon _xuLyHoaDon;
		private List<string> _dlMa;
		private List<string?[]> _dlVao;

		public List<SanPham> DanhSach { get; set; }
		public int TrangInt { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? XacNhan { get; init; }
		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty] public int TongTrang { get; set; }
		[BindProperty] public int SPMoiTrang { get; set; }
		[BindProperty] public string[] SapXep_SP { get; set; }
		public HD_ThemNhapXNModel(IXuLyNguoiDung xuLyNguoiDung, IXuLyHoaDon xuLyHoaDon)
		{
			_xuLyNguoiDung = xuLyNguoiDung;
			_xuLyHoaDon = xuLyHoaDon;
			_dlMa = new List<string>();
			_dlVao = new List<string?[]>();

			DanhSach = new List<SanPham>();
			TrangInt = 1;
			BaoLoi = "";

			TongTrang = 1;
			SPMoiTrang = 7;
			SapXep_SP = new string[8];
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				CaiDat caiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
				_dlMa = caiDat.ListTemp1_HD_ThemNhap;
				_dlVao = caiDat.ListTemp2_HD_ThemNhap;
				SPMoiTrang = caiDat.SPMoiTrang;
				SapXep_SP = caiDat.SapXep_SP;

				ServiceResult kq = _xuLyHoaDon.ThemNhapXN(SPMoiTrang, SapXep_SP, _dlMa, _dlVao, (x, y) => { DanhSach = x; TongTrang = y; });
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
				ServiceResult spmt = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, SPMoiTrang, "spmt");
				ServiceResult sxsp = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, SapXep_SP, "sapxep_sp");
				ServiceResult kq = new ServiceResult(true, "");
				if (XacNhan == "xacnhan")
				{
					CaiDat caiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
					_dlMa = caiDat.ListTemp1_HD_ThemNhap;
					_dlVao = caiDat.ListTemp2_HD_ThemNhap;
					kq = _xuLyHoaDon.ThemNhap(_dlMa, _dlVao);
				}
				if (spmt.IsSuccess && sxsp.IsSuccess && kq.IsSuccess)
				{
					if (XacNhan == "xacnhan")
					{
						_xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung,
							new KeyValuePair<List<string>, List<string[]>>(new List<string>(), new List<string[]>()), "lt_hd_tn");
						Response.Redirect("/hoa-don/danh-sach/?loai=nhap&trang=1");
					}
					else
					{
						TrangInt = (!string.IsNullOrEmpty(Trang) && Trang.All(char.IsDigit)) ? Convert.ToInt32(Trang) : 1;
						TrangInt = (TrangInt <= 0) ? 1 : (TrangInt > TongTrang) ? TongTrang : TrangInt;
						Response.Redirect("/hoa-don/them-nhapxn/?trang=" + TrangInt);
					}
				}
				else
				{
					BaoLoi = (!spmt.IsSuccess) ? spmt.Message :
						 (!sxsp.IsSuccess) ? sxsp.Message : kq.Message;
					OnGet();
				}
			}
		}
	}
}
