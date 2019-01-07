using System; 
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO 
{
    public class DiffuserimageDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int FkImage { get; set; }
        [Required]
        [StringLength(50, MinimumLength=3)]
        public string FkUtilisateur { get; set; }
        public DiffuserimageDTO()
        {

        }
    }
}