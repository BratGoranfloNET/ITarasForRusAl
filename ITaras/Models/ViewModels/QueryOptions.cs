using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITaras.Models.ViewModels
{
    public class QueryOptions
    {
        public QueryOptions()
        {
            CurrentPage = 1;
            PageSize = 15;
            SortField = "favColors";          
            SortOrder = ViewModels.SortOrder.ASC.ToString();
        }

        [JsonProperty(PropertyName = "sortField")]
        public string SortField { get; set; }
        

        [JsonProperty(PropertyName = "sortOrder")]
        public string SortOrder { get; set; }


        [JsonProperty(PropertyName = "currentPage")]
        public int CurrentPage { get; set; }


        [JsonProperty(PropertyName = "totalPages")]
        public int TotalPages { get; set; }


        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }


        [JsonProperty(PropertyName = "favColors")]
        public string FavColors { get; set; }


        [JsonProperty(PropertyName = "favDrinks")]
        public string FavDrinks { get; set; }
        

        [JsonIgnore]
        public string Sort
        {
            get
            {
                return string.Format("{0} {1}",
                    SortField, SortOrder.ToString());
            }
        }
    }
}
