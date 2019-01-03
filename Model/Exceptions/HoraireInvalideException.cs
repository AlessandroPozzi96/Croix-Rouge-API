using System;

namespace CroixRouge.Model.Exceptions
{
    public class HoraireInvalideException : PersonnalException 
    {
        public HoraireInvalideException() : base("L'horaire de fin est supérieur ou égal à l'horaire du début")
        {

        }
    }
}