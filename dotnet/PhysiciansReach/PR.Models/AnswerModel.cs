using System;

namespace PR.Models
{
    public class AnswerModel
    {
        public int AnswerId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
