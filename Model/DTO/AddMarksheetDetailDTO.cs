using System.ComponentModel.DataAnnotations;
using System.Net;

namespace TestProject1.API.Model.DTO
{
    public class AddMarksheetDetailDTO
    {
       // [Required(ErrorMessage = "Subject is required.")]
        public int UserId { get; set; }
        // [Required(ErrorMessage = "Subject is required.")]
        public List<MarkSheetListNew> markSheetListNew { get; set; }
        public class MarkSheetListNew
        {
            public int subjectId { get; set; }
            public string subject { get; set; }
            public int gradeId { get; set; }
            public string grade { get; set; }
        }
    }
}
