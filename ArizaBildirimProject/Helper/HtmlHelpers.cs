using ArizaBildirimProject.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ArizaBildirimProject.Helpers
{
    public static class HtmlHelpers
    {
        public static string UserProfilePictureUrl(this IHtmlHelper htmlHelper)
        {
            var userId = htmlHelper.ViewContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                // userId null ise varsayılan bir profil resmi döndürün
                return "/default/path/to/profile.jpg";
            }

            var context = htmlHelper.ViewContext.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            if (context == null)
            {
                throw new InvalidOperationException("Database context could not be retrieved.");
            }

            // userId'nin null olmadığından emin olduktan sonra parse işlemi
            if (int.TryParse(userId, out int parsedUserId))
            {
                var user = context.Users.Find(parsedUserId);
                return user?.ProfilePicturePath ?? "/default/path/to/profile.jpg";
            }
            else
            {
                // Eğer userId geçerli bir integer değilse, varsayılan bir profil resmi döndürün
                return "/default/path/to/profile.jpg";
            }
        }
    }



}
