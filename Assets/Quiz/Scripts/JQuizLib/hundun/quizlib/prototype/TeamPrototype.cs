using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype
{
    [Serializable]
    public class TeamPrototype {


        public TeamPrototype(string name, List<string> pickTags, List<string> banTags, RolePrototype rolePrototype)
        {
            this.name = name;
            this.pickTags = pickTags;
            this.banTags = banTags;
            this.rolePrototype = rolePrototype;
        }

        [JsonProperty]
        public String name {get; set;}
        [JsonProperty]
        public List<String> pickTags {get; set;}
        [JsonProperty]
        public List<String> banTags {get; set;}
        [JsonProperty]
        public RolePrototype rolePrototype {get; set;}
    }
}

