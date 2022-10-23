using System;

namespace hundun.quizlib.exception {
    public class NotFoundException : QuizgameException
    {


        private const long serialVersionUID = -237975911570660889L;


        public NotFoundException(String type, String key)
            : base(type + ":" + key + "未找到。", 404)
        {


        }

    }
}


