namespace hundun.quizlib.model.domain.buff
{
    public interface IBuffStrategy {
        int modifyScore(int baseScoreAddition, int buffDuration);
    }
}
