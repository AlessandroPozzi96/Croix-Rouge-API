using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class HoraireModel
    {
        [Required]
        public int IdCollecte { get; set; }
        [StringLength(8, MinimumLength=5)]
        public string LibelleJour { get; set; }
        public DateTime? Date { get; set; }
        public string HeureDebut { get; set; }
        public string HeureFin { get; set; }

        public HoraireModel (int idCollecte, string libelleJour, DateTime? date, string heureDebut, string heureFin)
        {
            IdCollecte = idCollecte;
            libelleJour = LibelleJour;
            Date = date;
            HeureDebut = heureDebut;
            HeureFin = heureFin;
        }

        public HoraireModel() 
        {
            
        }
    }
}