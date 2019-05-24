using RentMovie.Dtos;
using RentMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentMovie.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        public ApplicationDbContext _context { get; set; }
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (newRental.MovieIds.Count == 0)
                return BadRequest("No Movie ids have been given");

            var customer = _context.Customers
                           .SingleOrDefault(c => c.Id == newRental.CustomerId);

            if (customer == null)
                return BadRequest("Customer Id is not valid"); 

            var movies = _context.Movies
                        .Where(m => newRental.MovieIds
                        .Contains(m.Id)).ToList();

            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more movies are invalid"); 

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available"); 

                movie.NumberAvailable--; 
                var rental = new Rental()
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();
            return Ok(); 
        }
    }
}
