﻿using System;
using System.ComponentModel.DataAnnotations;

namespace firefish_candidate_api.Models

{
    public class CandidateSkill
    {

        public int CandidateID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
        public int SkillId { get; set; }
    }
}
