using System.ComponentModel.DataAnnotations;

namespace TestProject1.API.Model.Domain
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDescription { get; set; }
        public int UserId { get; set; }
    }
}
