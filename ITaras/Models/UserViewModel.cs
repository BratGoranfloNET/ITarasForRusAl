using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ITaras.Models
{
    public class UserViewModel
    {
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "BirthDate")]
        public string BirthDate { get; set; }

        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "FavoriteColors")]
        public string FavoriteColors { get; set; }

        [JsonProperty(PropertyName = "FavoriteDrinks")]
        public string FavoriteDrinks { get; set; }

    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
