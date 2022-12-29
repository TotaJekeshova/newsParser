
using TengriNewsParser;

GetPath();

void GetPath()
{
    Console.WriteLine("ссылка");
    var path = Console.ReadLine();
    var parser = new Parser();
    var article = parser.ParseArticle(path);

    var serverConnection = new ServerConnection();
    serverConnection.AddArticle(article);

    GetPath();
}