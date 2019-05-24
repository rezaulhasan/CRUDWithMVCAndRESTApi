using RentMovie.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentMovie.ViewModel
{
    public class MovieFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(0, 100)]
        [Display(Name = "Copies in Stock")]
        public int? CopiesInStock { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public MovieFormViewModel()
        {

            this.Id = 0;

        }

        public MovieFormViewModel(Movie movie)
        {
            this.Id = movie.Id;
            this.Name = movie.Name;
            this.ReleaseDate = movie.ReleaseDate;
            this.CopiesInStock = movie.CopiesInStock;
            this.GenreId = movie.GenreId;
        }
    }
}