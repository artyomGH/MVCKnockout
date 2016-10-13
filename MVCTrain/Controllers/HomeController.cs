using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTrain.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        MVCTrainDBEntities db = new MVCTrainDBEntities();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PostPerson(Person person)
        {
            int? id = db.People.Max(u => (int?)u.Id);
            if (id != null)
                person.Id = (int)id + 1;
            else
                person.Id = 0;
            db.People.Add(person);            
            db.SaveChanges();
            Person selectedPerson = new Person();
            selectedPerson = db.People.Where(t => t.Id == db.People.Max(i => i.Id)).First();            
            return Json(selectedPerson, JsonRequestBehavior.AllowGet);
        }        
        public JsonResult GetPeople()
        {            
            JsonResult jsonPeople = Json(db.People, JsonRequestBehavior.AllowGet);
            return jsonPeople;
        }
    }
}