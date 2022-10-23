using hundun.quizlib.model.domain;
using hundun.quizlib.model.domain.match;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.model
{
    public class SessionDataPackage
    {
        [JsonProperty]
        public String sessionId { get; set; }
        [JsonProperty]
        public List<String> questionIds { get; set; }
        [JsonProperty]
        public List<QuestionModel> dirtyQuestionIds { get; set; }
        [JsonProperty]
        public HashSet<String> tags { get; set; }
        [JsonProperty]
        public bool allowImageResource { get; set; }
        [JsonProperty]
        public bool allowVoiceResource { get; set; }
        [JsonProperty]
        public BaseMatch match;

        public SessionDataPackage()
        {
            this.allowImageResource = true;
            this.allowVoiceResource = false;
        }
    }
}


