using System.Data;
using Microsoft.Data.SqlClient;
using Posts.API.Model;
using Posts.API.Repository.Interfaces;

namespace Posts.API.Repository;

public class ArticleRepository : IArticleRepository
{
    private readonly IConfiguration _configuration;

    public ArticleRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public List<Article> GetByDateTime(DateTime startDate, DateTime endDate)
    {
        List<Article> articles;
        string queryString = "SELECT id AS Id, title AS Title, createdDate AS CreatedDate, articleText AS ArticleText FROM [dbo].[details] WHERE createdDate > @start AND createdDate < @end";
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlCommand sqlCmd = new SqlCommand(queryString, connection);
            sqlCmd.Parameters.Add("@start", SqlDbType.DateTime).Value = startDate;
            sqlCmd.Parameters.Add("@end", SqlDbType.DateTime).Value = endDate;
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            articles = GetList<Article>(dataReader);
        }
        return articles;
    }

    public List<string> GetTopTen()
    {
        var articles = GetAll();
        var articleTexts = articles.Select(a => a.ArticleText).ToList();
        
        List<string> allWords = new List<string>();
        
        foreach (var text in articleTexts)
        {
            var words = text.Split(" ");
            foreach (var word in words)
            {
                allWords.Add(word);
            }
        }

        var topWords = (from word in allWords
            where word != null
            where word.Length > 2
            where word != ""
            group word by word into grp
            orderby grp.Count() descending
            select grp.Key).Take(10).ToList();

        return topWords;
    }

    public List<Article> GetBySearch(string request)
    {
        List<Article> articles;
        string queryString = "SELECT id AS Id, title AS Title, createdDate AS CreatedDate, articleText AS ArticleText FROM [dbo].[details] WHERE ' ' + articleText + ' ' like '%[^]' + @text + '[^]%'";
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlCommand sqlCmd = new SqlCommand(queryString, connection);
            sqlCmd.Parameters.Add("@text", SqlDbType.NVarChar).Value = request;
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            articles = GetList<Article>(dataReader);
            connection.Close();
        }
        return articles;
    }
    
    private List<Article> GetAll()
    {
        List<Article> articles;
        string queryString =
            "SELECT id AS Id, title AS Title, createdDate AS CreatedDate, articleText AS ArticleText FROM [dbo].[details]";
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            connection.Open();
            SqlCommand sqlCmd = new SqlCommand(queryString, connection);
            SqlDataReader dataReader = sqlCmd.ExecuteReader();
            articles = GetList<Article>(dataReader);
        }
        return articles;
    }
    
    private List<T> GetList<T>(IDataReader reader)
    {
        List<T> list = new List<T>();
        while (reader.Read())
        {
            var type = typeof(T);
            T obj = (T) Activator.CreateInstance(type);
            foreach (var prop in type.GetProperties())
            {
                var propType = prop.PropertyType;
                prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType));
            }
            list.Add(obj);
        }
        return list;
    }

}