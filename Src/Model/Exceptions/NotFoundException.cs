using System;

namespace CroixRouge.Model.Exceptions
{
    public class NotFoundException : PersonnalException 
    {
        public NotFoundException(string entite) : base(entite + " non trouvé")
        {

        }
    }
}