using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using firefish_candidate_api.Models;

namespace firefish_candidate_api.Controllers
{
    [Route("api/candidateSkills")]
    [ApiController]
    public class CandidateSkillController : ControllerBase
    {
        private readonly string connectionString;

        public CandidateSkillController(string connectionString)
        {
            this.connectionString = connectionString;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CandidateSkill>> GetAllCandidateSkills()
        { 
            var candidateSkills = new List<CandidateSkill>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM CandidateSkill", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var candidateSkill = new CandidateSkill
                            {
                                CandidateID = reader.GetInt32(1),
                                CreatedDate = reader.GetDateTime(2),
                                UpdatedDate = reader.GetDateTime(3),
                                SkillId = reader.GetInt32(4),
                            };

                            candidateSkills.Add(candidateSkill);
                        }
                    }
                }
            }

            return Ok(candidateSkills);
        }

        [HttpPost]
        public ActionResult CreateCandidateSkill([FromBody] CandidateSkill newCandidateSkill)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO CandidateSkill (CandidateID, CreatedDate, UpdatedDate, SkillId) VALUES (@CandidateID, @CreatedDate, @UpdatedDate, @SkillId)", connection))
                {

                    command.Parameters.AddWithValue("@CandidateID", newCandidateSkill.CandidateID);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@SkillId", newCandidateSkill.SkillId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Candidate skill added successfully! :)");
                    }
                    else
                    {
                        return BadRequest("Failed to add the skill for the candidate, check all fields are completed.");
                    }
                }
            }
        }

        [HttpDelete("{candidateId}/{skillId}")]
        public ActionResult DeleteCandidateSkill([FromRoute] int candidateId, [FromRoute] int skillId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM CandidateSkill WHERE CandidateID = @CandidateID AND SkillId = @SkillId", connection))
                {
                    command.Parameters.AddWithValue("@CandidateID", candidateId);
                    command.Parameters.AddWithValue("@SkillId", skillId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Candidate skill deleted successfully!");
                    }
                    else
                    {
                        return NotFound("Skill with that ID does not exist.");
                    }
                }
            }
        }
    }
}
