using System;

namespace PR.Data.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }

        public string Text { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
