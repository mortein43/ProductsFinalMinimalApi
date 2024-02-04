using Microsoft.AspNetCore.Mvc;

namespace ProductsFinalMinimalApi;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Кавамашина", Price = 15000 },
            new Product { Id = 2, Name = "Велосипед", Price = 20000 },
            new Product { Id = 3, Name = "Товар № 3", Price = 7500 }
        };

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(products);
    }

    [HttpGet("{productId}")]
    public IActionResult GetProductById(int productId)
    {
        var product = products.Find(p => p.Id == productId);

        if (product == null)
            return NotFound();

        var productWithId = new { Id = product.Id, Name = product.Name, Price = product.Price };

        return Ok(productWithId);
    }

    [HttpPost]
    public ActionResult<Product> AddProduct(Product product)
    {
        product.Id = products.Count + 1;
        products.Add(product);
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, Product updatedProduct)
    {
        var existingProduct = products.Find(p => p.Id == id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var existingProduct = products.Find(p => p.Id == id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        products.Remove(existingProduct);
        return NoContent();
    }

    [HttpPost("/api/login")]
    public IActionResult Login([FromForm] string login, [FromForm] string password)
    {
        if (login == "admin" && password == "admin")
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30)
            };

            Response.Cookies.Append("authenticated", "true", cookieOptions);
        }
        else
        {
            Response.Cookies.Append("authenticated", "false", new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(30)
            });
        }

        // Завжди редіректимо на /index незалежно від результату аутентифікації
        return Redirect("/index");
    }

    [HttpGet("/api/check-auth")]
    public IActionResult CheckAuthentication()
    {
        var isAuthenticated = Request.Cookies.TryGetValue("authenticated", out string authenticatedValue) && authenticatedValue == "true";
        return Ok(isAuthenticated);
    }

}
