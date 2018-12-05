using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Role
    {
        public Role()
        {
            Utilisateur = new HashSet<Utilisateur>();
        }

        public string Libelle { get; set; }

        public ICollection<Utilisateur> Utilisateur { get; set; }
    }
}
