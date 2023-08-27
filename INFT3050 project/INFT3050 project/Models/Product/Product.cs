﻿using INFT3050_project.Models.Product.Subgenre;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFT3050_project.Models.Product
{
    public class Product
    {

            public int ID { get; set; }

            [Required(ErrorMessage = "Please enter a name.")]
            public string? Name { get; set; }

        [ForeignKey("GenreLink")]
            public int genre { get; set; }
            public Genre GenreLink { get; set; }

            [Required(ErrorMessage = "Please enter a Author.")]
            public String? Author { get; set; }

            [Required(ErrorMessage = "Please enter a Decsription.")]
            public String? Description { get; set; }

            [Required(ErrorMessage = "Please enter a genre.")]
            //public int GenreID { get; set; }
            public DateTime Published { get; set; }

            //public User LastUpdatedBy { get; set; }

            //public DateTime LastUpdated { get; set; }


    }
    
}
