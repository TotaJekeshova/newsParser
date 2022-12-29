using Posts.API.Model;

namespace Posts.API.Repository.Interfaces;

public interface IArticleRepository
{
    List<Article> GetByDateTime(DateTime startDate, DateTime endDate);
    List<string> GetTopTen ();
    List<Article> GetBySearch (string request);
}