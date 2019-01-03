using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class RoleDTO
    {
        [Required]
        public string Libelle {get; set;}

        public RoleDTO ()
        {

        }
    }
}