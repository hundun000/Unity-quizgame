using hundun.quizlib.exception;
using hundun.quizlib.model.domain;
using hundun.quizlib.model.domain.buff;
using hundun.quizlib.model.domain.match.strategy;
using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using hundun.quizlib.service;
using hundun.quizlib.tool;
using hundun.quizlib.view.match;
using hundun.quizlib.view.team;

using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain.match
{
    public class BaseMatch {
        
        MatchState state;
        protected readonly String sessionId;

        public List<TeamRuntimeModel> teamRuntimeModels { get; private set; } = new List<TeamRuntimeModel>();
        
        private int currentTeamIndex;
        private QuestionModel currentQuestion;
        
        protected AnswerRecorder recorder = new AnswerRecorder();

        
        public AnswerResultEvent answerResultEvent;
        public SkillResultEvent skillResultEvent;
        public SwitchQuestionEvent switchQuestionEvent;
        public SwitchTeamEvent switchTeamEvent;
        public MatchFinishEvent finishEvent;
        public StartMatchEvent startMatchEvent;
        
        BaseMatchStrategy strategy;
        
        
        public BaseMatch(
                String sessionId,
                BaseMatchStrategy strategy
                ) {

            // TODO 测试阶段固定
            //this.id = UUID.randomUUID().toString();
            this.sessionId = sessionId;
            this.strategy = strategy;
            this.state = MatchState.WAIT_START;
            
            
            strategy.initMatch(this);
        }
        
        public void initTeams(List<TeamRuntimeModel> teamRuntimeModels) {
            this.teamRuntimeModels.Clear();
            this.teamRuntimeModels.addAll(teamRuntimeModels);
            foreach (TeamRuntimeModel teamRuntimeModel in teamRuntimeModels) {
                int currentHealth = strategy.calculateCurrentHealth();
                teamRuntimeModel.setHealth(currentHealth);
            }
            this.setCurrentTeam(-1);
        }
        

        public void start(List<String> questionIds) {
            checkStateException(state, ClientActionType.START_MATCH);
            
            setCurrentTeam(0);
    
            eventsClear();
            this.startMatchEvent = MatchEventFactory.getTypeStartMatch(this.teamRuntimeModels, questionIds);
            //events.add(checkSwitchQuestionEvent());
            this.state = MatchState.WAIT_GENERATE_QUESTION;
        }
        
        private void eventsClear() {
            this.answerResultEvent = null;
            this.skillResultEvent = null;
            this.switchQuestionEvent = null;
            this.switchTeamEvent = null;
            this.finishEvent = null;
            this.startMatchEvent = null;
        }

        
        
        
        
        
        public void teamUseSkill(String skillName) {
            checkStateException(state, ClientActionType.USE_SKILL);
            eventsClear();
            this.skillResultEvent = strategy.generalUseSkill(getCurrentTeam(), skillName);  
        }
        
        public void nextQustion() {
            checkStateException(state, ClientActionType.NEXT_QUESTION);
            eventsClear();
            this.switchQuestionEvent = strategy.checkSwitchQuestionEvent(); 
            this.state = MatchState.WAIT_ANSWER;
        }
        
        
        private void checkStateException(MatchState state, ClientActionType actionType) {
            if (!MatchStateUtils.check(state, actionType)) {
                throw new StateException(state.ToString(), actionType.ToString());
            }
        }
        
        
        
        
        

        public void teamAnswerTimeout() {
        checkStateException(state, ClientActionType.ANSWER);
        eventsClear();
        generalAnswer(QuestionModel.TIMEOUT_ANSWER_TEXT);
        }
        
        public void teamAnswer(String answerText) {
            checkStateException(state, ClientActionType.ANSWER);
            eventsClear();
            generalAnswer(answerText);
        }

        private void generalAnswer(String answer) {

            AnswerType answerType = getCurrentQuestion().calculateAnswerType(answer);
            // 记录回答
            getRecorder().addRecord(getCurrentTeam().getName(), answer, getCurrentQuestion().id, answerType);
            // 结算加分与生命值
            this.answerResultEvent = strategy.addScoreAndCountHealth(answerType);
            // 清空旧题
            setCurrentQuestion(null);
            // 结算buff
            updateBuffsDuration(answerType);
            // 判断比赛结束
            this.finishEvent = strategy.checkFinishEvent();
            if (finishEvent == null) {
                // 判断换队
                this.switchTeamEvent = strategy.checkSwitchTeamEvent();
            }
            
            this.state = MatchState.WAIT_GENERATE_QUESTION;
        }

        
        private void updateBuffsDuration(AnswerType answerType) {
            // 某些BuffEffect有特殊的修改Duration规则
            foreach (BuffRuntimeModel buff in getCurrentTeam().getBuffs()) {
                if (buff.buffStrategy != null) {
                    
                    if (answerType == AnswerType.CORRECT) {
                        // 本身每回合所有buff会减1层。为了使答对后最终加1层，则在此处加2层
                        buff.addDuration(2);
                    } else {
                        buff.clearDuration();
                    }
                }

            }
            
            // 所有buff减少一层，然后清理已经没有层数的buff
            foreach (BuffRuntimeModel buff in getCurrentTeam().getBuffs())
            {
                buff.minusOneDurationAndCheckMaxDuration();
                if (buff.duration <= 0)
                {
                    getCurrentTeam().getBuffs().Remove(buff);
                }
            }
 
        }

        
        
        // ===== normal getter =====
        
        public String getSessionId() {
            return sessionId;
        }

        public List<TeamRuntimeModel> getTeams() {
            return teamRuntimeModels;
        }

        public TeamRuntimeModel getCurrentTeam() {
            if (currentTeamIndex >= 0) {
                return teamRuntimeModels.get(currentTeamIndex);
            } else {
                return null;
            }
        }

        public QuestionModel getCurrentQuestion() {
            return currentQuestion;
        }


        
        public int getCurrentTeamIndex() {
            return currentTeamIndex;
        }

        
        // ===== quick getter =====
        
    //    /**
    //     * 直接取出需要的类型
    //     * @param type
    //     * @return
    //     */
    //    public MatchEvent getEventByType(EventType type) {
    //        for (MatchEvent event : events) {
    //            if (event.getType() == type) {
    //                return event;
    //            }
    //        }
    //        return null;
    //    }
        
    //    public bool containsEventByType(EventType type) {
    //        for (MatchEvent event : events) {
    //            if (event.getType() == type) {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }

        public MatchSituationView toMatchSituationView() {
            MatchSituationView dto = new MatchSituationView();
            dto.id = (this.getSessionId());
            dto.question = (this.getCurrentQuestion() != null ? this.getCurrentQuestion().toQuestionDTO() : null);
            dto.currentTeamIndex = (this.getCurrentTeamIndex());
            dto.currentTeamRuntimeInfo = (getCurrentTeam() != null ? getCurrentTeam().toTeamRuntimeInfoDTO() : null);
            dto.answerResultEvent = (answerResultEvent);
            dto.skillResultEvent = (skillResultEvent);
            dto.startMatchEvent = (startMatchEvent);
            dto.switchTeamEvent = (switchTeamEvent);
            dto.switchQuestionEvent = (switchQuestionEvent);
            dto.finishEvent = (finishEvent);
            List<TeamRuntimeView> teamRuntimeInfos = new List<TeamRuntimeView>(this.teamRuntimeModels.Count);
            this.teamRuntimeModels.ForEach(team => teamRuntimeInfos.Add(team.toTeamRuntimeInfoDTO()));
            dto.teamRuntimeInfos = (teamRuntimeInfos);
            dto.state = (state);
            dto.actionAdvices = (MatchStateUtils.getValidClientActions(state));
            return dto;
        }

        public void setCurrentQuestion(QuestionModel currentQuestion) {
            this.currentQuestion = currentQuestion;
        }


        public void setCurrentTeam(int currentTeamIndex) {
            this.currentTeamIndex = currentTeamIndex;
            
        }

        public AnswerRecorder getRecorder() {
            return recorder;
        }
        
    }
}


