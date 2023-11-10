using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCMovies.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepo _repo;

        public MovieController(IMovieRepo repo)
        {
            _repo = repo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var movie = _repo.GetAllMovies();
            return View(movie);
        }

        public IActionResult ViewMovie(int id)
        {
            var movie = _repo.GetMovie(id);
            return View(movie);
        }
    }
}

