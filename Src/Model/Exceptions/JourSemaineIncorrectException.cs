using System;

namespace CroixRouge.Model.Exceptions
{
    public class JourSemaineIncorrectException : PersonnalException 
    {
        public JourSemaineIncorrectException() : base("Le jour de la semaine est incorrect, il doit être compris entre 0 et 6")
        {

        }
    }
}