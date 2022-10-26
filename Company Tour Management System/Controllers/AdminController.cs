using Microsoft.AspNetCore.Mvc;
using Company_Tour_Management_System.Models;
using Company_Tour_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Threading;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Company_Tour_Management_System.Controllers
{
    public class AdminController : Controller
    {
        #region Ctor
        private readonly IMainService obj;
        private readonly IHostingEnvironment env;
        public AdminController(IMainService obj, IHostingEnvironment env)
        {
            this.env = env;
            this.obj = obj;
        }
        #endregion
        #region Methods
        [Authorize (Roles ="Admin")]
        public IActionResult Index(string dept="",int pageNumber = 1, string SearchText = "",
        int page = 0,string HR="",string Devlopment="")
        {
            int pageSize = 4;
            if (page != 0)
            {
                pageNumber = page;
            }
            if (dept != "")
            {
                if (dept == "HR")
                    HR = dept;
                else if (dept == "Devlopment")
                    Devlopment = dept;
                else if (dept == "both")
                {
                    HR = "HR";
                    Devlopment = "Devlopment";
                }
            }
            List<Participant> participant;
            if (HR != "" && Devlopment!="" )
            {
                ViewBag.HR = "HR";
                ViewBag.Devlopment = "Devlopment";
                SearchText = "";
            }
            else if(HR!="")
            {
                ViewBag.HR = "HR";
                participant = obj.GetChkItems(HR);
                return View(PaginatedList<Participant>.Create(participant, pageNumber, pageSize));
            }
            else if(Devlopment!="")
            {
                ViewBag.Devlopment = "Devlopment";
                participant = obj.GetChkItems(Devlopment);
                return View(PaginatedList<Participant>.Create(participant, pageNumber, pageSize));
            }
            if (SearchText != "" && SearchText != null)
            {
                participant = obj.GetInitial(SearchText);
            }
            else
            {
                participant = obj.GetParticipants(0);
            }

            return View(PaginatedList<Participant>.Create(participant, pageNumber, pageSize));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult FinalList(string dept = "",int pageNumber = 1, string SearchText = "",
            int page=0, string HR = "", string Devlopment = "")
        {
            int pageSize = 4;
            if (page!=0)
            {
                pageNumber = page;
            }
            if (dept != "")
            {
                if (dept == "HR")
                    HR = dept;
                else if (dept == "Devlopment")
                    Devlopment = dept;
                else if (dept == "both")
                {
                    HR = "HR";
                    Devlopment = "Devlopment";
                }
            }
            List<Participant> participant;
            if (HR != "" && Devlopment != "")
            {
                ViewBag.HR = "HR";
                ViewBag.Devlopment = "Devlopment";
                SearchText = "";
            }
            else if (HR != "")
            {
                ViewBag.HR = "HR";
                participant = obj.GetChkItemsFinal(HR);
                return View(PaginatedList<Participant>.Create(participant, pageNumber, pageSize));
            }
            else if (Devlopment != "")
            {
                ViewBag.Devlopment = "Devlopment";
                participant = obj.GetChkItemsFinal(Devlopment);
                return View(PaginatedList<Participant>.Create(participant, pageNumber, pageSize));
            }
            if (SearchText != "" && SearchText != null)
            {
                participant = obj.GetFinal(SearchText);
            }
            else
            {
                participant = obj.GetParticipants(1);
            }
            
            return View(PaginatedList<Participant>.Create(participant, pageNumber, pageSize)); ;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddParticipants()
        {
            ViewBag.Success = false;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddParticipants(Participant _obj)
        {

            await obj.InsertPAsync(_obj);
            ViewBag.Success = true;
            ModelState.Clear();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? Id)
        {
            var participant = obj.get((int)Id);
            return View(participant);
        }
    
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Participant p)
        {
           await obj.deleteAsync(p);
            return RedirectToAction("Index", "Admin");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? Id)
        {
            var participant = obj.get((int)Id);
            return View(participant);
        }
     
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Participant p)
        {
            await obj.EditPAsync(p);
            return RedirectToAction("Index", "Admin");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Confirm(int? Id)
        {
            var participant = obj.get((int)Id);
            participant.State = 1;
           await obj.EditPAsync(participant);
            return RedirectToAction("Index", "Admin");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Refund(int? Id)
        {
            var p = obj.get((int)Id);
            await obj.deleteAsync(p);
            return RedirectToAction("FinalList", "Admin");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadImage(ImageCreateModel img)
        {
            if (ModelState.IsValid)
            {
                var path = env.WebRootPath;
                var filepath = "images/" + img.ImagePath.FileName;
                var fullpath = Path.Combine(path, filepath);
                UploadFile(fullpath, img.ImagePath);
                var data = new Image()
                {
                    Name = img.Name,
                    filePath = "/" + filepath
                };
               await obj.imgsaveAsync(data);
                return RedirectToAction("ImageGallery");
            }

            return View(img);

        }
        [Authorize(Roles = "Admin")]
        public void UploadFile(string path, IFormFile file)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ImageGallery()
        {
            var imgData =await obj.imggetAsync();
            if (imgData == null)
                return View();
            else
                return View(imgData);
        }
        [HttpPost]
        [Authorize (Roles ="Admin")]
        public async Task<IActionResult> ImageGallery(int? id)
        {
           await obj.imgDeleteAsync((int)id);
            return View();
        }
        #endregion
    }
}
