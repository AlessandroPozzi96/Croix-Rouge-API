using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Diffuserimage
    {
        public int Id { get; set; }
        public string FkImage { get; set; }
        public string FkUtilisateur { get; set; }

        public Imagepromotion FkImageNavigation { get; set; }
        public Utilisateur FkUtilisateurNavigation { get; set; }
    }
}
