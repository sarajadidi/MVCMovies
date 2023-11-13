using System;
namespace MVCMovies.Models
{
	public class Movie
	{
		public int MovieID { get; set; }
        public string Name { get; set; }
        public string LeadActor { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Rating { get; set; }
        public double RottenRating { get; set; }
        public string Description { get; set; }

    }
}

