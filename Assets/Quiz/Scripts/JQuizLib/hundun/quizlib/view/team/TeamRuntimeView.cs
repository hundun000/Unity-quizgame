using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using hundun.quizlib.view.buff;
using hundun.quizlib.view.role;

/**
 * @author hundun
 * Created on 2021/05/10
 */
namespace hundun.quizlib.view.team
{
    public class TeamRuntimeView {
        [JsonProperty]
        public String name {get; set;}
        [JsonProperty]
        public int matchScore {get; set;}
        [JsonProperty]
        public RoleRuntimeView roleRuntimeInfo {get; set;}
        [JsonProperty]
        public List<BuffRuntimeView> runtimeBuffs {get; set;}
        [JsonProperty]
        public bool alive {get; set;}
        [JsonProperty]
        public int health;
    }
}

