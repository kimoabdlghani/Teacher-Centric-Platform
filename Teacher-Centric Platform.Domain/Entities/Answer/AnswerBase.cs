using System;
using System.Collections.Generic;
using System.Text;
using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Domain.Entities.Exam;

namespace Teacher_Centric_Platform.Domain.Entities.Answer
{
    public abstract class AnswerBase : BaseAuditableEntity
    {
        public Guid QuestionId { get; protected set; }
        public Question Question { get; protected set; } = null!;

        public Guid? SelectedOptionId { get; protected set; }
        public QuestionOption? SelectedOption { get; protected set; }

        public string? TextAnswer { get; protected set; }

        public decimal? Score { get; protected set; } // unified
    }
}
