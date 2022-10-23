using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.match
{
    public enum ClientActionType {
        START_MATCH,
        NEXT_QUESTION,
        ANSWER,
        USE_SKILL
    }
}
