using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAdminLTE.ViewComponents
{
    public class UserViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = TempData["userFullname"] as string;
            TempData.Keep();
            ViewData["userFullname"] = data;
            return View("UserVC");
        }
    }
}
