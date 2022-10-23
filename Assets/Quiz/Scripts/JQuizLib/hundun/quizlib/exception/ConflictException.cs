using System;

namespace hundun.quizlib.exception
{
    public class ConflictException : QuizgameException
    {

        /**
         * 
         */
        private const long serialVersionUID = 4076058696749505391L;



        public ConflictException(String type, String key) : 
            base(type + ":" + key + "已存在。", 1)
        {

        }

    }
}


