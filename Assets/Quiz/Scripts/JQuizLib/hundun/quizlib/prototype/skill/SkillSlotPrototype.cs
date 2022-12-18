using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.skill
{
    [Serializable]
    public class SkillSlotPrototype {


        public SkillSlotPrototype(string name, string showName, string description, List<String> eventArgs, List<AddBuffSkillEffect> backendEffects, int fullUseTime)
        {
            this.name = name;
            this.showName = showName;
            this.description = description;
            this.eventArgs = eventArgs;
            this.backendEffects = backendEffects;
            this.fullUseTime = fullUseTime;
        }

        [JsonProperty]
        public  String name {get; set;}
        [JsonProperty]
        public String showName {get; set;}
        [JsonProperty]
        public String description {get; set;}
        [JsonProperty]
        public List<String> eventArgs {get; set;}
        [JsonProperty]
        public List<AddBuffSkillEffect> backendEffects {get; set;}
        [JsonProperty]
        public int fullUseTime {get; set;}
    }
}

