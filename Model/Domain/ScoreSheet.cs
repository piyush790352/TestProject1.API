namespace TestProject1.API.Model.Domain
{
    public class ScoreSheet
    {
       public int UserId { get; set; }

        public List<MarkSheetList> markSheetList { get; set; }
        public class MarkSheetList
        {
            public int subjectId { get; set; }
            public int gradeId { get; set; }
        }
    }
}
