using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication20.Models;

namespace MvcApplication20.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string name)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            return View(manager.SearchPeople(name));
        }

        [HttpPost]
        public ActionResult Add(string firstName, string lastName, int age)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            manager.Add(new Person
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age
            });
            return Redirect("/home/index");
        }

        [HttpPost]
        public ActionResult Delete(int personId)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            manager.Delete(personId);
            return Redirect("/home/index");
        }

    }
}
