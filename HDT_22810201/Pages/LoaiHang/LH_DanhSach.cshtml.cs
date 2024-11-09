using HDT_22810201_Entities;
using HDT_22810201_XuLyNghiepVu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class LH_DanhSachModel : PageModel
	{
		private readonly IXuLyNguoiDung _xuLyNguoiDung;
		private readonly IXuLyLoaiHang _xuLyLoaiHang;

		public List<HDT_22810201_Entities.LoaiHang> DanhSach { get; set; }
		public int TrangInt { get; set; }
		public string BaoLoi { get; set; }

		[BindProperty(SupportsGet = true)] public string? Trang { get; init; }
		[BindProperty] public string? Tim { get; set; }
		[BindProperty] public int TongTrang { get; set; }
		[BindProperty] public int LHMoiTrang { get; set; }
		[BindProperty] public string[] SapXep_LH { get; set; }
		public LH_DanhSachModel(IXuLyNguoiDung xuLyNguoiDung, IXuLyLoaiHang xuLyLoaiHang)
		{
			_xuLyNguoiDung = xuLyNguoiDung;
			_xuLyLoaiHang = xuLyLoaiHang;

			DanhSach = new List<HDT_22810201_Entities.LoaiHang>();
			TrangInt = 1;
			BaoLoi = "";

			TongTrang = 1;
			LHMoiTrang = 7;
			SapXep_LH = new string[8];
		}

		public void OnGet()
		{
			string? tenNguoiDung = HttpContext.Session.GetString("username");
			if (string.IsNullOrEmpty(tenNguoiDung)) Response.Redirect("/tai-khoan/dang-nhap");
			else
			{
				CaiDat caiDat = _xuLyNguoiDung.LayCaiDat(tenNguoiDung);
				LHMoiTrang = caiDat.LHMoiTrang;
				SapXep_LH = caiDat.SapXep_LH;
				Tim = caiDat.LichSuTim_LH;

				ServiceResult kq = _xuLyLoaiHang.SapXepDanhSach(Tim, LHMoiTrang, SapXep_LH, (x, y) => { DanhSach = x; TongTrang = y; });
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
				ServiceResult lhmt = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, LHMoiTrang, "lhmt");
				ServiceResult sxlh = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, SapXep_LH, "sapxep_lh");
				ServiceResult lstlh = _xuLyNguoiDung.ThayDoiCaiDat(tenNguoiDung, Tim!, "lst_lh");
				if (lhmt.IsSuccess && sxlh.IsSuccess && lstlh.IsSuccess)
				{
					TrangInt = (!string.IsNullOrEmpty(Trang) && Trang.All(char.IsDigit)) ? Convert.ToInt32(Trang) : 1;
					TrangInt = (TrangInt <= 0) ? 1 : (TrangInt > TongTrang) ? TongTrang : TrangInt;
					Response.Redirect("/loai-hang/danh-sach/?trang=" + TrangInt);
				}
				else
				{
					BaoLoi = (!lhmt.IsSuccess) ? lhmt.Message :
						(!sxlh.IsSuccess) ? sxlh.Message : lstlh.Message;
					OnGet();
				}
			}
		}
	}
}