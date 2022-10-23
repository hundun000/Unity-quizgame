using System;
using TMPro;

namespace hundun.quizlib.exception {

    public class ModFormatException : QuizgameException
    {

        private const long serialVersionUID = 4915826309430319294L;

        public ModFormatException(String modContent, String targetType)
            : base("Mod中的 " + modContent + " 不是正确的" + targetType + "格式。", -1)
        {
            
        }

    }
}

