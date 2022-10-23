using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * @author hundun
 * Created on 2021/07/17
 */
namespace hundun.quizlib.view.skill
{
    public class SkillSlotRuntimeView {
        public SkillSlotRuntimeView(string name, int remainUseTime)
        {
            this.name = name;
            this.remainUseTime = remainUseTime;
        }

        [JsonProperty]
        public String name {get; set;}
        [JsonProperty]
        public int remainUseTime {get; set;}
    }
}

