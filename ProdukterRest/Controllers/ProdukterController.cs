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
            return BadRequest("Email og password er påkrævet.");
        }

        try
        {
            _repo.CreateUser(userDto.Email, userDto.Password);
            return Ok("Bruger oprettet succesfuldt.");
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

        try
        {
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

        try
        {
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
        catch (Exception ex)
        {
            return StatusCode(500, "Der opstod en fejl: " + ex.Message);
        }
    }


}
