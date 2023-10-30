﻿using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn.Request
{
    public class NewLearnModel
    {

        [Required]
        public string? LearnName { get; set; }

        [Required]
        public string? Content { get; set; }

        public string? Code { get; set; }

    }
}
