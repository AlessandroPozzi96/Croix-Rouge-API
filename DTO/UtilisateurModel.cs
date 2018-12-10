using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class UtilisateurModel
    {
        [StringLength(50, MinimumLength=3)]
        [Required]
        public string Login { get; set; }
        [StringLength(200, MinimumLength=8)]
        [Required]
        public string Password { get; set; }
        [StringLength(100, MinimumLength=2)]
        public string Nom { get; set; }
        [StringLength(100, MinimumLength=2)]
        public string Prenom { get; set; }
        [StringLength(320, MinimumLength=4)]
        [Required]
        public string Mail { get; set; }
        public int NumGsm { get; set; }
        public DateTime DateNaissance { get; set; }
        public bool IsMale { get; set; }
        public int Score { get; set; }
        [StringLength(50, MinimumLength=3)]
        [Required]
        public string FkRole { get; set; }
        public int FkAdresse { get; set; }
        [StringLength(3, MinimumLength=2)]
        public string FkGroupesanguin { get; set; }
        public byte[] Rv { get; set; }

        public UtilisateurModel ()
        {

        }
    }
}