using System;
using System.ComponentModel.DataAnnotations;

namespace RentMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Genre Genre { get; set; } // "Genre" name has to be the same for the API TO WORK

        [Required]
        [Display(Name="Release Date")]
        public DateTime? ReleaseDate { get; set; }

        public DateTime? DateAdded { get; set; }

        [Required]
        [Range(0,100)]
        [Display(Name= "Copies in Stock")]
        public int CopiesInStock { get; set; }

        [Required]
        [Display(Name="Genre")]
        public int GenreId { get; set; }

        public byte NumberAvailable { get; set; }
    }
}