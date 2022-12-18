using hundun.quizlib.context;
using hundun.quizlib.exception;
using hundun.quizlib.model.domain;
using hundun.quizlib.prototype;
using System.Collections.Generic;
using System;

namespace hundun.quizlib.service
{
    public class TeamService : IQuizComponent
    {


        RoleSkillService roleSkillService;


        public void postConstruct(QuizComponentContext context)
        {
            this.roleSkillService = context.roleSkillService;
        }

        private Dictionary<String, TeamPrototype> teamPrototypes = new Dictionary<string, TeamPrototype>();

        public void registerTeam(String teamName, List<String> pickTagNames, List<String> banTagNames, String roleName)
        {

            RolePrototype rolePrototype = roleName != null ? roleSkillService.getRole(roleName) : null;

            TeamPrototype teamPrototype = new TeamPrototype(teamName, pickTagNames, banTagNames, rolePrototype);
            teamPrototypes.put(teamName, teamPrototype); 

        }


        public void updateTeam(TeamPrototype teamPrototype)
        {
            if (!existTeam(teamPrototype.name)) {
                throw new NotFoundException(typeof(TeamRuntimeModel).Name, teamPrototype.name);
            }
            teamPrototypes.put(teamPrototype.name, teamPrototype);
        }
    
        public TeamPrototype getTeam(String name)
        {

            if (!existTeam(name)) {
                throw new NotFoundException(typeof(TeamRuntimeModel).Name, name);
            }
            return teamPrototypes.get(name);
        }
    
        public List<TeamPrototype> listTeams()
        {
            return new List<TeamPrototype>(teamPrototypes.Values);
        }

        public bool existTeam(String name)
        {
            return teamPrototypes.containsKey(name);
        }


    

    }
}




