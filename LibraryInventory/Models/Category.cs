﻿using System.Collections.Generic;

namespace LibraryInventory.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}