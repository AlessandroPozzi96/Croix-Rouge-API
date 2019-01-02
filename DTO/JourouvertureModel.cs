using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class JourouvertureModel
    {
        [Required]
        public int Id { get; set; }
        [StringLength(8, MinimumLength=5)]
        public string LibelleJour { get; set; }
        public DateTime? Date { get; set; }
        [Required]
        public int FkCollecte { get; set; }
        [Required]
        public TimeSpan HeureDebut { get; set; }
        [Required]
        public TimeSpan HeureFin { get; set; }

        public JourouvertureModel ()
        {

        }
    }
}