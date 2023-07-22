using System.ComponentModel.DataAnnotations;

namespace Api_Web_React.Api.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LasName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
