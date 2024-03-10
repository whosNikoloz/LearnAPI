using LearnAPI.Model.Learn.Test;
using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn.Request
{
    public class NewLessonModel
    {
        public string? LessonName_ka { get; set; }
        public string? LessonName_en { get; set; }

    }
}
