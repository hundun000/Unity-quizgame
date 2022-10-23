using hundun.quizlib.prototype.skill;

using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain
{
    public class SkillSlotRuntimeModel {
        
        public SkillSlotPrototype prototype { get; set; }
        public int remainUseTime { get; set; }

        public SkillSlotRuntimeModel(SkillSlotPrototype prototype, int startUseTime) {
            this.prototype = prototype;
            this.remainUseTime = startUseTime;
        }

        public bool canUseOnce(String skillName) {
            return remainUseTime > 0;
        }
        
        public bool useOnce(String skillName) {
            if (canUseOnce(skillName)) {
                remainUseTime--;
                return true;
            }
            return false;
        }
    }
}

