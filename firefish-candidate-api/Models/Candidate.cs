namespace firefish_candidate_api.Models
{
    public class Candidate
    {

        public int ID { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address1 { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }

        public string PostCode { get; set; }

        public string PhoneHome { get; set; }

        public string PhoneMobile { get; set; }

        public string PhoneWork { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
