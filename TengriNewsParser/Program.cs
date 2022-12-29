
using TengriNewsParser;

GetPath();

void GetPath()
{
    var path = "https://tengrinews.kz/kazakhstan_news/kazahstanskie-ukrainskie-kiberpolitseyskie-proveli-487341/";
    var parser = new Parser();
    var article = parser.ParseArticle(path);

    var serverConnection = new ServerConnection();
    serverConnection.AddArticle(article);

    GetPath();
}