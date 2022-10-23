using hundun.quizlib.exception;
using hundun.quizlib.model.domain;
using hundun.quizlib.model.domain.buff;
using hundun.quizlib.model.domain.match;
using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using hundun.quizlib.prototype.skill;
using hundun.quizlib.service;

using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain.match.strategy
{
    public abstract class BaseMatchStrategy {

        protected BaseMatch parent;
        
        protected QuestionService questionService;
        protected TeamService teamService;
        protected RoleSkillService roleSkillService;
        protected BuffService buffService;
        
        protected HealthType healthType;
        
        protected const int DEFAULT_CORRECT_ANSWER_SCORE = 1;
        
        
        public BaseMatchStrategy(
                QuestionService questionService,
                TeamService teamService,
                RoleSkillService roleSkillService,
                BuffService buffService,
                HealthType healthType
                ) {
            this.questionService = questionService;
            this.teamService = teamService;
            this.roleSkillService = roleSkillService;
            this.buffService = buffService;
            this.healthType = healthType;
        }
        
        /**
        * 为刚刚的答题加分。
        * 可实现为：固定加分；连续答对comb加分...
        * 
        * 为刚刚的答题结算剩余健康值
        * 可实现为：累计答n题死亡；连续答错n题死亡；累计答错n题死亡...
        * @param answerType 
        */
        public AnswerResultEvent addScoreAndCountHealth(AnswerType answerType) {
            
            int score = 0;
            if (answerType == AnswerType.CORRECT) {
                score = DEFAULT_CORRECT_ANSWER_SCORE;
                score += calculateScoreAdditionByBuffs(answerType, score);
            }
            parent.getCurrentTeam().addScore(score);
            
            int currentHealth = calculateCurrentHealth();
            
            parent.getCurrentTeam().setHealth(currentHealth);
            
            
            return MatchEventFactory.getTypeAnswerResult(answerType, score, parent.getCurrentTeam().getName());
        }

        /**
        * 计算所有buff引起的加分offset
        * @param baseScore
        * @return
        */
        protected int calculateScoreAdditionByBuffs(AnswerType answerType, int baseScoreAddition) {
            int modifiedScoreAddition = 0;
            foreach (BuffRuntimeModel buff in parent.getCurrentTeam().getBuffs()) {
                modifiedScoreAddition = buff.buffStrategy.modifyScore(baseScoreAddition, buff.duration);
            }
            return modifiedScoreAddition;
        }
        
        
        /**
        * 判断是否切换队伍。
        * 可实现为：答完一题切换；答错才切换...
        * @return
        */
        public abstract SwitchTeamEvent checkSwitchTeamEvent();

        
        /**
        * 换题时可指定不同时间
        * @return
        * @throws QuizgameException 
        */
        public SwitchQuestionEvent checkSwitchQuestionEvent() {
            bool removeToDirty = this.healthType != HealthType.ENDLESS;
            parent.setCurrentQuestion(questionService.getNewQuestionForTeam(parent.getSessionId(), parent.getCurrentTeam(), removeToDirty));
            return MatchEventFactory.getTypeSwitchQuestion(15);
        }
        
        public MatchFinishEvent checkFinishEvent() {
            bool anyDie = false;
            foreach (TeamRuntimeModel teamRuntimeModel in parent.getTeams()) {
                if (!teamRuntimeModel.isAlive()) {
                    anyDie = true;
                    break;
                }
            }
            if (anyDie) {

                Dictionary<String, int> scores = new Dictionary<String, int>(parent.getTeams().Count);
                parent.getTeams().ForEach(item => scores.put(item.getName(), item.getMatchScore()));
                return MatchEventFactory.getTypeFinish(scores);
            } else {
                return null;
            }
        }
        
        public void switchToNextTeam() {
            int checkingIndex = parent.getCurrentTeamIndex();
            int tryTimes = 0;
            
            do {
                tryTimes++;
                if (tryTimes > parent.getTeams().Count) {
                    throw new Exception("试图在所有队伍死亡情况下换队");
                }
                
                checkingIndex++;
                if (checkingIndex == parent.getTeams().Count) {
                    checkingIndex = 0;
                }
                
            } while (!parent.getTeams().get(checkingIndex).isAlive());
            
            parent.setCurrentTeam(checkingIndex);
            
        }
        
        public abstract int calculateCurrentHealth();
        
        
        
        public SkillResultEvent generalUseSkill(TeamRuntimeModel team, String skillName)
        {
            SkillResultEvent newEvents; 
            
            SkillSlotRuntimeModel skillSlotRuntimeModel = parent.getCurrentTeam().getSkillSlotRuntime(skillName);
            
            bool success = skillSlotRuntimeModel.useOnce(skillName);
            if (success) {
                newEvents = MatchEventFactory.getTypeSkillSuccess(team.getName(), team.getRoleName(), skillSlotRuntimeModel);
                
                if (skillSlotRuntimeModel.prototype.backendEffects != null) {
                    foreach (AddBuffSkillEffect skillEffect in skillSlotRuntimeModel.prototype.backendEffects) {
                        AddBuffSkillEffect addBuffSkillEffect = skillEffect;
                        BuffRuntimeModel buff = buffService.generateRunTimeBuff(addBuffSkillEffect.buffName, addBuffSkillEffect.duration);
                        parent.getCurrentTeam().addBuff(buff);
                    }
                }
            } else {
                newEvents = MatchEventFactory.getTypeSkillUseOut(team.getName(), team.getRoleName(), skillSlotRuntimeModel);
            }
            
            return newEvents;
        }
        


        public void initMatch(BaseMatch baseMatch) {
            this.parent = baseMatch;
        }

        public abstract int calculateSkillStartUseTime(int fullUseTime);
        
        
    }
}
