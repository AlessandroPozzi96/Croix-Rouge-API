using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class CollecteDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength=2)]
        public string Nom { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        public int? Telephone { get; set; }
        public byte[] Rv { get; set; }
        public JourouvertureDTO[] Jourouverture { get; set; }

        public CollecteDTO ()
        {

        }
    }
}