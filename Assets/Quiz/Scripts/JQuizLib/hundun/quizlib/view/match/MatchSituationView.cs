using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using hundun.quizlib.prototype.@event;  
using hundun.quizlib.prototype.match;      
using hundun.quizlib.view.question;
using hundun.quizlib.view.team;

namespace hundun.quizlib.view.match
{
    public class MatchSituationView {
        [JsonProperty]
        public String id {get; set;}
        [JsonProperty]
        public QuestionView question {get; set;}
        [JsonProperty]
        public TeamRuntimeView currentTeamRuntimeInfo {get; set;}
        [JsonProperty]
        public int currentTeamIndex {get; set;}
        [JsonProperty]
        public List<TeamRuntimeView> teamRuntimeInfos {get; set;}
        [JsonProperty]
        public MatchState state {get; set;}
        [JsonProperty]
        public HashSet<ClientActionType> actionAdvices {get; set;} 
        
        public AnswerResultEvent answerResultEvent {get; set;}
        [JsonProperty]
        public SkillResultEvent skillResultEvent {get; set;}
        [JsonProperty]
        public SwitchQuestionEvent switchQuestionEvent {get; set;}
        [JsonProperty]
        public SwitchTeamEvent switchTeamEvent {get; set;}
        [JsonProperty]
        public MatchFinishEvent finishEvent {get; set;}
        [JsonProperty]
        public StartMatchEvent startMatchEvent;
    }
}

