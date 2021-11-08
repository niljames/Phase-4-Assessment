using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phase_4_Final_assessment.Helpers;
using Phase_4_Final_assessment.Models;
using Stripe;


namespace Phase_4_Final_assessment.Controllers
{
    public class CartController : Controller
    {
        PizzaDBContext pizza = new PizzaDBContext();
        public IActionResult Index()
        {
            if (SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart") == null)
                return View("EmptyCart");
            else
            {
                var cart = (SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart"));
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.product.Price * item.Quantity);
                string t = (ViewBag.total).ToString();
                HttpContext.Session.SetString("total", t);
                return View("Index");
            }
        }

        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart") == null)
            {
                List<Items> cart = new List<Items>();
                cart.Add(new Items { product = pizza.Product.Find(id), Quantity = 1 });
                SessionHelper.setObjectasJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Items> cart = SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart");
                int index = IsExist(id);
                if (index != -1)
                    cart[index].Quantity++;
                else
                    cart.Add(new Items { product = pizza.Product.Find(id), Quantity = 1 });
                SessionHelper.setObjectasJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("index");
        }

        public int IsExist(int id)
        {
            List<Items> cart = SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].product.ProductId == id)
                    return i;
            }
            return -1;
        }

        public IActionResult Checkout()
        {
           
                string amount = HttpContext.Session.GetString("total");
                ViewBag.Amount = Convert.ToDouble(amount);
                return View("Checkout");
          

        }

        public ActionResult Payment()
        {
            ViewBag.StripePublishKey = ConfigurationManager.AppSettings["pk_test_51JaFRgSC1tqaOG4n6Yqw1ecAJbe8C6E8xvxoVsW5JSu4jJqtPgU3uWO9mUMDllwVJw884wLHPhjc5mwwCdZL576l00KtVFiOGC"];
            return View();
        }

        [HttpPost]
        [Obsolete]
        public ActionResult Charge(string stripeToken, string stripeEmail)
        {
            StripeConfiguration.SetApiKey("pk_test_51JaFRgSC1tqaOG4n6Yqw1ecAJbe8C6E8xvxoVsW5JSu4jJqtPgU3uWO9mUMDllwVJw884wLHPhjc5mwwCdZL576l00KtVFiOGC");
            StripeConfiguration.ApiKey = "sk_test_51JaFRgSC1tqaOG4nqwb6D2jCpaZw3VxOtRLWzhXCIKtwTFEoKXs54bTYJFRsaOLfg3wyVFAsIOxT8H6BIyLUvpRR00OLQ2Daxg";
            string amount = HttpContext.Session.GetString("total");

            var myCharge = new ChargeCreateOptions();
            // always set these properties
            myCharge.Amount = (long?)Convert.ToDouble(amount); ;
            myCharge.Currency = "USD";
            myCharge.ReceiptEmail = stripeEmail;
            myCharge.Description = "Payment";
            myCharge.Source = stripeToken;
            myCharge.Capture = true;
            ViewBag.Token = myCharge.Source;
            var cart = (SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart"));
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.product.Price * item.Quantity);
            string t = (ViewBag.total).ToString();
            HttpContext.Session.SetString("total", t);
            return View("Charge");
        }
    }
}
