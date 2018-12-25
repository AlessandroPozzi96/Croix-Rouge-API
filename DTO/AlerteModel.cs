using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class AlerteModel
    {
        [Required]
        public int Id { get; set; }
        [StringLength(100, MinimumLength=2)]
        [Required]
        public string Nom { get; set; }
        [StringLength(500, MinimumLength=2)]
        [Required]
        public string Contenu { get; set; }
        [Required]
        [StringLength(3, MinimumLength=2)]
        public string FkGroupesanguin { get; set; }
        public byte[] Rv { get; set; }

        public AlerteModel ()
        {

        }
    }
}