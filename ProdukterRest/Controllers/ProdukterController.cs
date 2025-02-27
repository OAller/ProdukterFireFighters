using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using ProdukterRest.DTO;
using Newtonsoft.Json;

[Route("api/[controller]")]
[ApiController]
public class ProdukterController : ControllerBase
{
    private readonly ProdukterRepo _repo = new ProdukterRepo();

    private bool IsValidEmail(string email)
    {
        // Traditionel email validering med regex
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    private void ValidatePassword(string password)
    {
        if (password.Length < 11)
        {
            throw new ArgumentException("Password skal være mindst 12 tegn langt.");
        }
        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            throw new ArgumentException("Password skal indeholde mindst ét stort bogstav.");
        }
        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            throw new ArgumentException("Password skal indeholde mindst ét lille bogstav.");
        }
        if (password.Contains(" "))
        {
            throw new ArgumentException("Password må ikke indeholde mellemrum.");
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserDTO userDto)
    {
        if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
        {
            return BadRequest("Email og password er påkrævet.");
        }

        if (!IsValidEmail(userDto.Email))
        {
            return BadRequest("Email skal indeholde '@'.");
        }

        try
        {
            ValidatePassword(userDto.Password);
            _repo.CreateUser(userDto.Email, userDto.Password);
            return StatusCode(201, "Bruger oprettet succesfuldt.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Fejl ved oprettelse af bruger: " + ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserDTO userDto)
    {
        if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
        {
            return BadRequest("Email og password er påkrævet.");
        }

        if (!IsValidEmail(userDto.Email))
        {
            return BadRequest("Email skal indeholde '@'.");
        }

        try
        {
            ValidatePassword(userDto.Password);
            bool isValidUser = _repo.ValidateUser(userDto.Email, userDto.Password);

            if (isValidUser)
            {
                return Ok("Login succesfuldt.");
            }
            else
            {
                return Unauthorized("Forkert email eller adgangskode.");
            }
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Der opstod en fejl: " + ex.Message);
        }
    }

    [HttpPost("change-password")]
    public IActionResult ChangePassword([FromBody] ChangePasswordDTO changePasswordDto)
    {
        if (string.IsNullOrEmpty(changePasswordDto.Email) ||
            string.IsNullOrEmpty(changePasswordDto.OldPassword) ||
            string.IsNullOrEmpty(changePasswordDto.NewPassword))
        {
            return BadRequest("Alle felter skal udfyldes.");
        }

        if (!IsValidEmail(changePasswordDto.Email))
        {
            return BadRequest("Email skal indeholde '@'.");
        }

        try
        {
            ValidatePassword(changePasswordDto.NewPassword);
            bool isUpdated = _repo.ChangeUserPassword(changePasswordDto.Email, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (isUpdated)
            {
                return Ok("Adgangskode opdateret.");
            }
            else
            {
                return Unauthorized("Forkert email eller adgangskode.");
            }
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Der opstod en fejl: " + ex.Message);
        }
    }
    [HttpGet("products")]
    public IActionResult GetAllProducts()
    {
        try
        {
            var products = _repo.GetAllProducts();

            if (products == null || products.Count == 0)
            {
                return NotFound("Ingen produkter fundet.");
            }

            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Fejl ved hentning af produkter: " + ex.Message);
        }
    }

    [Route("api/produkter")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProdukterRepo _produktRepo;

        public ProductController(ProdukterRepo produktRepo)
        {
            _produktRepo = produktRepo;
        }



        // 🛒 Tilføj produkt til indkøbskurven (bruger session)
        [HttpPost("cart/add/{productId}")]
        public IActionResult AddToCart(int productId)
        {
            // Hent alle produkter fra databasen
            var products = _produktRepo.GetAllProducts();
            var product = products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound(new { message = "Produktet blev ikke fundet" });
            }

            // Hent kurven fra session
            List<Product> cart = GetCartFromSession();

            // Tilføj produktet til kurven
            cart.Add(product);

            // Gem kurven i session igen
            SaveCartToSession(cart);

            return Ok(new { message = "Produkt tilføjet til kurven", cart });
        }

        // 🛒 Hent indkøbskurven
        [HttpGet("cart")]
        public IActionResult GetCart()
        {
            var cart = GetCartFromSession();
            return Ok(cart);
        }

        // 🛒 Fjern produkt fra kurven
        [HttpDelete("cart/remove/{productId}")]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCartFromSession();
            var product = cart.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound(new { message = "Produktet findes ikke i kurven" });
            }

            // Fjern produktet
            cart.Remove(product);
            SaveCartToSession(cart);

            return Ok(new { message = "Produkt fjernet fra kurven", cart });
        }

        // 🛒 Hjælpefunktion til at hente kurven fra session
        private List<Product> GetCartFromSession()
        {
            var sessionData = HttpContext.Session.GetString("Cart");
            return sessionData == null ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(sessionData);
        }

        // 🛒 Hjælpefunktion til at gemme kurven i session
        private void SaveCartToSession(List<Product> cart)
        {
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
    }
}
