using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Lanceralerte
    {
        public int Id { get; set; }
        public int FkAlerte { get; set; }
        public string FkUtilisateur { get; set; }

        public Alerte FkAlerteNavigation { get; set; }
        public Utilisateur FkUtilisateurNavigation { get; set; }
    }
}
