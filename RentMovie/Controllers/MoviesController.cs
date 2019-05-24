using RentMovie.Models;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using RentMovie.ViewModel;

namespace RentMovie.Controllers
{
    public class MoviesController : Controller
    {
        public ApplicationDbContext _Context { get; set; } //Database instance .. 

        //Constructor
        public MoviesController()
        {
            _Context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }

        //Index Action
        public ActionResult Index()
        {
            var movie = _Context.Movies.ToList();
            return new JsonResult { Data = movie, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 
        }

        //Edit Action
        public ActionResult Edit(int id)
        {
            var movie = _Context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _Context.Genres.ToList()
            };
            return View("NewMovieForm", viewModel);
        }

        //New Action
        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = _Context.Genres.ToList()
            };
            return View("NewMovieForm", viewModel);
        }

        //Save Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid) // Model state is checked for a new and old movie 
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _Context.Genres.ToList()
                };
                return View("NewMovieForm", viewModel);
            }

            if (movie.Id == 0) //For a new movie 
            {
                _Context.Movies.Add(movie);
                _Context.SaveChanges();
            }
            else  // "A movie coming from the edit action"
            {
                var movieIndb = _Context.Movies.Single(m => m.Id == movie.Id);

                movieIndb.Name = movie.Name;
                movieIndb.ReleaseDate = movie.ReleaseDate;
                movieIndb.GenreId = movie.GenreId;
                movieIndb.CopiesInStock = movie.CopiesInStock;

                _Context.SaveChanges();
            }
            return RedirectToAction("Index", "Movies");
        }
    }
}