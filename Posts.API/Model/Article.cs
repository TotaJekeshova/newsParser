using System.Text.Json.Serialization;

namespace Posts.API.Model;

public class Article
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("articleText")]
    public string ArticleText { get; set; }
}