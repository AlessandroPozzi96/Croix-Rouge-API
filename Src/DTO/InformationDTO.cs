using System; 
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO 
{
    public class InformationDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(4000, MinimumLength=1)]
        public string Question { get; set; }
        [Required]
        [StringLength(4000, MinimumLength=1)]
        public string Reponse { get; set; }
        public InformationDTO()
        {

        }
    }
}