using Microsoft.AspNetCore.Mvc;
using ProdukterLib;            // For at få adgang til ProdukterRepo
using ProdukterLib.Classes;   // For at få adgang til User-klassen
using System.Collections.Generic;

namespace ProdukterRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdukterController : ControllerBase
    {
        // Typisk enten DI (Dependency Injection) eller en simpel instans til demo
        private readonly ProdukterRepo _repo;

        public ProdukterController()
        {
            // Simpel demo: ny instans hver gang controlleren oprettes
            // I et rigtigt projekt vil man ofte bruge dependency injection i Startup/Program
            _repo = new ProdukterRepo();
        }

        // GET: api/Produkter
        // Henter alle brugere
        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            // Returnerer en liste af Users
            return Ok(_repo.GetAll());
        }

        // GET api/Produkter/5
        // Henter én bruger ud fra ID
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            try
            {
                User user = _repo.GetById(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/Produkter
        // Opretter en ny bruger
        [HttpPost]
        public ActionResult<User> Post([FromBody] User newUser)
        {
            if (newUser == null)
                return BadRequest("User object cannot be null");

            User createdUser = _repo.Add(newUser);

            // Created(...) returnerer en 201 (Created) status med et 'Location'-header
            return Created($"api/Produkter/{createdUser.Id}", createdUser);
        }

        // PUT api/Produkter/5
        // Opdaterer en eksisterende bruger
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null)
                return BadRequest("User object cannot be null");

            try
            {
                User user = _repo.Update(id, updatedUser);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/Produkter/5
        // Sletter en bruger baseret på ID
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            try
            {
                User user = _repo.Delete(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
