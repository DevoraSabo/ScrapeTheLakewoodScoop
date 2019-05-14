using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScrapeTLS.Data;
using ScrapeTLS.Web.Models;

namespace ScrapeTLS.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var api = new TlsApi();
            List<News> items = TlsApi.ScrapeTLS();
            return View(items);
        }

        public IActionResult Search(string query)
        {
            var api = new TlsApi();
            List<News> items = TlsApi.ScrapeTLS(query);
            return View(items);
        }

        public IActionResult SearchIt()
        {
            return View();
        }
    }
}
