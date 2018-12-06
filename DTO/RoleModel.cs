using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class RoleModel
    {
        [Required]
        public string Libelle {get; set;}

        public RoleModel ()
        {

        }
    }
}