using Newtonsoft.Json;
using System.Collections.Generic;

namespace ITaras.Models.ViewModels
{ 
    public class ResultList<T>
    {
        public ResultList(IEnumerable<T> results, QueryOptions queryOptions)
        {
            Results = results;
            QueryOptions = queryOptions;
        }

        public ResultList()
        {
            
        }

        [JsonProperty(PropertyName = "queryOptions")]
        public QueryOptions QueryOptions { get; set; }

        [JsonProperty(PropertyName = "results")]
        public IEnumerable<T> Results { get; set; }       
    }
}
