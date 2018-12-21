using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Information
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Reponse { get; set; }
        public string FkUtilisateur { get; set; }

        public Utilisateur FkUtilisateurNavigation { get; set; }
    }
}
