using System;
using System.Collections.Generic;

namespace model
{
    public partial class Alerte
    {
        public Alerte()
        {
            Concerne = new HashSet<Concerne>();
            Lanceralerte = new HashSet<Lanceralerte>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Contenu { get; set; }
        public string FkUtilisateur { get; set; }

        public Utilisateur FkUtilisateurNavigation { get; set; }
        public ICollection<Concerne> Concerne { get; set; }
        public ICollection<Lanceralerte> Lanceralerte { get; set; }
    }
}
