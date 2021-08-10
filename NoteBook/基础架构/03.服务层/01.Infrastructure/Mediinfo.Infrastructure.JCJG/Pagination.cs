using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Infrastructure.JCJG
{
    public class Pagination
    {
        public Pagination()
        {
            Order = "asc";
            Limit = 10;
            Offset = 0;
            PageNumber = 1;
            PageSize = 10;
        }
        [JsonProperty(PropertyName = "search")]
        public string Search { get; set; }

        [JsonProperty(PropertyName = "sort")]
        public string Sort { get; set; }

        [DefaultValue("asc")]
        [JsonProperty(PropertyName = "order")]
        public string Order { get; set; }

        [DefaultValue(10)]
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        [DefaultValue(0)]
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [DefaultValue(1)]
        [JsonProperty(PropertyName = "pageNumber")]
        public int PageNumber { get; set; }

        [DefaultValue(10)]
        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }
    }
}
