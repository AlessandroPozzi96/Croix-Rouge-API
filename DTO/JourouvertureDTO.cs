using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class JourouvertureDTO
    {
        [Required]
        public int Id { get; set; }
        [StringLength(8, MinimumLength=2)]
        public byte? Jour { get; set; }
        public DateTime? Date { get; set; }
        [Required]
        public int FkCollecte { get; set; }
        [Required]
        public TimeSpan HeureDebut { get; set; }
        [Required]
        public TimeSpan HeureFin { get; set; }
        public byte[] Rv { get; set; }

        public JourouvertureDTO ()
        {

        }
    }
}