using System;
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO
{
    public class StatistiqueDTO
    {
        public int NbDons { get; set; }
        public int NbUtilisateursInscrit { get; set; }
        public int NbCollecteTot { get; set; }
        public int NbDonsTot { get; set; }

        public StatistiqueDTO ()
        {
        }
    }
}