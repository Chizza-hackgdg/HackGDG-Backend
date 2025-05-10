using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ForumCategory: IEntity<Guid>
    {

        [Display(Name = "Category Name: ")]
        [StringLength(50, ErrorMessage = "Category name can not be longer than 50 characters!")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Description: ")]
        [StringLength(250, ErrorMessage = "Category description can not be longer than 250 characters!")]
        public string Description { get; set; }
    }
}
