using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.match
{
    public enum AnswerType {
        NULL,
        CORRECT,
        WRONG,
        TIMEOUOT_WRONG,
        SKIPPED
    }
    
    public class AnswerTypeCompanion {

        public static AnswerType frombool(bool isCorrect, bool isSkip, bool isTimeout) {
            if (isTimeout) {
                return AnswerType.TIMEOUOT_WRONG;
            } else if (isSkip) {
                return AnswerType.SKIPPED;
            } else {
                return isCorrect ? AnswerType.CORRECT : AnswerType.WRONG;
            }
        }
    }
}

