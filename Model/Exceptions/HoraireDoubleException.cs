using System;

namespace CroixRouge.Model.Exceptions
{
    public class HoraireDoubleException : PersonnalException 
    {
        public HoraireDoubleException() : base("Jour et la date sont tous les 2 remplit")
        {

        }
    }
}