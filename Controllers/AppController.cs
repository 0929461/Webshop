using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly ILogger _logger;
        private readonly IDutchRepository _repository;

        public AppController(IMailService mailService, ILogger<AppController> logger, IDutchRepository repository)
        {
            _mailService = mailService;
            _logger = logger;
            _repository = repository;
            
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("Tahsin@live.nl", model.Subject, $"from: {model.Name}-{model.Email} - Message: {model.Message}");
                ViewBag.UserMessage = "Mail has been send";
                ModelState.Clear();
            }
            else
            {
                //show error page
                
                _logger.LogError("Failed. No information found");
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        //[Authorize]
        public IActionResult Shop()
        {
            var getAllProducts = _repository.GetAllProducts();
            
            return View(getAllProducts);
            /*
             first alternative - method syntax
            var results = _context.Products
                .OrderBy(x=>x.Category)
                .ToList();
            return View();
            
            second alternative query syntax
            var linqquery = from p in _context.Products
                            orderby p.Category
                            select p;
            return View(linqquery);
            */
        }
    }
}
