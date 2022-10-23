using hundun.quizlib.prototype.match;
using hundun.quizlib.prototype.question;
using hundun.quizlib.tool;
using hundun.quizlib.view.question;


using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain
{
    public class QuestionModel {
        public const String SKIP_ANSWER_TEXT = "skip";
        public const String TIMEOUT_ANSWER_TEXT = "timeout";

        public readonly String id;
        public readonly String stem;
        public readonly String[] options;
        public readonly int answer;
        public readonly Resource resource;
        public readonly HashSet<String> tags;
        
        //public const String TIMEOUT_ANSWER_TEXT = "timeout";
        
        public QuestionModel(String stem, String optionA, String optionB, String optionC, String optionD, String answerText, String resourceText, HashSet<String> tags) {
            this.stem = stem;
            this.options = new String[4];
            this.options[0] = optionA;
            this.options[1] = optionB;
            this.options[2] = optionC;
            this.options[3] = optionD;
            this.answer = TextHelper.answerTextToInt(answerText);
            this.tags = tags;
            this.resource = new Resource(resourceText);
            
    //        StringBuilder builder = new StringBuilder();
    //        tags.forEach(e -> builder.append(e.charAt(0)));
    //        builder.append("-").append(stem.substring(0, Math.min(6, stem.length())));
            this.id = (this.GetHashCode().ToString());
        }
        
        
        
        
        public bool isCorrectOrSkipped(String answerText) {
            if (answerText == null) {
                return false;
            }
            return TextHelper.answerTextToInt(answerText) == this.answer;
        }
        
        
        public String getAnswerChar() {
            switch (answer) {
            case 0:
                return "A";
            case 1:
                return "B";
            case 2:
                return "C";
            case 3:
                return "D";
            default:
                throw new Exception("default int answer cannot to String.");
            }
        }


        public AnswerType calculateAnswerType(String answerText) {
            if (answerText.Equals(SKIP_ANSWER_TEXT)) {
                return AnswerType.SKIPPED;
            } else if (answerText == (TIMEOUT_ANSWER_TEXT)) {
                return AnswerType.TIMEOUOT_WRONG;
            } else {
                // 正常回答A、B、C、D
                if (TextHelper.answerTextToInt(answerText) == this.answer) {
                    return AnswerType.CORRECT;
                } else {
                    return AnswerType.WRONG;
                }
            }
        }


        public QuestionView toQuestionDTO() {
            QuestionView dto = new QuestionView();
            dto.id = (this.id);
            dto.answer = (this.answer);
            dto.stem = (this.stem);
            dto.options = (JavaFeatureExtension.ArraysAsList(this.options));
            dto.resource = (this.resource);
            dto.tags = (this.tags);
            return dto;
        }
        
        
    }
}

