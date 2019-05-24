using System;
using RentMovie.Dtos;
using System.Data.Entity; 
using RentMovie.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace RentMovie.Controllers.Api
{
    public class MoviesController : ApiController
    {
        #region Context and Constructor Initialization 
        private ApplicationDbContext _context { get; set; }

        public MoviesController()
        {
            _context = new ApplicationDbContext(); 
        }
        #endregion

        #region GET Movies
        //GET/api/movies 
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                             .Include(m => m.Genre)
                             .Where(m => m.NumberAvailable > 0);

            if (!string.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query)); 

            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }
        #endregion

        #region GET Movie
        //GET/api/movies/1
        public IHttpActionResult GetMovie (int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return NotFound(); 

            return Ok (Mapper.Map<Movie, MovieDto>(movie)); 
        }
        #endregion

        #region Add Movie
        //Need to check the "ModelState" when "Posting", "Updating" a model 
        //POST/api/movies  ... post a movie to the customer collection 
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); // From IHttpActionResult ... 

            var movie = Mapper.Map<MovieDto, Movie>(movieDto); 

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id; 
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto); 
        }
        #endregion

        #region Update Movie
        //Need to check the "ModelState" when "Posting", "Updating" a database  
        //PUT/api/movie/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); 
           
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound(); 

            Mapper.Map(movieDto, movieInDb); 

            _context.SaveChanges();
            return Ok(); 
        }
        #endregion

        #region Delete Movie
        //DELETE/api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound(); 
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok(); 
        }
        #endregion
    }
}
