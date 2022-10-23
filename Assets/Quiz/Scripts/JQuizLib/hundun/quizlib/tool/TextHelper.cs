using hundun.quizlib;
using hundun.quizlib.prototype;
using hundun.quizlib.prototype.match;
using hundun.quizlib.prototype.skill;
using hundun.quizlib.view.buff;
using hundun.quizlib.view.skill;
using hundun.quizlib.view.team;
using System;
using System.Collections.Generic;
using System.Text;

namespace hundun.quizlib.tool
{


}



/**
 * @author hundun
 * Created on 2021/07/17
 */
public class TextHelper {
    
    public static MatchStrategyType chineseToMatchStrategyType(String matchMode) {
        switch (matchMode) {
            case "无尽模式":
                return MatchStrategyType.ENDLESS;
            case "单人模式":
                return MatchStrategyType.PRE;
            case "双人模式":
                return MatchStrategyType.MAIN;
            default:
                return MatchStrategyType.NULL;
        }
    }
    
    public static String teamsNormalText(List<TeamRuntimeView> dtos) {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("队伍状态:\n");
        foreach (TeamRuntimeView dto in dtos) {
            stringBuilder.Append(dto.name).Append(" ");
            stringBuilder.Append("得分:").Append(dto.matchScore).Append(" ");
            stringBuilder.Append("生命:").Append(dto.health).Append(" ");
            if (dto.runtimeBuffs.Count > 0) {
                stringBuilder.Append("Buff:\n");
                foreach (BuffRuntimeView buffDTO in dto.runtimeBuffs) {
                    stringBuilder.Append(buffDTO.name).Append("x").Append(buffDTO.duration).Append(" ").Append(buffDTO.description).Append("\n");
                }
            }
            if (dto.roleRuntimeInfo != null) {
                stringBuilder.Append("英雄:").Append(dto.roleRuntimeInfo.name).Append(" 技能:\n");
                foreach (SkillSlotRuntimeView skillSlotRuntimeView in dto.roleRuntimeInfo.skillSlotRuntimeViews) {
                    stringBuilder.Append(skillSlotRuntimeView.name).Append(":").Append(skillSlotRuntimeView.remainUseTime).Append(" ");
                }
            }
            stringBuilder.Append("\n");
        }
        return stringBuilder.ToString();
    }
    
    public static String teamsDetailText(List<TeamRuntimeView> teamRuntimeDTOs, List<TeamPrototype> teamPrototypes) {
        StringBuilder stringBuilder = new StringBuilder();
        
        
        stringBuilder.Append("队伍详情:\n");
        for (int i = 0; i < teamRuntimeDTOs.Count; i++) {
            TeamRuntimeView teamRuntimeView = teamRuntimeDTOs.get(i);
            TeamPrototype teamPrototypeSimpleView = teamPrototypes.get(i);
            
            
            stringBuilder.Append(teamPrototypeSimpleView.name).Append(" 生命:").Append(teamRuntimeView.health).Append("\n");
            if (teamPrototypeSimpleView.pickTags.Count > 0) {
                stringBuilder.Append("Pick:");
                teamPrototypeSimpleView.pickTags.ForEach(tag => stringBuilder.Append(tag).Append("、"));
                stringBuilder.Length = (stringBuilder.Length - 1);
                stringBuilder.Append("\n");
            }
            if (teamPrototypeSimpleView.banTags.Count > 0) {
                stringBuilder.Append("Ban:");
                teamPrototypeSimpleView.banTags.ForEach(tag => stringBuilder.Append(tag).Append("、"));
                stringBuilder.Length = (stringBuilder.Length - 1);
                stringBuilder.Append("\n");
            }
            
            RolePrototype rolePrototype = teamPrototypeSimpleView.rolePrototype;
            if (rolePrototype != null) {
                stringBuilder.Append("英雄:").Append(rolePrototype.name).Append(" 介绍:").Append(rolePrototype.description).Append("\n");
                for (int j = 0; j < rolePrototype.skillSlotPrototypes.Count; j++) {
                    SkillSlotPrototype skillSlotPrototype = rolePrototype.skillSlotPrototypes.get(j);
                    stringBuilder.Append("技能").Append(j + 1).Append(":").Append(skillSlotPrototype.name).Append(" ");
                    stringBuilder.Append("次数:").Append(skillSlotPrototype.fullUseTime).Append(" ");
                    stringBuilder.Append("效果:").Append(skillSlotPrototype.fullUseTime).Append("\n");
                }
                stringBuilder.Append("\n");
            }
            
        }
        return stringBuilder.ToString();
    }
    
    public static int answerTextToInt(String text) {
        switch (text) {
            case "A":
            case "a":
                return 0;
            case "B":
            case "b":
                return 1;
            case "C":
            case "c":
                return 2;
            case "D":
            case "d":
                return 3;
            default:
                return -1;
        }
        
    }
    
    public static String intToAnswerText(int value) {
        switch (value) {
            case 0:
                return "A";
            case 1:
                return "B";
            case 2:
                return "C";
            case 3:
                return "D";
            default:
                return "?";
        }
        
    }
    
    
    
    
    

}
