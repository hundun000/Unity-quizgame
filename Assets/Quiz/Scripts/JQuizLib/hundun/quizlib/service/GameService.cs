

using hundun.quizlib.context;
using hundun.quizlib.exception;
using hundun.quizlib.model;
using hundun.quizlib.model.domain;
using hundun.quizlib.model.domain.match;
using hundun.quizlib.model.domain.match.strategy;
using hundun.quizlib.prototype;
using hundun.quizlib.prototype.match;
using hundun.quizlib.view.match;
using System.Collections.Generic;
using System;
using System.Linq;

namespace hundun.quizlib.service
{
    public class GameService : IQuizComponent
    {

        private TeamService teamService;
        private MatchStrategyFactory matchStrategyFactory;
        private SessionService sessionService;

        List<MatchRecord> matchRecords = new List<MatchRecord>();

        public GameService()
        {

        }

        public void postConstruct(QuizComponentContext context)
        {
            this.teamService = context.teamService;
            this.matchStrategyFactory = context.matchStrategyFactory;
            this.sessionService = context.sessionService;
        }

        public MatchSituationView createMatch(MatchConfig matchConfig)
        {
            //log.info("createEndlessMatch by {}", matchConfig);

            BaseMatchStrategy strategy = matchStrategyFactory.getMatchStrategy(matchConfig.matchStrategyType);

            SessionDataPackage sessionDataPackage = sessionService.createSession(matchConfig.questionPackageName);

            BaseMatch match = new BaseMatch(sessionDataPackage.sessionId, strategy);
            List<TeamRuntimeModel> teamRuntimeModels = new List<TeamRuntimeModel>();
            foreach (String teamName in matchConfig.teamNames)
            {
                TeamPrototype teamPrototype = teamService.getTeam(teamName);
                RoleRuntimeModel roleRuntimeModel;
                if (teamPrototype.rolePrototype != null)
                {
                    List<SkillSlotRuntimeModel> skillSlotRuntimeModels = teamPrototype.rolePrototype.skillSlotPrototypes
                            .Select(prototype => {
                                int startUseTime = strategy.calculateSkillStartUseTime(prototype.fullUseTime);
                                SkillSlotRuntimeModel runtimeModel = new SkillSlotRuntimeModel(prototype, startUseTime);
                                return runtimeModel;
                            })
                            .ToList()
                            ;
                    roleRuntimeModel = new RoleRuntimeModel(teamPrototype.rolePrototype, skillSlotRuntimeModels);
                }
                else
                {
                    roleRuntimeModel = null;
                }
                teamRuntimeModels.Add(new TeamRuntimeModel(teamPrototype, roleRuntimeModel));
            }
            match.initTeams(teamRuntimeModels);

            sessionDataPackage.match = (match);

            //log.info("match created, id = {}", match.getSessionId());
            MatchSituationView matchSituationView = match.toMatchSituationView();
            return matchSituationView;
        }





        public MatchSituationView startMatch(String sessionId)
        {
            //log.info("start match:{}", sessionId);
            SessionDataPackage sessionDataPackage = sessionService.getSessionDataPackage(sessionId);
            BaseMatch match = sessionDataPackage.match;
            match.start(sessionDataPackage.questionIds);
            MatchSituationView matchSituationView = match.toMatchSituationView();
            return matchSituationView;
        }

        public MatchSituationView nextQustion(String sessionId)
        {
            //log.info("nextQustion:{}", sessionId);
            SessionDataPackage sessionDataPackage = sessionService.getSessionDataPackage(sessionId);
            BaseMatch match = sessionDataPackage.match;
            match.nextQustion();
            MatchSituationView matchSituationView = match.toMatchSituationView();
            return matchSituationView;
        }

        private void finishMatch(BaseMatch match)
        {
            matchRecords.Add(new MatchRecord(match));

        }


        public MatchSituationView teamAnswer(String sessionId, String answer)
        {
            //log.info("teamAnswer match:{}, answer = {}", sessionId, answer);
            SessionDataPackage sessionDataPackage = sessionService.getSessionDataPackage(sessionId);
            BaseMatch match = sessionDataPackage.match;
            match.teamAnswer(answer);
            if (match.finishEvent != null)
            {
                finishMatch(match);
            }
            MatchSituationView matchSituationView = match.toMatchSituationView();
            return matchSituationView;
        }

        public MatchSituationView teamUseSkill(String sessionId, String skillName)
        {
            //log.info("teamUseSkill match:{}, skillName = {}", sessionId, skillName);
            SessionDataPackage sessionDataPackage = sessionService.getSessionDataPackage(sessionId);
            BaseMatch match = sessionDataPackage.match;
            match.teamUseSkill(skillName);
            MatchSituationView matchSituationView = match.toMatchSituationView();
            return matchSituationView;
        }

    }

}


