using EComJwtCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Application.DTOs
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public CategoryProduct Category { get; set; }

    }

    public class CategoryProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
