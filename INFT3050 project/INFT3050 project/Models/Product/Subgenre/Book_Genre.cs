﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

//info for the book genre's
namespace INFT3050_project.Models.Product.Subgenre
{
    [Table("Book_genre")]
    public class Book_Genre : ISubGenre
    {
        [Column("subGenreID")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}