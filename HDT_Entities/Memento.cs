namespace HDT_22810201_Entities
{
	public abstract class Memento
	{
		public string IDTruoc { get; set; }
		public string IDHienTai { get; init; }
		public string IDSau { get; set; }
		public Memento()
		{
			IDTruoc = "0";
			IDHienTai = Guid.NewGuid().ToString();
			IDSau = "curr";
		}
	}
	public class Memento_SP : Memento
	{
		public string Ma { get; set; }
		public string Ten { get; set; }
		public string Loai { get; set; }
		public string CongTy { get; set; }
		public string[] NgaySX { get; set; }
		public string[] HanDung { get; set; }
		public string[] SoLuong { get; set; }
		public string[] Gia { get; set; }
		public Memento_SP()
		{
			Ma = "";
			Ten = "";
			Loai = "";
			CongTy = "";
			NgaySX = new string[4];
			HanDung = new string[4];
			SoLuong = new string[4];
			Gia = new string[4];
		}
	}
	public class Memento_HD : Memento
	{
		public string Loai { get; set; }
		public string[] Ngay { get; set; }
		public string[] TongSanPham { get; set; }
		public string[] TongTien { get; set; }
		public Memento_HD()
		{
			Loai = "";
			Ngay = new string[4];
			TongSanPham = new string[4];
			TongTien = new string[4];
		}
	}
}