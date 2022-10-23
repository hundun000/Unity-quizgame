using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.question
{
    public class Resource {
        [JsonProperty]
        public ResourceType type {get; set;}
        [JsonProperty]
        public String data {get; set;}
        
        public Resource(String localFilePathName) {
            this.type = ResourceTypeCompanion.getByLocalFileExtension(localFilePathName);
            this.data = type == ResourceType.NONE ? null : localFilePathName;
        }
        
        public Resource() {
            
        }
    }
}