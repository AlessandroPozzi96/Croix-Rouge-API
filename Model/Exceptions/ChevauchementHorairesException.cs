using System;

namespace CroixRouge.Model.Exceptions
{
    public class ChevauchementHorairesException : PersonnalException 
    {
        public ChevauchementHorairesException() : base("Erreur les horaires se chevauchent")
        {

        }
    }
}