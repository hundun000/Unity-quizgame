using System;
using TMPro;

namespace hundun.quizlib.exception 
{
    public class StateException : QuizgameException
    {

        private const long serialVersionUID = 2799953340358311729L;

        public StateException(String stateName, String operationName)
            : base("state = " + stateName + " 时不应 " + operationName, -1)
        {
            

        }

    }

}

