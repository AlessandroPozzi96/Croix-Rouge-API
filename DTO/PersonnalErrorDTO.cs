using System;

namespace CroixRouge.DTO
{
    public class PersonnalErrorDTO
    {
        public string Message { get; set; }
        public PersonnalErrorDTO(string message)
        {
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}