using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPhotos.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult StatusCodePgesWithRedirects(int statusCode)
        {
            return View(statusCode);
        }
    }
}