using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Collecte
    {
        public Collecte()
        {
            Don = new HashSet<Don>();
            Jourouverture = new HashSet<Jourouverture>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int FkAdresse { get; set; }

        public Adresse FkAdresseNavigation { get; set; }
        public ICollection<Don> Don { get; set; }
        public ICollection<Jourouverture> Jourouverture { get; set; }
    }
}
