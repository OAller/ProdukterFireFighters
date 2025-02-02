using Microsoft.AspNetCore.Mvc;
using ProdukterLib;            // For at få adgang til ProdukterRepo
using ProdukterLib.Classes;   // For at få adgang til User-klassen
using System.Collections.Generic;
using ProdukterRest.DTO;
using System.Net.NetworkInformation;
using System;
using System.Numerics;

namespace ProdukterRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ProdukterRepo _users;

        // Dependency Injection
        public UsersController(ProdukterRepo repo)
        {
            _users = repo;
        }
        
        // GET: api/Users
        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            return Ok(_users.GetAll());
        }
    

        // GET api/Produkter/5
        // Henter én bruger ud fra ID
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            try
            {
                User user = _users.GetById(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public ActionResult<User> AddUser([FromBody] UserDTO ObjektDTO)
        {
            try
            {
                User objekt = new User { FirstName = ObjektDTO.FirstName, LastName = ObjektDTO.LastName, Email = ObjektDTO.Email, Phone = ObjektDTO.Phone, Password = ObjektDTO.Password }; //konverterer ParticipantsDTO til Participant
                User result = _users.Add(objekt);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result); //201 http statuskode
            }
            catch (ArgumentException EX)
            {
                return BadRequest(EX.Message); //400 http statuskode
            }
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
                User user = _users.Update(id, updatedUser);
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
                User user = _users.Delete(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
