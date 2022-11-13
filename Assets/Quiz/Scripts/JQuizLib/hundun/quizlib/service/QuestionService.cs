using hundun.quizlib.context;
using hundun.quizlib.exception;
using hundun.quizlib.model;
using hundun.quizlib.model.domain;
using hundun.quizlib.prototype.question;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

namespace hundun.quizlib.service
{
    public class QuestionService : IQuizComponent
    {

        SessionService sessionService;
        QuestionLoaderService questionLoaderService;

        public void postConstruct(QuizComponentContext context)
        {
            this.sessionService = context.sessionService;
            this.questionLoaderService = context.questionLoaderService;
        }

        private Random hitRandom = new Random(1);
        private Random insertQuestionRandom = new Random(1);


        //private HashSet<String> tags = new HashSet<>();

        Dictionary<String, List<QuestionModel>> questionPackages = new Dictionary<string, List<QuestionModel>>();
        Dictionary<String, QuestionModel> questionPool = new Dictionary<string, QuestionModel>();


        public QuestionModel getNewQuestionForTeam(String sessionId, TeamRuntimeModel teamRuntimeModel, bool removeToDirty)
        {
            SessionDataPackage sessionDataPackage = sessionService.getSessionDataPackage(sessionId);
  
            QuestionModel question;
            bool hitPick = hitRandom.NextDouble() < teamRuntimeModel.hitPickRate;
            if (hitPick) 
            {
                question = getFirstPickQuestionIndex(sessionId, teamRuntimeModel);
                teamRuntimeModel.resetHitPickRate();
                //log.debug("FirstPickQuestionIndex");
            } else {
                question = getFirstNotBanQuestionIndex(sessionId, teamRuntimeModel);
                teamRuntimeModel.increaseHitPickRate();
                //log.debug("FirstNotBanQuestionIndex");
            }
            if (question == null)
            {
                question = getFirstQuestionIgnorePickBan(sessionId, teamRuntimeModel);
            }
            //log.info("questionDTO = {}", question.toQuestionDTO());

            sessionDataPackage.questionIds.Remove(question.id);
            if (removeToDirty)
            {
                sessionDataPackage.dirtyQuestionIds.Add(question);
            }
            else
            {
                int insertIndex = Math.Min(sessionDataPackage.questionIds.Count, sessionDataPackage.questionIds.Count / 2 + insertQuestionRandom.Next(sessionDataPackage.questionIds.Count / 2));
                sessionDataPackage.questionIds.Insert(insertIndex, question.id);
            }

            return question;
        }
    
    
        private QuestionModel getFirstQuestionIgnorePickBan(String sessionId, TeamRuntimeModel teamRuntimeModel) 
        {
            SessionDataPackage sessionDataPackage = sessionService.getSessionDataPackage(sessionId);
            QuestionModel question = null;
            int i = 0;  
            if (sessionDataPackage.questionIds.isEmpty()) {
                throw new QuizgameException("题库已经空了", -1);
            }
            String questionId = sessionDataPackage.questionIds.get(i);
            question = questionPool.get(questionId);

            return question;
        }


        private QuestionModel getFirstPickQuestionIndex(String sessionId, TeamRuntimeModel teamRuntimeModel)
        {
            SessionDataPackage sessionDataPackage = sessionService.getSessionDataPackage(sessionId);
            QuestionModel question = null;
                int i = 0;    
                while (question == null && i < sessionDataPackage.questionIds.Count) {
                String questionId = sessionDataPackage.questionIds.get(i);
                question = questionPool.get(questionId);

                if (!teamRuntimeModel.isPickAndNotBan(question.tags))
                {
                    question = null;
                    i++;
                    continue;
                }
                if (!sessionDataPackage.allowImageResource && question.resource.type == ResourceType.IMAGE)
                {
                    question = null;
                    i++;
                    continue;
                }
                if (!sessionDataPackage.allowVoiceResource && question.resource.type == ResourceType.VOICE)
                {
                    question = null;
                    i++;
                    continue;
                }
            }
                return question;
        }

        private QuestionModel getFirstNotBanQuestionIndex(String sessionId, TeamRuntimeModel teamRuntimeModel)
        { 
            SessionDataPackage sessionDataPackage = sessionService.getSessionDataPackage(sessionId);
            QuestionModel question = null;
                int i = 0;    
                while (question == null && i < sessionDataPackage.questionIds.Count) {
                String questionId = sessionDataPackage.questionIds.get(i);
                question = questionPool.get(questionId);

                if (!teamRuntimeModel.isNotBan(question.tags))
                {
                    question = null;
                    i++;
                    continue;
                }
                if (!sessionDataPackage.allowImageResource && question.resource.type == ResourceType.IMAGE)
                {
                    question = null;
                    i++;
                    continue;
                }
                if (!sessionDataPackage.allowVoiceResource && question.resource.type == ResourceType.VOICE)
                {
                    question = null;
                    i++;
                    continue;
                }
            }
                return question;
        }



        public List<QuestionModel> getQuestions(String questionPackageName)
        {
            if (!questionPackages.containsKey(questionPackageName)) {
                List<QuestionModel> questions = questionLoaderService.loadAllQuestions(questionPackageName);
                questionPackages.put(questionPackageName, questions);
                questions.ForEach(item => questionPool.put(item.id, item));
            }
            return questionPackages.get(questionPackageName);
        }


        public HashSet<String> getTags(String questionPackageName)
        {
            List<QuestionModel> questions = getQuestions(questionPackageName);
            HashSet<String> tags = new HashSet<String>();
            questions.ForEach(question => tags.AddRange(question.tags));
            return tags;
        }
    
//    public void addTag(String tag) {
//        tags.add(tag);
//    }
//    
//    public HashSet<String> getTags() {
//        return tags;
//    }
//    
//    public bool tagExsist(String tag) {
//        return tags.contains(tag);
//    }

    }
}




