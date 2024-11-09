namespace HDT_22810201_Entities
{
	public class NguoiDung
	{
		public string TenNguoiDung { get; init; }
		public string MatKhau { get; init; }
		public string HoTen { get; set; }
		public string Email { get; set; }
		public string SDT { get; set; }
		public CaiDat CaiDat { get; set; }
		public NguoiDung(string tenNguoiDung, string matKhau)
		{
			TenNguoiDung = tenNguoiDung;
			MatKhau = matKhau;
			HoTen = "";
			Email = "";
			SDT = "";
			CaiDat = new CaiDat();
		}

		public static List<NguoiDung> TaoThuNghiem()
		{
			List<NguoiDung> danhSach = new List<NguoiDung>
			{
				new NguoiDung("nv1", "123")
				{
					HoTen = "Y.S",
					Email = "#",
					SDT = "#",
				},
				new NguoiDung("nv2", "123")
				{
					HoTen = "Dương Trần Bình An",
					Email = "duongtranbinhan0203@gmail.com",
					SDT = "09xx.0xx.9xx",
				}
			};

			return danhSach;
		}
	}
}