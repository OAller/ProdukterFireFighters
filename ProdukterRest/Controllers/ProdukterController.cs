using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using ProdukterRest.DTO;

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

}
