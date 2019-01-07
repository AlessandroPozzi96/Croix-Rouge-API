using System;
using System.Collections.Generic;

namespace CroixRouge.Model
{
    public partial class Imagepromotion
    {
        public Imagepromotion()
        {
            Diffuserimage = new HashSet<Diffuserimage>();
            Partagerimage = new HashSet<Partagerimage>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public byte[] Rv { get; set; }

        public ICollection<Diffuserimage> Diffuserimage { get; set; }
        public ICollection<Partagerimage> Partagerimage { get; set; }
    }
}
