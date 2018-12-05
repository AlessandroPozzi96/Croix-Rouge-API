using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Concerne
    {
        public int Id { get; set; }
        public int FkAlerte { get; set; }
        public string FkGroupesanguin { get; set; }

        public Alerte FkAlerteNavigation { get; set; }
        public Groupesanguin FkGroupesanguinNavigation { get; set; }
    }
}
