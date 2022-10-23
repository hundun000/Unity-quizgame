using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using hundun.quizlib.view.skill;

/**
 * @author hundun
 * Created on 2021/06/28
 */
namespace hundun.quizlib.view.role
{
    public class RoleRuntimeView {
        [JsonProperty]
        public String name {get; set;}
        [JsonProperty]
        public List<SkillSlotRuntimeView> skillSlotRuntimeViews {get; set;}
    }
}

