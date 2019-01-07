using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Alerte
    {
        public Alerte()
        {
            Lanceralerte = new HashSet<Lanceralerte>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Contenu { get; set; }
        public string FkGroupesanguin { get; set; }
        public byte[] Rv { get; set; }

        public Groupesanguin FkGroupesanguinNavigation { get; set; }
        public ICollection<Lanceralerte> Lanceralerte { get; set; }
    }
}
