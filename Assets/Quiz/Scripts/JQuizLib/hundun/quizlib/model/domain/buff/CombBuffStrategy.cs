namespace hundun.quizlib.model.domain.buff
{
    public class CombBuffStrategy : IBuffStrategy {
        
        public CombBuffStrategy() {
            
        }

        //@Override
        public int modifyScore(int baseScoreAddition, int buffDuration) {
            return buffDuration > 0 ? baseScoreAddition + buffDuration : baseScoreAddition;
        }

    }
}


