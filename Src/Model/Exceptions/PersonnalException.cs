using System;

namespace CroixRouge.Model.Exceptions 
{
    public class PersonnalException:Exception
    {
        public PersonnalException(string Message)
            :base(Message)
        {
        }
    }
}