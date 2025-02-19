using Microsoft.AspNetCore.Mvc;
using ProdukterRest.DTO;

[Route("api/[controller]")]
[ApiController]
public class ProdukterController : ControllerBase
{
    private readonly ProdukterRepo _repo = new ProdukterRepo();

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserDTO userDto)
    {
        if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
        {
            // 400 Bad Request
            return BadRequest("Email og password er påkrævet.");
        }

        try
        {
            _repo.CreateUser(userDto.Email, userDto.Password);
            // 201 Created – selvom vi ikke returnerer en resource URL her
            return StatusCode(201, "Bruger oprettet succesfuldt.");
        }
        catch (Exception ex)
        {
            // 500 Internal Server Error
            return StatusCode(500, "Fejl ved oprettelse af bruger: " + ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserDTO userDto)
    {
        if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
        {
            // 400 Bad Request
            return BadRequest("Email og password er påkrævet.");
        }

        try
        {
            bool isValidUser = _repo.ValidateUser(userDto.Email, userDto.Password);

            if (isValidUser)
            {
                // 200 OK
                return Ok("Login succesfuldt.");
            }
            else
            {
                // 401 Unauthorized
                return Unauthorized("Forkert email eller adgangskode.");
            }
        }
        catch (Exception ex)
        {
            // 500 Internal Server Error
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
            // 400 Bad Request
            return BadRequest("Alle felter skal udfyldes.");
        }

        try
        {
            bool isUpdated = _repo.ChangeUserPassword(changePasswordDto.Email, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (isUpdated)
            {
                // 200 OK
                return Ok("Adgangskode opdateret.");
            }
            else
            {
                // 401 Unauthorized – hvis for eksempel det gamle kodeord er forkert
                return Unauthorized("Forkert email eller adgangskode.");
            }
        }
        catch (Exception ex)
        {
            // 500 Internal Server Error
            return StatusCode(500, "Der opstod en fejl: " + ex.Message);
        }
    }
}
