using System;

namespace hundun.quizlib.exception
{
    public class TeamDeadException : QuizgameException 
    {
        
        /**
        * 
        */
        private const long serialVersionUID = 4825087489852106495L;

        public TeamDeadException(String teamName)
        : base(teamName + "已经死了", 1)
        {
            
        }

    }
}
