using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCExcerciseKLA.Models;

namespace MVCExcerciseKLA.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: Calculator
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Initial action that shows an empty and ready to use calculator
        /// </summary>
        /// <returns></returns>
        public ActionResult Show()
        {
            return View(new Calculator());
        }
        /// <summary>
        /// Action invoked when the equals sign is pressed on the calculator ( form is submitted)
        /// </summary>
        /// <param name="calcr"></param>
        /// <returns></returns>
        public ActionResult Calculate(Calculator calc)
        {
            if (ModelState.IsValid)
            {
                calc.Calculate();
            }
            return View("Show", calc);
        }



    }
}