using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using hundun.quizlib.prototype.skill;

namespace hundun.quizlib.prototype
{
    public class RolePrototype {
        
        [JsonProperty]
        public String name {get; set;}
        [JsonProperty]
        public String description {get; set;}
        [JsonProperty]
        public List<SkillSlotPrototype> skillSlotPrototypes;

    }
}
