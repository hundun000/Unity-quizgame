
using hundun.quizlib.prototype;
using hundun.quizlib.prototype.skill;
using System;
using System.Collections.Generic;

namespace hundun.quizlib.service
{
    public class RoleSkillService
    {

        Dictionary<String, RolePrototype> roles = new Dictionary<string, RolePrototype>();
        Dictionary<String, SkillSlotPrototype> skills = new Dictionary<string, SkillSlotPrototype>();

        public RoleSkillService()
        {



        }


        public void registerRole(RolePrototype role)
        {
            roles.put(role.name, role);
        }

        public void registerSkill(SkillSlotPrototype skill)
        {
            skills.put(skill.name, skill);
        }

        public SkillSlotPrototype getSkillSlotPrototype(String name)
        {
            return skills.get(name);
        }



        public RolePrototype getRole(String name)
        {
            return roles.get(name);
        }

        public bool existRole(String name)
        {
            return roles.containsKey(name);
        }

        public ICollection<RolePrototype> listRoles()
        {
            return roles.Values;
        }
    }

}
