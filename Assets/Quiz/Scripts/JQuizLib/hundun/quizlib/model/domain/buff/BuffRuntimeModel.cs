using hundun.quizlib.prototype.buff;
using hundun.quizlib.view.buff;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.model.domain.buff
{
    public class BuffRuntimeModel {
        //@Getter
        public BuffPrototype prototype { get; private set; }
        //@Getter
        public int duration { get; private set; }

        //@Getter
        public IBuffStrategy buffStrategy { get; private set; }

        public BuffRuntimeModel(BuffPrototype prototype, int duration) {
            this.prototype = prototype;
            this.duration = duration;
            
            if (prototype.buffStrategyType == BuffStrategyType.SCORE_MODIFY) {
                this.buffStrategy = new CombBuffStrategy();
            }
        }
        
        public void minusOneDurationAndCheckMaxDuration() {
            duration--;
            if (duration > prototype.maxDuration) {
                duration = prototype.maxDuration;
            }
        }
        
        public void clearDuration() {
            duration = 0;
        }
        
        public void addDuration(int plus) {
            duration += plus;
        }
        


        public BuffRuntimeView toRunTimeBuffDTO() {
            BuffRuntimeView dto = new BuffRuntimeView();
            dto.name = (this.prototype.name);
            dto.description = (this.prototype.description);
            dto.duration = (duration);
            return dto;
        }

    }

}


