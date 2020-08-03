using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using ITaras.Models.ViewModels;

namespace ITaras.Models
{
    public static class Helper
    {
        public static string ConvertColors(string colorscodes)
        {
            if (colorscodes != null)
            {
                StringBuilder sb = new StringBuilder();
                char[] arr;
                arr = colorscodes.ToCharArray();
                sb.Append(arr[0] == '1' ? "Синий " : "");
                sb.Append(arr[1] == '1' ? "Желтый " : "");
                sb.Append(arr[2] == '1' ? "Красный " : "");
                return sb.ToString();
            }
            else
            {
                return " ";
            }           

        }

        public static string ConvertDrinks(string drinkscodes)
        {
            if (drinkscodes != null)
            {
                StringBuilder sb = new StringBuilder();
                char[] arr;
                arr = drinkscodes.ToCharArray();
                sb.Append(arr[0] == '1' ? "Чай " : "");
                sb.Append(arr[1] == '1' ? "Кофе " : "");
                sb.Append(arr[2] == '1' ? "Сок " : "");
                sb.Append(arr[3] == '1' ? "Вода " : "");
                return sb.ToString();
            }
            else
            {
                return " ";
            }
        }

        public static string GenerateRandom(int length)
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}
