using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Groupesanguin
    {
        public Groupesanguin()
        {
            Concerne = new HashSet<Concerne>();
            Utilisateur = new HashSet<Utilisateur>();
        }

        public string Nom { get; set; }

        public ICollection<Concerne> Concerne { get; set; }
        public ICollection<Utilisateur> Utilisateur { get; set; }
    }
}
