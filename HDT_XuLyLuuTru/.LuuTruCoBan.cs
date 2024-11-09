using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;
using HDT_22810201_Entities;

namespace HDT_22810201_XuLyLuuTru
{
	public abstract class LuuTruCoBan<T> : ILuuTruCoBan<T>
	{
		private static readonly string _rootFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low\\HDT_22810201\\";
		private static readonly string _databaseFolderPath = _rootFolderPath + "CoSoDuLieu\\";
		private static readonly string _resultFolderPath = _rootFolderPath + "KetQua\\";
		private static readonly string _loiFile = _databaseFolderPath + ".Loi.txt";
		private static readonly string _nguoiDungFile = _databaseFolderPath + "NguoiDung.json";
		private static readonly string _sanPhamFile = _databaseFolderPath + "SanPham.json";
		private static readonly string _hoaDonFile = _databaseFolderPath + "HoaDon.json";
		private static readonly string _zzzFile = _databaseFolderPath + "zzz.json";
		private readonly string _fileName;
		private readonly Func<List<T>> _ketQua;
		public LuuTruCoBan()
		{
			switch (typeof(T))
			{
				case Type type when type == typeof(NguoiDung):
					_fileName = _nguoiDungFile;
					_ketQua = () => { return (NguoiDung.TaoThuNghiem() as List<T>)!; };
					break;
				case Type type when type == typeof(SanPham):
					_fileName = _sanPhamFile;
					_ketQua = () => { return (SanPham.TaoThuNghiem() as List<T>)!; };
					break;
				case Type type when type == typeof(HoaDon):
					_fileName = _hoaDonFile;
					_ketQua = () => { return (HoaDon.TaoThuNghiem() as List<T>)!; };
					break;
				default:
					_fileName = _zzzFile;
					_ketQua = () => { return new List<T>(); };
					break;
			}
		}

		public List<T> DocDanhSach()
		{
			List<T> kq;
			try
			{
				// tạo thư mục và tệp nếu không có
				Directory.CreateDirectory(_databaseFolderPath);
				using (StreamWriter w = File.AppendText(_fileName)) { }

				// đọc tệp
				string jsonString = File.ReadAllText(_fileName);
				kq = JsonSerializer.Deserialize<List<T>>(jsonString)!;
			}
			catch (JsonException)
			{
				kq = _ketQua();
				LuuDanhSach(kq);
			}
			return kq;
		}
		public void LuuLoi(Exception ex)
		{
			Directory.CreateDirectory(_rootFolderPath);
			using (StreamWriter w = File.AppendText(_loiFile))
			{
				w.WriteLine($"[{DateTime.UtcNow.AddHours(7).ToString(new CultureInfo("vi-VN"))}]");
				w.WriteLine(ex.Message);
				w.WriteLine(ex.StackTrace);
			}
		}
		public void LuuDanhSach(List<T> danhSach)
		{
			// tạo thư mục và tệp nếu không có
			Directory.CreateDirectory(_databaseFolderPath);
			using (StreamWriter w = File.AppendText(_fileName)) { }

			// ghi tệp
			string jsonString = JsonSerializer.Serialize(danhSach);
			File.WriteAllText(_fileName, jsonString);
		}
		public void LuuKetQua(List<T> danhSach, string dt)
		{
			// tạo đường dẫn
			string path = _resultFolderPath + $"HetHan_{dt}.txt";

			// tạo thư mục và tệp nếu không có
			Directory.CreateDirectory(_resultFolderPath);
			using (StreamWriter w = File.AppendText(path)) { }

			// ghi tệp
			var options = new JsonSerializerOptions();
			options.WriteIndented = true;
			options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
			string jsonString = JsonSerializer.Serialize(danhSach, options);
			File.WriteAllText(path, jsonString);
		}
	}
}