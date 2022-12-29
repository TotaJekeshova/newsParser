using Microsoft.AspNetCore.Mvc;
using Posts.API.Repository.Interfaces;

namespace Posts.API.Controllers;
[Route("api/posts")] 
public class PostsController : Controller
{
    private readonly IArticleRepository _articleRepository;
    
    public PostsController(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }
    
    [HttpGet("getValues/{start}/{end}")]
    public async Task<ActionResult> GetByDateTime(DateTime start, DateTime end)
    {
        if (start is DateTime && end is DateTime)
        {
            var articles = _articleRepository.GetByDateTime(start, end);
            if(articles.Count == 0) 
                return StatusCode(404, "Ничего не удалось найти");
        
            return Ok(articles);
        }

        return StatusCode(415, "Неверный запрос");
        
    }  
    
    [HttpGet ("/topten")]
    public async Task<ActionResult> GetTopTen()
    {
        var topWords = _articleRepository.GetTopTen();
        if(topWords == null)
            return NoContent();
        
        return Ok(topWords);
    }  
    
    [HttpGet ("/search")]
    public async Task<ActionResult> GetBySearch([FromQuery] string text)
    {
        var articles = _articleRepository.GetBySearch(text);
        if(articles.Count == 0) 
            return StatusCode(404, "Ничего не удалось найти");
        
        return Ok(articles);
    }  
}