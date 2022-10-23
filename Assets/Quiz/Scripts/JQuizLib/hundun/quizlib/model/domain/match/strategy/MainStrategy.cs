using hundun.quizlib.model.domain;
using hundun.quizlib.prototype.@event;
using hundun.quizlib.prototype.match;
using hundun.quizlib.service;

using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain.match.strategy
{
    public class MainStrategy : BaseMatchStrategy {
        


        public MainStrategy (
                QuestionService questionService, 
                TeamService teamService, 
                RoleSkillService roleSkillService,
                BuffService buffService
                )
            : base(questionService, teamService, roleSkillService, buffService,
                    HealthType.CONSECUTIVE_WRONG_AT_LEAST
                    ) {
        }

        
        //@Override
        override public SwitchTeamEvent checkSwitchTeamEvent() {
            /*
            * 每一题换队（被调用一定换）
            */
            TeamRuntimeModel lastTeam = parent.getCurrentTeam();
            switchToNextTeam();
            return MatchEventFactory.getTypeSwitchTeam(lastTeam, parent.getCurrentTeam());
        }

        int fullHealth = 2;

        //@Override
        override public int calculateCurrentHealth() {
            
            /*
            * 连续答错数, 即为健康度的减少量。
            */
            int currentHealth = fullHealth - parent.getRecorder().countConsecutiveWrong(parent.getCurrentTeam().getName(), fullHealth);
            return currentHealth;
        }

        //@Override
        override public int calculateSkillStartUseTime(int fullUseTime) {
            return fullUseTime;
        }
    }
}


