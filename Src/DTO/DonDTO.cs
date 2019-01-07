using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
     public class DonDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int FkCollecte { get; set; }
        [Required]
        [StringLength(50, MinimumLength=3)]
        public string FkUtilisateur { get; set; }
        public DonDTO ()
        {

        }

    }
}