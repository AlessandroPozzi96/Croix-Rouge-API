using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class AlerteModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Contenu { get; set; }
        public byte[] Rv { get; set; }

        public AlerteModel ()
        {

        }
    }
}