using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class TrancheHoraireModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public TimeSpan HeureDebut { get; set; }
        [Required]
        public TimeSpan HeureFin { get; set; }

        public TrancheHoraireModel ()
        {

        }
    }
}