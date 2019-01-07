using System; 
using System.ComponentModel.DataAnnotations;

namespace CroixRouge.DTO 
{
    public class ImagepromotionDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(2083, MinimumLength=1)]
        public string Url { get; set; }
        [Required]
        [StringLength(500, MinimumLength=1)]
        public string Description { get; set; }
        public byte[] Rv { get; set; }
        public ImagepromotionDTO()
        {

        }
    }
}