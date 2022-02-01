using System.Collections.Generic;

namespace DeptAssignment.Business.Models.Imdb
{
    public class SearchData
    {
        public string SearchType { get; set; }
        public string Expression { get; set; }
        public List<SearchResult> Results { get; set; }
        public string ErrorMessage { get; set; }
    }
}
