using System.Data;
using Microsoft.Data.SqlClient;
using TengriNewsParser.Model;

namespace TengriNewsParser;

public class ServerConnection
{
    private const string ConnectionString =
        "Server=articlesdb;Database=NewsArticles;User Id=sa;Password=reallyStrongPwd123;TrustServerCertificate=True;MultipleActiveResultSets=true";
    public void AddArticle(Article article)
    {
        SqlConnection connection = new SqlConnection(ConnectionString);
        connection.Open();
        SqlCommand sqlCmd = new SqlCommand("ArticleAdd", connection);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.AddWithValue("Title", article.Title);
        sqlCmd.Parameters.AddWithValue("CreatedDate", article.CreatedDate);
        sqlCmd.Parameters.AddWithValue("ArticleText", article.ArticleText);
        sqlCmd.ExecuteNonQuery();
        connection.Close();
    }
    
    
}