using Microsoft.AspNetCore.Mvc;
using Posts.API.Repository.Interfaces;

namespace Posts.API.Controllers;
[Route("api/")] 
public class PostsController : Controller
{
    private readonly IArticleRepository _articleRepository;
    
    public PostsController(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }
    
    [HttpGet("posts")]
    public async Task<ActionResult> GetByDateTime(DateTime from, DateTime to)
    {
        if (from is DateTime && to is DateTime)
        {
            var articles = _articleRepository.GetByDateTime(from, to);
            if(articles.Count == 0) 
                return StatusCode(404, "Ничего не удалось найти");
        
            return Ok(articles);
        }

        return StatusCode(415, "Неверный запрос");
        
    }  
    
    [HttpGet ("topten")]
    public async Task<ActionResult> GetTopTen()
    {
        var topWords = _articleRepository.GetTopTen();
        if(topWords == null)
            return NoContent();
        
        return Ok(topWords);
    }  
    
    [HttpGet ("search")]
    public async Task<ActionResult> GetBySearch([FromQuery] string text)
    {
        var articles = _articleRepository.GetBySearch(text);
        if(articles.Count == 0) 
            return StatusCode(404, "Ничего не удалось найти");
        
        return Ok(articles);
    }  
}