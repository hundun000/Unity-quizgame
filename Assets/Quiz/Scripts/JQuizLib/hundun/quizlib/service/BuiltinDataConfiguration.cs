using System;
using System.Collections.Generic;

using hundun.quizlib.context;
using hundun.quizlib.exception;
using hundun.quizlib.prototype;
using hundun.quizlib.prototype.buff;
using hundun.quizlib.prototype.skill;

namespace hundun.quizlib.service
{
    public class BuiltinDataConfiguration : IQuizComponent
    {


        public static String DEMO_LIST_TEAM_NAME_0 = "游客1";
        public static String DEMO_LIST_TEAM_NAME_1 = "游客2";


        public enum BuiltinSkill
        {
            SKILL_SKIP,
            SKILL_5050,
            SKILL_HELP_1,
            SKILL_HELP_2,
            SKILL_DEMO_BUFF
            
        }

        private TeamService teamService;
        private RoleSkillService roleSkillService;
        private BuffService buffService;


        public void postConstruct(QuizComponentContext context) 
        {
            this.teamService = context.teamService;
            this.roleSkillService = context.roleSkillService;
            this.buffService = context.buffService;

            buffService.registerBuffPrototype(new BuffPrototype(
                    "连击中",
                    "答题正确时，额外获得与“连击中”层数相同的分数，且“连击中”层数加1（最大为3层）；否则，失去所有“连击中”层数。",
                    3,
                    BuffStrategyType.SCORE_MODIFY
                    ));
        

            List<SkillSlotPrototype> prototypes = JavaFeatureExtension.ArraysAsList(
                    new SkillSlotPrototype(BuiltinSkill.SKILL_SKIP.ToString(), "跳过", "结束本题。本题不计入得分、答对数、答错数。", null, null, 2),
                    new SkillSlotPrototype(BuiltinSkill.SKILL_5050.ToString(), "5050", "揭示2个错误选项。", JavaFeatureExtension.ArraysAsList("2"), null, 2),
                    new SkillSlotPrototype(BuiltinSkill.SKILL_HELP_1.ToString(), "求助现场", "答题时间增加30秒，并且本题期间可与现场观众交流。", JavaFeatureExtension.ArraysAsList("30"), null, 2),
                    new SkillSlotPrototype(BuiltinSkill.SKILL_HELP_2.ToString(), "求助专家", "答题时间增加30秒，并且本题期间可与专家团交流。", JavaFeatureExtension.ArraysAsList("30"), null, 2),
                    new SkillSlotPrototype(BuiltinSkill.SKILL_DEMO_BUFF.ToString(), "连击之力", "为自己增加一层“连击中”。", null, JavaFeatureExtension.ArraysAsList(new AddBuffSkillEffect("连击中", 1)), 2)
                    );
            prototypes.ForEach(it => roleSkillService.registerSkill(it));
        
        }

        public void registerForGuest()
        {

            teamService.registerTeam(BuiltinDataConfiguration.DEMO_LIST_TEAM_NAME_0, new List<String>(0), new List<String>(0), null);
            teamService.registerTeam(BuiltinDataConfiguration.DEMO_LIST_TEAM_NAME_1, new List<String>(0), new List<String>(0), null);
        
        }


    
    }
}





