using System;
using System.Collections.Generic;

namespace model
{
    public partial class Jourouverture
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public int FkCollecte { get; set; }

        public Collecte FkCollecteNavigation { get; set; }
    }
}
