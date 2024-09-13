using BooksWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        // GET: api/<LoansController>
        [HttpGet]
        public List<Loan> Get()
        {
            var db = new DBConnection();
            var loans = db.GetAllLoans();

            return loans;
        }

        // GET api/<LoansController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var db = new DBConnection();
            var loan = db.GetLoanById(id);

            if (loan == null)
            {
                return NotFound();
            }

            return Ok(loan);
        }

        // POST api/<LoansController>
        [HttpPost]
        public IActionResult Post([FromBody] Loan loan)
        {
            var db = new DBConnection();

            if (!db.Bookexists(loan.BookId))
            {
                return NotFound($"Book with Id {loan.BookId} does not exist.");
            }
            
            db.SaveLoan(loan);

            return Ok("Loan added successfully.");
        }

        // PUT api/<LoansController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Loan loan)
        {
            var db = new DBConnection();

            if (!db.Bookexists(loan.BookId))
            {
                return NotFound($"Book with Id {loan.BookId} does not exist.");
            }

            db.UpdateLoan(id, loan);

            return Ok("Loan updated successfully.");
        }

        // DELETE api/<LoansController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var db = new DBConnection();
            db.DeleteBookById(id);
        }
    }
}
