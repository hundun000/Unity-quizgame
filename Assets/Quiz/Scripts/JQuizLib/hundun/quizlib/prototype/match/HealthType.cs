using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.match
{
    public enum HealthType {
        
        /**
        * 生命值即为剩余可连续答错数
        */
        CONSECUTIVE_WRONG_AT_LEAST, 
        /**
        * 生命值即为已答题目总数
        */
        SUM,
        /**
        * 无尽
        */
        ENDLESS
    }
}
