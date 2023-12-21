using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.DependencyInjections;

namespace Demo.Controllers
{
    public class DIController : Controller
    {
        private readonly IServiceA _serviceA;
        private readonly IServiceA _serviceA1;
        private readonly IServiceA _serviceA2;
        public DIController(IServiceA serviceA)
        {
            this._serviceA = serviceA;
            this._serviceA1 = serviceA;
            this._serviceA2 = serviceA;
        }
        public IActionResult Index()
        {
            var id = _serviceA.GetId();

            var id1 = _serviceA1.GetId();
            var id2 = _serviceA2.GetId();
            ViewBag.Id = id;
            ViewBag.Id1 = id1;
            ViewBag.Id2 = id2;
            return View();
        }
    }
}
