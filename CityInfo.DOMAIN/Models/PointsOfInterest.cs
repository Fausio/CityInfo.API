using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.DOMAIN.Models
{
    [Table("PointsOfInterest")]
    public class PointsOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Description { get; set; }

        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int  CityId { get; set; }

        public PointsOfInterest(string name)
        {
            this.Name = name;
        }
    }
}
