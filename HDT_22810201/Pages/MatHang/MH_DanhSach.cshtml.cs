using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class MH_DanhSachModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;
		private readonly IXuLySanPham _xuLySanPham;

		public List<SanPham> DanhSach { get; set; }
		public int TrangInt { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty] public int TongTrang { get; set; }
		[BindProperty] public int SPMoiTrang { get; set; }
		[BindProperty] public string[] SapXep_SP { get; set; }
		public MH_DanhSachModel(IXuLyNguoiDung xuLyNguoiDung, IXuLySanPham xuLySanPham)
		{
			_xuLyNguoiDung = xuLyNguoiDung;
			_xuLySanPham = xuLySanPham;

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
				SPMoiTrang = caiDat.SPMoiTrang;
				SapXep_SP = caiDat.SapXep_SP;

				ServiceResult kq = _xuLySanPham.SapXepDanhSach(tenNguoiDung, null, SPMoiTrang, SapXep_SP, (x, y) => { DanhSach = x; TongTrang = y; });
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
				if (spmt.IsSuccess && sxsp.IsSuccess)
				{
					TrangInt = (!string.IsNullOrEmpty(Trang) && Trang.All(char.IsDigit)) ? Convert.ToInt32(Trang) : 1;
					TrangInt = (TrangInt <= 0) ? 1 : (TrangInt > TongTrang) ? TongTrang : TrangInt;
					Response.Redirect("/mat-hang/danh-sach/?trang=" + TrangInt);
				}
				else
				{
					BaoLoi = (!spmt.IsSuccess) ? spmt.Message : sxsp.Message;
					OnGet();
				}
			}
		}
	}
}
