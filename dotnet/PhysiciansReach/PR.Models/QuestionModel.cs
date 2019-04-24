using System;
using System.Collections.Generic;

namespace PR.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }

        public string Text { get; set; }

        public string Key { get; set; }

        public List<AnswerModel> Answers { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
