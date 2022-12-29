using System.ComponentModel.DataAnnotations;

namespace TengriNewsParser.Model;

public class Article
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ArticleText { get; set; }

    public override string ToString()
    {
        return $"Название: {Title}\n" +
               $"Дата создания: {CreatedDate}\n" +
               $"Текcт: {ArticleText}";
    }
}