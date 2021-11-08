using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phase_4_Final_assessment.Models;

namespace Phase_4_Final_assessment.Controllers
{
    public class HomeController : Controller
    {

        PizzaDBContext pizza = new PizzaDBContext();
        public ActionResult<IList<PizzaDBContext>> Index()
        {
            var products = pizza.Product.ToList();
            return View(products);
        }

        public ActionResult<IList<PizzaDBContext>> Details(int? id)
        {
            var products = pizza.Product.Find(id);

            if (id == null)
            {
                return NotFound();
            }
            return View(products);
        }
    }
}

