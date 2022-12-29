using System.Net;
using System.Text.RegularExpressions;
using TengriNewsParser.Model;

namespace TengriNewsParser;

public class Parser
{
     public Article ParseArticle(string? path)
    {
        WebClient client = new WebClient(); 
        var htmlString = client.DownloadString(path);
        var title = ParseTitle(htmlString);
        var date = ParseDate(htmlString);
        var articleText = ParseText(htmlString);

        var article = new Article()
        {
            Title = title,
            CreatedDate = date,
            ArticleText = articleText
        };

        return article;
    }

    private string ParseTitle(string htmlString)
    {
        string title = null;
        Regex titleRegex = new Regex(@"<title>(.*?)</title>"); 
        MatchCollection matches = titleRegex.Matches(htmlString); 
        if (matches.Count > 0)
        {
            foreach (Match match in matches)
            { 
                string newsTitle = Regex.Replace(match.ToString(), @"<title>", "");
                newsTitle = Regex.Replace(newsTitle, @"</title>", "");
                title = newsTitle.Split(":")[0];
            }
        }
        else 
        { 
            Console.WriteLine("Совпадений не найдено");
        }

        return title ?? throw new InvalidOperationException();
    }

    private DateTime ParseDate(string htmlString)
    {
        DateTime parsedDate = default;
        Regex dateTimeRegex = new Regex(@"<time class=""tn-visible@t"">(.*?)</time>");
        MatchCollection dateMatches = dateTimeRegex.Matches(htmlString);
        if (dateMatches.Count > 0) 
        { 
            foreach (Match match in dateMatches) 
            { 
                string newsdate = Regex.Replace(match.ToString(), @"<time class=""tn-visible@t"">", "");
                newsdate = Regex.Replace(newsdate, @"</time>", "");
                var dateTime = newsdate.Split(",");
                var date = dateTime[0];
                if(date.Equals("Сегодня")) 
                { 
                    date = DateTime.Today.ToString("d");
                }
                if(date.Equals("Вчера")) 
                { 
                    date = DateTime.Today.AddDays(-1).ToString("d");
                }
                else
                {
                    date = GetDate(dateTime[0]);
                }
                var time = dateTime[1];
                parsedDate = Convert.ToDateTime(date + time);
            }
                            
        }
        else
        {
            Console.WriteLine("Совпадений не найдено");
        }

        return parsedDate;
    }

    private string ParseText(string htmlString)
    {
        string newsText = null;
        Regex textRegex = new Regex(@"<p>(.*?)</p>");
        MatchCollection textMatches = textRegex.Matches(htmlString);
        if (textMatches.Count > 0)
        {
            foreach (Match match in textMatches)
            { 
                var paragraph = Regex.Replace(match.ToString(), 
                    @"<p>", "");
                paragraph = Regex.Replace(paragraph, @"<strong>", "");
                paragraph = Regex.Replace(paragraph, @"</strong>", "");
                paragraph = Regex.Replace(paragraph, @"</p>", "");
                paragraph = Regex.Replace(paragraph, @"<a href=(.*?)>", " ");
                paragraph = Regex.Replace(paragraph, @"</a>", "");
                paragraph = Regex.Replace(paragraph, @"<iframe (.*?)>", "");
                paragraph = Regex.Replace(paragraph, @"</iframe>", "");
                paragraph = Regex.Replace(paragraph, @"<img src=(.*?)>", " ");
                paragraph = Regex.Replace(paragraph, @"<span>", " ");
                paragraph = Regex.Replace(paragraph, @"</span>", " ");
                
                newsText += paragraph;
            }
                
        }
        else
        { 
            Console.WriteLine("Совпадений не найдено");
        }

        return newsText ?? throw new InvalidOperationException();
    }

    private string GetDate(string datePart)
    {
        string date = null;
        var fullDate = datePart.Split(" ");
        if (fullDate[1].Contains("январ")) 
            date= new DateTime(Int32.Parse(fullDate[2]), 1, Int32.Parse(fullDate[0])).ToString("d");
      
        if (fullDate[1].Contains("феврал")) 
            date= new DateTime(Int32.Parse(fullDate[2]), 2, Int32.Parse(fullDate[0])).ToString("d");
       
        if (fullDate[1].Contains("март")) 
            date= new DateTime(Int32.Parse(fullDate[2]), 3, Int32.Parse(fullDate[0])).ToString("d");
      
        if (fullDate[1].Contains("апрел")) 
            date= new DateTime(Int32.Parse(fullDate[2]), 4, Int32.Parse(fullDate[0])).ToString("d");
        
        if (fullDate[1].Contains("ма")) 
            date= new DateTime(Int32.Parse(fullDate[2]), 5, Int32.Parse(fullDate[0])).ToString("d");
        
        if (fullDate[1].Contains("июн"))
            date= new DateTime(Int32.Parse(fullDate[2]), 6, Int32.Parse(fullDate[0])).ToString("d");
        
        if (fullDate[1].Contains("июл"))
            date= new DateTime(Int32.Parse(fullDate[2]), 7, Int32.Parse(fullDate[0])).ToString("d");
        
        if (fullDate[1].Contains("авгус")) 
            date= new DateTime(Int32.Parse(fullDate[2]), 8, Int32.Parse(fullDate[0])).ToString("d");
        
        if (fullDate[1].Contains("сентябр")) 
            date= new DateTime(Int32.Parse(fullDate[2]), 9, Int32.Parse(fullDate[0])).ToString("d");
        
        if (fullDate[1].Contains("октяб")) 
            date= new DateTime(Int32.Parse(fullDate[2]), 10, Int32.Parse(fullDate[0])).ToString("d");
       
        if (fullDate[1].Contains("ноябр")) 
            date= new DateTime(Int32.Parse(fullDate[2]), 11, Int32.Parse(fullDate[0])).ToString("d");
        
        if(fullDate[1].Contains("декаб")) 
            date= new DateTime(Int32.Parse(fullDate[2]), 12, Int32.Parse(fullDate[0])).ToString("d");
        
        return date;
    }
    
}