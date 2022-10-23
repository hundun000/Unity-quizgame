using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using hundun.quizlib.service;

using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain.match.strategy
{
    public class PreStrategy : BaseMatchStrategy {
        

        public PreStrategy(
                QuestionService questionService, 
                TeamService teamService, 
                RoleSkillService roleSkillService,
                BuffService buffService
                )
            : base(questionService, teamService, roleSkillService, buffService,
                    HealthType.SUM
                    ) {
        }
        

        
        //@Override
        override public SwitchTeamEvent checkSwitchTeamEvent() {
            /*
            * 一定不换队
            */
            return null;
        }



        //@Override
        override public int calculateCurrentHealth() {
            /*
            * 累计答n题后死亡
            */
            int fullHealth = 5;
            int currentHealth = fullHealth - parent.getRecorder().countSum(parent.getCurrentTeam().getName(), fullHealth);
            
            return currentHealth;
        }

        //@Override
        override public int calculateSkillStartUseTime(int fullUseTime) {
            return 0;
        }

    }
}


