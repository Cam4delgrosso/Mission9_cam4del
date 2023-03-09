using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission9_cam4del.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Mission9_cam4del.Models.ViewModels;

namespace Mission9_cam4del.Controllers
{
    public class HomeController : Controller
    {

        //Using a repository instead of context
        private IBookstoreRepository repo;

        public HomeController(IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string bookCategory, int pageNum = 1)
        {
            int pageSize = 10;

            //Controls how many books are on each page
            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(b => b.Category == bookCategory || bookCategory == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = bookCategory == null ? 
                                        repo.Books.Count() 
                                        : repo.Books.Where(x => x.Category == bookCategory).Count(),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum

                }
            };
            return View(x);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
