using System; 
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO 
{
    public class GroupesanguinDTO
    {
        [Required]
        [StringLength(3, MinimumLength=2)]
        public string Nom {get; set;}
        public GroupesanguinDTO()
        {

        }
    }
}