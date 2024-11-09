using HDT_22810201_Entities;
using HDT_22810201_XuLyLuuTru;
using HDT_22810201_XuLyNghiepVu;

namespace HDT_22810201
{
	static class ServicesExtension
	{
		public static void MyServices(this IServiceCollection services)
		{
			services.AddRazorPages();
			services.Configure<RouteOptions>(opt =>
			{
				opt.AppendTrailingSlash = true;
				opt.LowercaseUrls = true;
				opt.LowercaseQueryStrings = false;
			});
			services.AddMvc().AddRazorPagesOptions(options =>
			{
				options.Conventions.AddPageRoute("/TaiKhoan/DangNhap", "");
			});
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(10);
			});
			services
				.AddScoped<IXuLyNguoiDung, XuLyNguoiDung>()
				.AddScoped<IXuLySanPham, XuLySanPham>()
				.AddScoped<IXuLyLoaiHang, XuLyLoaiHang>()
				.AddScoped<IXuLyHoaDon, XuLyHoaDon>()
				.AddScoped<IXuLyThongKe, XuLyThongKe>()
				.AddScoped<ILuuTruNguoiDung<NguoiDung>, LuuTruNguoiDung<NguoiDung>>()
				.AddScoped<ILuuTruSanPham<SanPham>, LuuTruSanPham<SanPham>>()
				.AddScoped<ILuuTruHoaDon<HoaDon>, LuuTruHoaDon<HoaDon>>()
				.AddScoped<ILuuTruThongKe<SanPham>, LuuTruThongKe<SanPham>>();
		}
	}
	static class WebApplicationExtension
	{
		public static void MyConfigs(this WebApplication app)
		{
			app.MapRazorPages();
			app.UseStaticFiles();

			app.UseHsts();
			app.UseHttpsRedirection();

			app.UseSession();
			app.UseStatusCodePagesWithRedirects("/Loi/{0}");
		}
	}
	static class Program
	{
		static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.WebHost.UseStaticWebAssets();
			builder.Services.MyServices();

			var app = builder.Build();

			app.MyConfigs();

			app.Run();
		}
	}
}