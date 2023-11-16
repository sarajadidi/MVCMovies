using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCMovies.Models;

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

        public IActionResult GenerateRandomMovie(string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                // Handle the case where no genre is selected
                return BadRequest("Please select a genre.");
            }

            try
            {
                var moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre == genre).ToList();

                if (moviesInGenre.Count == 0)
                {
                    // Handle the case where no movie is found for the selected genre
                    return NotFound($"No movies found for the genre: {genre}");
                }

                var random = new Random();
                var randomMovie = moviesInGenre.OrderBy(m => random.Next()).FirstOrDefault();

                return View(randomMovie);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
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

        public IActionResult UpdateMovie(int id)
        {
            Movie mov = _repo.GetMovie(id);
            if (mov == null)
            {
                return View("MovieNotFound");
            }
            return View(mov);
        }

        public IActionResult UpdateMovieToDatabase(Movie movie)
        {
            _repo.UpdateMovie(movie);
            return RedirectToAction("ViewMovie", new { id = movie.MovieID });
        }

        public IActionResult AddMovie(Movie movieToAdd)
        {
            if (movieToAdd == null)
            {
                return View("MovieNotFound");
            }
            return View(movieToAdd);
        }

        public IActionResult AddMovieToDatabase(Movie movie)
        {
            _repo.AddMovie(movie);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteMovie(Movie movie)
        {
            if (movie == null)
            {
                return View("MovieNotFound");
            }
            _repo.RemoveMovie(movie);
            return RedirectToAction("Index");

        }
        public IActionResult Generator()
        {
            return View();
        }
    }
}

