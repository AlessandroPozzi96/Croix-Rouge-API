using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class AdresseModel
    {
        [Required]
        public int Id { get; set; }
        [StringLength(100, MinimumLength=2)]
        [Required]
        public string Ville { get; set; }
        [StringLength(100, MinimumLength=2)]
        [Required]
        public string Rue { get; set; }
        [StringLength(4, MinimumLength=1)]
        [Required]
        public string Numero { get; set; }
        public byte[] Rv { get; set; }

        public AdresseModel ()
        {

        }
    }
}