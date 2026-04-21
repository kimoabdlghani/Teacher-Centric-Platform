using Teacher_Centric_Platform.Domain.Common;

namespace Teacher_Centric_Platform.Domain.Entities.Exam
{

        public class QuestionOption : BaseAuditableEntity
        {
            

            public Guid QuestionId { get; private set; }

            public Question Question { get; private set; } = null!;

            public string Text { get; private set; }

            private bool IsCorrect { get; set; }

            private QuestionOption() { } // EF

            public QuestionOption(string text, bool isCorrect)
            {
                SetText(text);
                IsCorrect = isCorrect;
            }

            private void SetText(string text)
            {
                if (string.IsNullOrWhiteSpace(text))
                    throw new ArgumentException("Option text cannot be empty");

                Text = text;
            }

            
            public bool CheckIfCorrect() => IsCorrect;
        }
}
