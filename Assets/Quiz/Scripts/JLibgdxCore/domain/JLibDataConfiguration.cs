using hundun.quizlib.context;
using hundun.quizlib.prototype;
using hundun.quizlib.service;
using static hundun.quizlib.service.BuiltinDataConfiguration;
using System.Collections.Generic;
using System;
using hundun.quizlib;

public class LibDataConfiguration
{

    public static String ZACA_TEAM_NAME_1 = "砍口垒同好组";
    public static String ZACA_TEAM_NAME_2 = "方舟同好组";

    public static String ZACA_ROLE_NAME = "ZACA娘";

    private TeamService teamService;
    private RoleSkillService roleSkillService;
    private BuffService buffService;


    public LibDataConfiguration(QuizComponentContext context)
    {
        this.teamService = context.teamService;
        this.roleSkillService = context.roleSkillService;
        this.buffService = context.buffService;
    }

    public void registerForSaveData(List<TeamPrototype> teamPrototypes)
    {

        roleSkillService.registerRole(new RolePrototype(
                ZACA_ROLE_NAME,
                "主人公。",
                JavaFeatureExtension.ArraysAsList(
                        roleSkillService.getSkillSlotPrototype(BuiltinSkill.SKILL_5050.ToString()),
                        roleSkillService.getSkillSlotPrototype(BuiltinSkill.SKILL_SKIP.ToString()),
                        roleSkillService.getSkillSlotPrototype(BuiltinSkill.SKILL_HELP_1.ToString()),
                        roleSkillService.getSkillSlotPrototype(BuiltinSkill.SKILL_HELP_2.ToString())
                )
        )); 
        
        if (teamPrototypes == null) {
            teamService.registerTeam(ZACA_TEAM_NAME_1,
                    new List<String>(),
                    new List<String>(),
                    ZACA_ROLE_NAME
                    );
            teamService.registerTeam(ZACA_TEAM_NAME_2,
                    new List<String>(),
                    new List<String>(),
                    ZACA_ROLE_NAME
                    );
        } 
        else
        {
            foreach (TeamPrototype teamPrototype in teamPrototypes)
            {
                String roleName = teamPrototype.rolePrototype != null ? teamPrototype.rolePrototype.name : null;
                teamService.registerTeam(teamPrototype.name,
                        teamPrototype.pickTags,
                        teamPrototype.banTags,
                        roleName
                        );
            }
        }
        
    }
    
}