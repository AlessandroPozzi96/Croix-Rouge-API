using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Don
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int FkCollecte { get; set; }
        public string FkUtilisateur { get; set; }

        public Collecte FkCollecteNavigation { get; set; }
        public Utilisateur FkUtilisateurNavigation { get; set; }
    }
}
