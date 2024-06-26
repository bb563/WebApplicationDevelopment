using System.Web;

namespace WebStore.Helpers
{
    public static class UserHelper
    {
        public static bool IsUserLoggedIn()
        {
            return HttpContext.Current.Session["CustomerID"] != null || HttpContext.Current.Session["AdminID"] != null;
        }

        public static string GetUserRole()
        {
            if (HttpContext.Current.Session["AdminID"] != null)
            {
                return "Admin";
            }
            else if (HttpContext.Current.Session["CustomerID"] != null)
            {
                return "Customer";
            }
            else
            {
                return null;
            }
        }
    }
}
