using System;
using System.Collections.Generic;

namespace hundun.quizlib.exception
{
    public class QuizgameException : Exception 
    {
        
        private const long serialVersionUID = 6572553799656923229L;
        
        private readonly int code;
        
        public QuizgameException(int code) {
            this.code = code;
        }
        
        public QuizgameException(String msg, int code) 
            : base(msg)
        {
            this.code = code;
        }

        
        public int getRetcode() {
            return code;
        }

        
        public int getStatus() {
            return 400;
        }

        
        public String getPayload() {
            return null;
        }
        
        
        public String getMessage() {
            return base.Message;
        }

    }
}

