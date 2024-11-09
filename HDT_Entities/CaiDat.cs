namespace HDT_22810201_Entities
{
	public class CaiDat
	{
		public int SPMoiTrang { get; set; }
		public int LHMoiTrang { get; set; }
		public int HDMoiTrang { get; set; }
		public string[] SapXep_SP { get; set; }
		public string[] SapXep_LH { get; set; }
		public string[] SapXep_HD { get; set; }
		public string[] SapXep_HD_ThemNhap { get; set; }
		public string[] SapXep_HD_ThemBan { get; set; }
		public List<string> ListTemp1_HD_ThemNhap { get; set; }
		public List<string?[]> ListTemp2_HD_ThemNhap { get; set; }
		public List<string> ListTemp1_HD_ThemBan { get; set; }
		public List<string?> ListTemp2_HD_ThemBan { get; set; }
		public Dictionary<string, Memento_SP> LichSuTim_SP { get; set; }
		public string? LichSuTim_LH { get; set; }
		public Dictionary<string, Memento_HD> LichSuTim_HD { get; set; }
		public CaiDat()
		{
			SPMoiTrang = 7;
			LHMoiTrang = 7;
			HDMoiTrang = 7;
			SapXep_SP = new string[8] { "tang", "0", "0", "0", "0", "0", "0", "0" };
			SapXep_LH = new string[3] { "tang", "0", "0" };
			SapXep_HD = new string[4] { "giam", "0", "0", "0" };
			SapXep_HD_ThemNhap = new string[4] { "tang", "0", "0", "0" };
			SapXep_HD_ThemBan = new string[7] { "tang", "0", "0", "0", "0", "0", "0" };
			ListTemp1_HD_ThemNhap = new List<string>();
			ListTemp2_HD_ThemNhap = new List<string?[]>();
			ListTemp1_HD_ThemBan = new List<string>();
			ListTemp2_HD_ThemBan = new List<string?>();
			LichSuTim_SP = new Dictionary<string, Memento_SP>();
			LichSuTim_LH = "";
			LichSuTim_HD = new Dictionary<string, Memento_HD>();
		}

		public void DatLaiLST_SP(Memento_SP x)
		{
			LichSuTim_SP = new Dictionary<string, Memento_SP>()
			{
				{ "0", x },
				{ x.IDHienTai, x },
				{ "curr", x }
			};
		}
		public void DatLaiLST_HD(Memento_HD x)
		{
			LichSuTim_HD = new Dictionary<string, Memento_HD>(){
				{ "0", x },
				{ x.IDHienTai, x },
				{ "curr", x }
			};
		}
	}
}