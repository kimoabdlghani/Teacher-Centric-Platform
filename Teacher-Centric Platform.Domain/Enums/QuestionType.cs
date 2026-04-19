using System;
using System.Collections.Generic;
using System.Text;

namespace Teacher_Centric_Platform.Domain.Enums
{
    public enum QuestionType
    {
        Mcq = 1,      // Multiple Choice - Auto-graded
        Written = 2,  // Essay/Text - Manual grading by Teacher
        TrueFalse = 3 // Auto-graded
    }
}
