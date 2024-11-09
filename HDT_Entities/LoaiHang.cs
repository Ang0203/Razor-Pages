using System.Globalization;
using System.Reflection;

namespace HDT_22810201_Entities
{
	public class LoaiHang
	{
		public string Loai { get; set; }
		public string TongSP { get; set; }
		public string TongTien { get; set; }
		public LoaiHang()
		{
			Loai = "";
			TongSP = "";
			TongTien = "";
		}

		private class SoSanhHelper : IComparer<LoaiHang>
		{
			private int _index;
			private readonly string _opt;
			public SoSanhHelper(int index, string opt)
			{
				_index = index;
				_opt = opt;
			}

			private int SoSanhAB(dynamic? a, dynamic? b)
			{
				if (a is null && b is null) return 0;
				else if (a is null) return -1;
				else if (b is null) return 1;

				// kiểm tra string
				if (a is string && _opt == "tang") return string.Compare(a, b);
				else if (a is string) return string.Compare(b, a);

				// kiểm tra loại khác
				if (_opt == "tang") return (a < b) ? -1 : (a > b) ? 1 : 0;
				else return (a > b) ? -1 : (a < b) ? 1 : 0;
			}
			public int Compare(LoaiHang? x, LoaiHang? y)
			{
				if (x is null && y is null) return 0;
				else if (x is null) return -1;
				else if (y is null) return 1;

				int ss = 0;
				PropertyInfo prop = typeof(LoaiHang).GetProperties().ElementAt(_index);
				switch (_index)
				{
					case 0:
						string a0 = (string)prop.GetValue(x)!;
						string b0 = (string)prop.GetValue(y)!;
						ss = SoSanhAB(a0, b0);
						break;
					case 1:
						ulong a1 = ulong.Parse((string)prop.GetValue(x)!);
						ulong b1 = ulong.Parse((string)prop.GetValue(y)!);
						ss = SoSanhAB(a1, b1);
						break;
					case 2:
						decimal a2 = decimal.Parse((string)prop.GetValue(x)!, new CultureInfo("vi-VN"));
						decimal b2 = decimal.Parse((string)prop.GetValue(y)!, new CultureInfo("vi-VN"));
						ss = SoSanhAB(a2, b2);
						break;
				}
				return ss;
			}
		}
		public static IComparer<LoaiHang> SoSanh(string[] sapXep_LH)
		{
			int index = 0;
			foreach (var s in sapXep_LH)
			{
				if (s != "0") break;
				index++;
			}
			string opt = sapXep_LH[index];

			return new SoSanhHelper(index, opt);
		}
	}
}