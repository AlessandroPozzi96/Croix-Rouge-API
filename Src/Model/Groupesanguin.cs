using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Groupesanguin
    {
        public Groupesanguin()
        {
            Alerte = new HashSet<Alerte>();
            Utilisateur = new HashSet<Utilisateur>();
        }

        public string Nom { get; set; }

        public ICollection<Alerte> Alerte { get; set; }
        public ICollection<Utilisateur> Utilisateur { get; set; }
    }
}
