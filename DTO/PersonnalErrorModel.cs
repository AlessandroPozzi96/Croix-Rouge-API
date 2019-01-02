using System;

namespace CroixRouge.DTO
{
    public class PersonnalErrorModel
    {
        public string Message { get; set; }
        public PersonnalErrorModel(string message)
        {
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}