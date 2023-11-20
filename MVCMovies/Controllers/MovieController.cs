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
                IEnumerable<Movie> moviesInGenre;

                switch (genre.ToLower())
                {
                    case "drama":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Drama")).ToList();
                        break;

                    case "action":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Action")).ToList();
                        break;
                    case "fantasy":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Fantasy")).ToList();
                        break;
                    case "science fiction":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Science Fiction")).ToList();
                        break;
                    case "musical":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Musical")).ToList();
                        break;
                    case "thriller":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Thriller")).ToList();
                        break;
                    case "war":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("War")).ToList();
                        break;
                    case "comedy":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Comedy")).ToList();
                        break;
                    case "romance":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Romance")).ToList();
                        break;
                    case "superhero":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Superhero")).ToList();
                        break;
                    case "animation":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Animation")).ToList();
                        break;
                    case "adventure":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Adventure")).ToList();
                        break;
                    case "crime":
                        moviesInGenre = _repo.GetAllMovies().Where(m => m.Genre.Contains("Crime")).ToList();
                        break;

                    default:
                        return BadRequest($"Unsupported genre: {genre}");
                }

                if (moviesInGenre.Count() == 0)
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

