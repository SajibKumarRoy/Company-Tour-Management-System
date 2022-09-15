using Company_Tour_Management_System.Models;
using Company_Tour_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Company_Tour_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMainService obj;

        public HomeController(IMainService obj)
        {
            this.obj = obj;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult InitialList(int pageNumber = 1, string SearchText = "")
        {
            
            List<Participant> participant;
            if (SearchText != "" && SearchText != null)
            {
                participant = obj.GetInitial(SearchText);
            }
            else
            {
                participant = obj.GetParticipants(0);
            }
            int pageSize = 2;
            return View(PaginatedList<Participant>.Create(participant,pageNumber,pageSize));
        }
        public IActionResult FinalList(int pageNumber = 1, string SearchText = "")
        {

            List<Participant> participant;
            if (SearchText != "" && SearchText != null)
            {
                participant = obj.GetFinal(SearchText);
            }
            else
            {
                participant = obj.GetParticipants(1);
            }
            int pageSize = 2;
            return View(PaginatedList<Participant>.Create(participant, pageNumber, pageSize)); ;
        }
        public IActionResult AddParticipants()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddParticipants(Participant _obj)
        {
          
            obj.InsertP(_obj);
            return RedirectToAction("AddParticipants", "Home");
        }
        public IActionResult Delete(int? Id)
        {
            var participant = obj.get((int)Id);
            return View(participant);
        }
        [HttpPost]
        public IActionResult Delete(Participant p)
        {
            obj.delete(p);
            return RedirectToAction("InitialList", "Home");
        }
        public IActionResult Edit(int? Id)
        {
            var participant = obj.get((int)Id);
            return View(participant);
        }
        [HttpPost]
        public IActionResult Edit(Participant p)
        {
            obj.EditP(p);
            return RedirectToAction("InitialList", "Home");
        }
        public IActionResult Confirm(int? Id)
        {
            var participant = obj.get((int)Id);
            participant.State = 1;
            obj.EditP(participant);
            return RedirectToAction("InitialList", "Home");
        }
        public IActionResult Refund(int? Id)
        {
            var p = obj.get((int)Id);
            obj.delete(p);
            return RedirectToAction("FinalList", "Home");
        }
    }
}
