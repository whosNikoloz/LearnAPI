﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearnAPI.Model.Learn.Test
{
    public class TestAnswerModel
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        public string Option { get; set; }

        public bool IsCorrect { get; set; }

        // Foreign Key
        public int TestId { get; set; }

        // Navigation Property
        [JsonIgnore]
        public virtual TestModel? Test { get; set; }
    }
}
