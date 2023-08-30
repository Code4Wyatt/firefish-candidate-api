using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using firefish_candidate_api.Models;

namespace firefish_candidate_api.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly string connectionString;

        public SkillController(string connectionString)
        {
            this.connectionString = connectionString;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Skill>> GetAllSkills()
        { 
            var skills = new List<Skill>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Skill", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var skill = new Skill
                            {
                                Name = reader.GetString(1),
                                CreatedDate = reader.GetDateTime(2),
                                UpdatedDate = reader.GetDateTime(3)
                            };

                            skills.Add(skill);
                        }
                    }
                }
            }

            return Ok(skills);
        }
    }
}
