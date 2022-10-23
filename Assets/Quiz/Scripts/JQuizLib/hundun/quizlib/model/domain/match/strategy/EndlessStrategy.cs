using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using hundun.quizlib.service;

using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain.match.strategy
{
    public class EndlessStrategy : BaseMatchStrategy {

        public EndlessStrategy(QuestionService questionService, TeamService teamService, RoleSkillService roleSkillService,
                BuffService buffService) 
            :base(
                    questionService, 
                    teamService, 
                    roleSkillService, 
                    buffService, 
                    HealthType.ENDLESS
                    ) {
        }

        //@Override
        override public int calculateCurrentHealth() {
            /*
            * 一定不死亡
            */
            return 1;
        }


        //@Override
        override public SwitchTeamEvent checkSwitchTeamEvent() {
            /*
            * 一定不换队
            */
            return null;
        }

        //@Override
        override public int calculateSkillStartUseTime(int fullUseTime) {
            return 0;
        }

    }
}

