using System; 
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO 
{
    public class GroupesanguinModel 
    {
        [Required]
        [StringLength(3, MinimumLength=2)]
        public string Nom {get; set;}
        public GroupesanguinModel()
        {

        }
    }
}