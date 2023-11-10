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
    }
}

