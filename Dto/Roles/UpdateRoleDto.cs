﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Roles
{
    public class UpdateRoleDto
    {
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Rol adı boş bırakılamaz.")]
        [Display(Name = "Rol adı: ")]
        public string Name { get; set; } = null!;
    }
}
