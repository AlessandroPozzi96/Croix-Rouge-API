using System;

namespace CroixRouge.Model.Exceptions
{
    public class HoraireManquantException : PersonnalException 
    {
        public HoraireManquantException() : base("Le jour et la date sont vides")
        {

        }
    }
}