using System;

namespace CroixRouge.api.Exceptions 
{
    public class PersonnalException:Exception
    {
        public PersonnalException(string message)
            :base(message)
        {
        }
    }
}