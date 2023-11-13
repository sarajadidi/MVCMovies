using System;
using System.Data;
using Dapper;
using MVCMovies.Models;

namespace MVCMovies
{
	public class MovieRepo : IMovieRepo
	{
		private readonly IDbConnection _conn;

		public MovieRepo(IDbConnection conn)
		{
			_conn = conn;
		}

        public IEnumerable<Movie> GetAllMovies()
        {
			return _conn.Query<Movie>("SELECT * From MOVIE");
        }

		public Movie GetMovie(int id)
		{
			return _conn.QuerySingle<Movie>("SELECT * FROM MOVIE WHERE MOVIEID = @id", new { id = id });
		}

		public void UpdateMovie(Movie movie)
		{
			_conn.Execute("UPDATE movie SET Rating = @rating, RottenRating = @rottenrating WHERE MovieID = @id",
				new { rating = movie.Rating, rottenrating = movie.RottenRating, id = movie.MovieID });
		}

		public void AddMovie(Movie movieToAdd)
		{
			_conn.Execute("INSERT INTO movie (NAME, LEADACTOR, GENRE, YEAR, RATING, ROTTENRATING, DESCRIPTION) VALUES (@name, @leadActor, @genre, @year, @rating, @rottenRating, @description)",
				new { name = movieToAdd.Name, leadActor = movieToAdd.LeadActor, genre = movieToAdd.Genre, year = movieToAdd.Year, rating = movieToAdd.Rating, rottenRating = movieToAdd.RottenRating, description = movieToAdd.Description});
		}

		public void RemoveMovie(Movie movieToRemove)
		{
			_conn.Execute("DELETE FROM movie WHERE MovieId = @id", new { id = movieToRemove.MovieID });
		}
    }
}

