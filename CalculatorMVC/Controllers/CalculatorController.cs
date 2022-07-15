using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculatorMVC.Models;
using CalculatorMVC.BusinessLogic;

namespace CalculatorMVC.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICalculator _calculator;

        public CalculatorController(ICalculator calculator)
        {
            _calculator = calculator;
        }
        // GET: Calculator
        public ActionResult Calculator()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Calculate(CalculatorViewModel input)
        {
            ViewData["CalculatedValue"] = _calculator.Calculate(input.UserInput);

            return View("Calculator");
        }
    }
}
