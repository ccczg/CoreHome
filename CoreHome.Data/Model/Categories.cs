﻿using System.Collections.Generic;

namespace CoreHome.Data.Model
{
    public class Categories
    {
        public int Id { get; set; }

        public string CategoriesName { get; set; }

        public List<Article> Articles { get; set; }
    }
}
