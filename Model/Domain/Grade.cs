using System.ComponentModel.DataAnnotations;

namespace TestProject1.API.Model.Domain
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }
        public string GradeType { get; set; }
        public string GradeDescription { get; set; }
    }
}
