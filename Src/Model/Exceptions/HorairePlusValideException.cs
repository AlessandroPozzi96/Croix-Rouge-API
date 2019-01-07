using System;

namespace CroixRouge.Model.Exceptions
{
    public class HorairePlusValideException : PersonnalException 
    {
        public HorairePlusValideException() : base("La date est inférieur à la date actuelle")
        {

        }
    }
}