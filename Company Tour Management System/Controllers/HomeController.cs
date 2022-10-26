using Company_Tour_Management_System.Models;
using Company_Tour_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Threading;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Company_Tour_Management_System.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Ctror
        private readonly IMainService obj;
        private readonly IHostingEnvironment env;
        public HomeController(IMainService obj, IHostingEnvironment env)
        {
            this.env = env;
            this.obj = obj;
        }
        #endregion
        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DestinationGallery()
        {
            var imgData =await obj.imggetAsync();
            if (imgData == null)
                return View();
            else
                return View(imgData);
        }

        public IActionResult RequestPage()
        {
            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> RequestPage(Participant _obj)
        {
           
            await obj.InsertPAsync(_obj);
            ViewBag.Success = true;
            ModelState.Clear();
            return View();
        }
        #endregion

    }

}
