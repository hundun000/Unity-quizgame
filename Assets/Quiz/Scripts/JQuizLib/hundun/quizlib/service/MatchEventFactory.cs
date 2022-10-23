using hundun.quizlib.model.domain;
using hundun.quizlib.prototype;
using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using System;
using System.Collections.Generic;

namespace hundun.quizlib.service 
{ 

}



/**
 * @author hundun
 * Created on 2020/05/23
 */
public class MatchEventFactory {
    
    public static bool isTypeInCollection(ICollection<MatchEvent> events, EventType type) {
        foreach (MatchEvent @event in events) {
            if (@event.type == type) {
                return true;
            }
        }
        return false;
    }
    
    public static StartMatchEvent getTypeStartMatch(List<TeamRuntimeModel> teamRuntimeModels, List<String> questionIds){
        StartMatchEvent @event = new StartMatchEvent();
        @event.type = (EventType.START_MATCH);
        List<TeamPrototype> teamPrototypes = new List<TeamPrototype>();
        foreach (TeamRuntimeModel teamRuntimeModel in teamRuntimeModels) {
            teamPrototypes.Add(teamRuntimeModel.prototype);
        }
        @event.teamPrototypes = (teamPrototypes);
        @event.questionIds = (questionIds);
        return @event;
    }
    
    public static SwitchTeamEvent getTypeSwitchTeam(TeamRuntimeModel lastTeam, TeamRuntimeModel currentTeam) {
        SwitchTeamEvent @event = new SwitchTeamEvent();
        @event.type = (EventType.SWITCH_TEAM);
        @event.fromTeamName = (lastTeam.getName());
        @event.toTeamName = (currentTeam.getName());
        return @event;
    }
    
    public static SwitchQuestionEvent getTypeSwitchQuestion(int time) {
        SwitchQuestionEvent @event = new SwitchQuestionEvent();
        @event.type = (EventType.SWITCH_QUESTION);
        @event.time = (time);
        return @event;
    }
    
    
    public static MatchFinishEvent getTypeFinish(Dictionary<String, int> scores) {
//        ObjectNode data = mapper.createObjectNode();
//        data.put("scores", scores.toString());
        MatchFinishEvent @event = new MatchFinishEvent();
        @event.type = (EventType.FINISH);
        return @event;
    }
    
    public const String KEY_SKILL_NAME = "skill_name";

    public static SkillResultEvent getTypeSkillSuccess(String teamName, String roleName, SkillSlotRuntimeModel skillSlotRuntimeModel) {
        SkillResultEvent @event = new SkillResultEvent();
        @event.type = (EventType.SKILL_SUCCESS);
        @event.teamName = (teamName);
        @event.roleName = (roleName);
        @event.skillName = (skillSlotRuntimeModel.prototype.name);
        @event.skillDesc = (skillSlotRuntimeModel.prototype.description);
        @event.skillRemainTime = (skillSlotRuntimeModel.remainUseTime);
        @event.args = (skillSlotRuntimeModel.prototype.eventArgs);
        
        return @event;
    }
    
    public static SkillResultEvent getTypeSkillUseOut(String teamName, String roleName, SkillSlotRuntimeModel skillSlotRuntimeModel) {
        SkillResultEvent @event = new SkillResultEvent();
        @event.type = (EventType.SKILL_USE_OUT);
        @event.teamName = (teamName);
        @event.roleName = (roleName);
        @event.skillName = (skillSlotRuntimeModel.prototype.name);
        @event.skillDesc = (skillSlotRuntimeModel.prototype.description);
        @event.skillRemainTime = (skillSlotRuntimeModel.remainUseTime);
        @event.args = (skillSlotRuntimeModel.prototype.eventArgs);
        
        return @event;
    }
    
    public static AnswerResultEvent getTypeAnswerResult(AnswerType answerType, int addScore, String addScoreTeamName) {
        AnswerResultEvent @event = new AnswerResultEvent();
        @event.type = (EventType.ANSWER_RESULT);
        @event.addScore = (addScore);
        @event.addScore = (addScore);
        @event.addScoreTeamName = (addScoreTeamName);
        @event.result = (answerType);
        return @event;
    }

}
