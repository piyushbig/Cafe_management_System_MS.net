namespace RolesAuth.Models
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        public string Address { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
