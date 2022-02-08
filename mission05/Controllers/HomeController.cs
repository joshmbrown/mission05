using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using mission05.Models;

namespace mission05.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MovieContext _movieContext { get; set; }

        public HomeController(ILogger<HomeController> logger, MovieContext newMovie)
        {
            _logger = logger;
            _movieContext = newMovie;
        }

        //Home Page
        public IActionResult Index()
        {
            return View();
        }

        //Podcasts Page
        public IActionResult Podcasts()
        {
            return View();
        }

        //Add Movie Page
        [HttpGet]
        public IActionResult MovieForm()
        {
            ViewBag.Categories = _movieContext.Categories.ToList();
            NewMovie newMovie = new NewMovie();

            return View(newMovie);
        }

        [HttpPost]
        public IActionResult MovieForm(NewMovie movie)
        {
            if (ModelState.IsValid)
            {
                _movieContext.Add(movie);
                _movieContext.SaveChanges();

                return RedirectToAction("MovieList");
            }
            else
            {
                ViewBag.Categories = _movieContext.Categories.ToList();

                return View();
            }

        }

        //Movie List Page
        [HttpGet]
        public IActionResult MovieList()
        {
            var lstMovies = _movieContext.Movies
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .ToList();

            return View(lstMovies);
        }

        //Edit Movie
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _movieContext.Categories.ToList();
            var editMovie = _movieContext.Movies.Single(mov => mov.MovieID == id);

            return View("MovieForm", editMovie);
        }

        [HttpPost]
        public IActionResult Edit(NewMovie movie)
        {
            _movieContext.Update(movie);
            _movieContext.SaveChanges();

            return RedirectToAction("MovieList");
        }

        //Delete Movie
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deleteMovie = _movieContext.Movies.Single(mov => mov.MovieID == id);

            return View(deleteMovie);
        }

        [HttpPost]
        public IActionResult Delete(NewMovie mov)
        {
            _movieContext.Movies.Remove(mov);
            _movieContext.SaveChanges();

            return RedirectToAction("MovieList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
