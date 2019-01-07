using System;

namespace CroixRouge.Model.Exceptions
{
    public class EmailDejaExistanteException : PersonnalException 
    {
        public EmailDejaExistanteException() : base("Cette adresse mail a déjà été enregistré")
        {

        }
    }
}