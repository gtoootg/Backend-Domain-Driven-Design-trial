using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Model.User;

namespace App.Domain.Model.Country;

public class CountryEntity
{
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public int Id { get; set; } 
     
     public string Name { get; set; }
     public string CountryCode { get; set; }
     
     public List<UserEntity> Users { get; set; }
}