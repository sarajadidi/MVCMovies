using System;
using MVCMovies.Models;

namespace MVCMovies
{
	public interface IMovieRepo
	{
        public IEnumerable<Movie> GetAllMovies();
        public Movie GetMovie(int id);
        public void UpdateMovie(Movie movie);

        public void AddMovie(Movie movieToAdd);
        public void RemoveMovie(Movie movieToRemove);
    }
}

