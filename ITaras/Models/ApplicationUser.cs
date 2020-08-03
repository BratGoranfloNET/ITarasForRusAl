using System;
using Microsoft.AspNetCore.Identity;

namespace ITaras.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string FavoriteColors { get; set; }
        public string FavoriteDrinks { get; set; }  
    }
}
