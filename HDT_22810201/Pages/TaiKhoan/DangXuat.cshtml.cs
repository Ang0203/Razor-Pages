using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HDT_22810201.Pages
{
	public class DangXuatModel : PageModel
	{
		public void OnGet()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
				Response.Redirect("/tai-khoan/dang-nhap");
		}
		public void OnPost()
		{
			HttpContext.Session.SetString("username", "");
			Response.Redirect("/tai-khoan/dang-nhap");
		}
	}
}