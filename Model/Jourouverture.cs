using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Jourouverture
    {
        public int Id { get; set; }
        public string LibelleJour { get; set; }
        public DateTime? Date { get; set; }
        public int FkCollecte { get; set; }
        public int FkTrancheHoraire { get; set; }

        public Collecte FkCollecteNavigation { get; set; }
        public TrancheHoraire FkTrancheHoraireNavigation { get; set; }
    }
}
