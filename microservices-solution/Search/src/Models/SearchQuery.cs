public class SearchQuery
{
    public string Query { get; set; }
    public Dictionary<string, string> Filters { get; set; }

    public SearchQuery()
    {
        Filters = new Dictionary<string, string>();
    }
}