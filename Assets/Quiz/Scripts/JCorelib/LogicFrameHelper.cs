using Unity.VisualScripting;

public class LogicFrameHelper {

    private readonly int LOGIC_FRAME_PER_SECOND;
    private readonly float LOGIC_FRAME_LENGTH;

    public int clockCount {get; private set;}
    private float logicFramAccumulator;

    public bool logicFramePause { get; set; }

    public LogicFrameHelper(int LOGIC_FRAME_PER_SECOND) {
        this.LOGIC_FRAME_PER_SECOND = LOGIC_FRAME_PER_SECOND;
        this.LOGIC_FRAME_LENGTH = 1f / LOGIC_FRAME_PER_SECOND;
        this.clockCount = 0;
    }
    
    public bool logicFrameCheck(float delta) {
        logicFramAccumulator += delta;
        if (logicFramAccumulator >= LOGIC_FRAME_LENGTH) {
            logicFramAccumulator -= LOGIC_FRAME_LENGTH;
            if (!logicFramePause) {
                clockCount++;
                return true;
            }
        }
        return false;
    }
    
    public double frameNumToSecond(int frameNum) {
        return frameNum * LOGIC_FRAME_LENGTH;
    }

    public int secondToFrameNum(double second) {
        return (int) (LOGIC_FRAME_PER_SECOND * second);
    }
}