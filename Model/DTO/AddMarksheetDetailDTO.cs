using System.ComponentModel.DataAnnotations;
using System.Net;

namespace TestProject1.API.Model.DTO
{
    public class AddMarksheetDetailDTO
    {
       // [Required(ErrorMessage = "Subject is required.")]
        public int UserId { get; set; }
        // [Required(ErrorMessage = "Subject is required.")]
        public List<MarkSheetList>? markSheetList { get; set; }
        public class MarkSheetList
        {
            public string? subject { get; set; }
            // public string SubjectDescription { get; set; }
            //  [Required(ErrorMessage = "Grade is required.")]
            public string? grade { get; set; }
            //public string GradeDescription { get; set; }
        }
    }
}
