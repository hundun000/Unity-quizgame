using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using hundun.quizlib.prototype.question;

namespace hundun.quizlib.view.question
{
    public class QuestionView {
        [JsonProperty]
        public String id {get; set;}
        [JsonProperty]
        public String stem {get; set;}
        [JsonProperty]
        public List<String> options {get; set;}
        [JsonProperty]
        public int answer {get; set;}
        [JsonProperty]
        public Resource resource {get; set;}
        [JsonProperty]
        public HashSet<String> tags {get; set;}
    }
}

