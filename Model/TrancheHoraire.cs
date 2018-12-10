using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class TrancheHoraire
    {
        public TrancheHoraire()
        {
            Jourouverture = new HashSet<Jourouverture>();
        }

        public int Id { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }

        public ICollection<Jourouverture> Jourouverture { get; set; }
    }
}
