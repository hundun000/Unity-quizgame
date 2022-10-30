using hundun.quizlib.context;
using hundun.quizlib;
using hundun.quizlib.model;
using hundun.quizlib.model.domain;
using System.Collections.Generic;
using System;
using hundun.quizlib.exception;
using Unity.VisualScripting;

namespace hundun.quizlib.service 
{
    public class SessionService : IQuizComponent
    {

        private static int currentId;

        QuestionService questionService;

        public void postConstruct(QuizComponentContext context)
        {
            this.questionService = context.questionService;
        }

        Dictionary<String, SessionDataPackage> dataPackages = new Dictionary<string, SessionDataPackage>();
        private Random shuffleRandom = new Random();

        public SessionDataPackage createSession(String questionPackageName)
        {

            SessionDataPackage sessionDataPackage = new SessionDataPackage();


            List<QuestionModel> questions = questionService.getQuestions(questionPackageName);
            List<String> questionIds = new List<string>(questions.Count);
            HashSet<String> tags = new HashSet<String>();
            foreach (QuestionModel question in questions) {
                questionIds.Add(question.id);
                tags.AddRange(question.tags);
            }
            questionIds.Shuffle(shuffleRandom);

            sessionDataPackage.sessionId = ((currentId++).ToString());
            sessionDataPackage.questionIds = (questionIds);
            sessionDataPackage.dirtyQuestionIds = (new List<QuestionModel>());
            sessionDataPackage.tags = (tags);
        
            dataPackages.put(sessionDataPackage.sessionId, sessionDataPackage);
        
            return sessionDataPackage;
        }

    public SessionDataPackage getSessionDataPackage(String sessionId)
    {
        SessionDataPackage sessionDataPackage = dataPackages.get(sessionId);
        if (sessionDataPackage == null)
        {
            throw new NotFoundException("sessionDataPackage by sessionId", sessionId);
        }
        return sessionDataPackage;
        }



    }
}






