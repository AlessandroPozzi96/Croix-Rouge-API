using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Jourouverture
    {
        public int Id { get; set; }
        public byte? Jour { get; set; }
        public DateTime? Date { get; set; }
        public int FkCollecte { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public byte[] Rv { get; set; }

        public Collecte FkCollecteNavigation { get; set; }
    }
}
