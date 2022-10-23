using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.skill
{
    public class AddBuffSkillEffect {

        public AddBuffSkillEffect(string buffName, int duration)
        {
            this.buffName = buffName;
            this.duration = duration;
        }

        [JsonProperty]
        public String buffName {get; set;}
        [JsonProperty]
        public int duration {get; set;}

    }
}


