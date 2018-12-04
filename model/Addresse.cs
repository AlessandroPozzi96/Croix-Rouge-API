using System;
using System.Collections.Generic;

namespace model
{
    public partial class Addresse
    {
        public Addresse()
        {
            Collecte = new HashSet<Collecte>();
            Utilisateur = new HashSet<Utilisateur>();
        }

        public int Id { get; set; }
        public string Ville { get; set; }
        public string Rue { get; set; }
        public string Numero { get; set; }

        public ICollection<Collecte> Collecte { get; set; }
        public ICollection<Utilisateur> Utilisateur { get; set; }
    }
}
