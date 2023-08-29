using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using firefish_candidate_api.Models;

namespace firefish_candidate_api.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly string connectionString;

        public CandidateController(string connectionString)
        {
            this.connectionString = connectionString;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Candidate>> GetAllCandidates()
        {
            var candidates = new List<Candidate>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Candidate", connection)) 
                {
                    using (var reader = command.ExecuteReader()) 
                    {
                        while (reader.Read())
                        {
                            var candidate = new Candidate
                            {
                                FirstName = reader.GetString(1),
                                Surname = reader.GetString(2),
                                DateOfBirth = reader.GetDateTime(3),
                                Address1 = reader.GetString(4),
                                Town = reader.GetString(5),
                                Country = reader.GetString(6),
                                PostCode = reader.GetString(7),
                                PhoneHome = reader.GetString(8),
                                PhoneMobile = reader.GetString(9),
                                PhoneWork = reader.GetString(10),
                                CreatedDate = reader.GetDateTime(11),
                                UpdatedDate = reader.GetDateTime(12)
                            };
                            candidates.Add(candidate);
                        }
                    }
                }
            }
            return Ok(candidates);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCandidate(int id, [FromBody] Candidate updatedCandidate)
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Candidate SET FirstName = @FirstName, Surname = @Surname, DateOfBirth = @DateOfBirth, Address1 = @Address1, Town = @Town, Country = @Country, PostCode = @PostCode, PhoneHome = @PhoneHome, PhoneMobile = @PhoneMobile, PhoneWork = @PhoneWork, UpdatedDate = @UpdatedDate WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("@FirstName", updatedCandidate.FirstName);
                    command.Parameters.AddWithValue("@Surname", updatedCandidate.Surname);
                    command.Parameters.AddWithValue("@DateOfBirth", updatedCandidate.DateOfBirth);
                    command.Parameters.AddWithValue("@Address1", updatedCandidate.Address1);
                    command.Parameters.AddWithValue("@Town", updatedCandidate.Town);
                    command.Parameters.AddWithValue("@Country", updatedCandidate.Country);
                    command.Parameters.AddWithValue("@PostCode", updatedCandidate.PostCode);
                    command.Parameters.AddWithValue("@PhoneHome", updatedCandidate.PhoneHome);
                    command.Parameters.AddWithValue("@PhoneMobile", updatedCandidate.PhoneMobile);
                    command.Parameters.AddWithValue("@PhoneWork", updatedCandidate.PhoneWork);
                    command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);

                    int rowsffected = command.ExecuteNonQuery();

                    if (rowsffected > 0)
                    {
                        return Ok("Candidate updated succesfully!");
                    }
                    else
                    {
                        return NotFound("Candidate with that ID does not exist.");
                    }

                }
            }
        }

        [HttpPost]
        public ActionResult CreateCandidate([FromBody] Candidate newCandidate) 
        {
            using (var connection = new SqlConnection(connectionString)) 
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Candidate (FirstName, Surname, DateOfBirth, Address1, Town, Country, PostCode, PhoneHome, PhoneMobile, PhoneWork, CreatedDate, UpdatedDate) VALUES (@FirstName, @Surname, @DateOfBirth, @Address1, @Town, @Country, @PostCode, @PhoneHome, @PhoneMobile, @PhoneWork, @CreatedDate, @UpdatedDate)", connection))
                {
              
                    command.Parameters.AddWithValue("@FirstName", newCandidate.FirstName);
                    command.Parameters.AddWithValue("@Surname", newCandidate.Surname);
                    command.Parameters.AddWithValue("@DateOfBirth", newCandidate.DateOfBirth);
                    command.Parameters.AddWithValue("@Address1", newCandidate.Address1);
                    command.Parameters.AddWithValue("@Town", newCandidate.Town);
                    command.Parameters.AddWithValue("@Country", newCandidate.Country);
                    command.Parameters.AddWithValue("@PostCode", newCandidate.PostCode);
                    command.Parameters.AddWithValue("@PhoneHome", newCandidate.PhoneHome);
                    command.Parameters.AddWithValue("@PhoneMobile", newCandidate.PhoneMobile);
                    command.Parameters.AddWithValue("@PhoneWork", newCandidate.PhoneWork);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0) 
                    {
                        return Ok("Candidate created successfully! :)");
                    }
                    else
                    {
                        return BadRequest("Failed to create the candidate, check all fields are completed.");
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCandidate(int id) 
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Candidate WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Candidate deleted successfully!");
                    }
                    else
                    {
                        return NotFound("Candidate with that ID does not exist.");
                    }
                }
            }
        }
    }
}
