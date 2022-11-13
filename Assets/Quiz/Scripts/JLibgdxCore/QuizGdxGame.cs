using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizGdxGame : BaseHundunGame<QuizRootSaveData>
{
    public static readonly int LOGIC_FRAME_PER_SECOND = 20;
    public static QuizGdxGame INSTANCE { get; private set; }

    public QuizLibBridge quizLibBridge { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        QuizGdxGame.INSTANCE = this;
        DontDestroyOnLoad(gameObject);

        this.saveTool = new TempSaveTool();
        libgdxGameCreate();
    }

    class TempSaveTool : ISaveTool<QuizRootSaveData>
    {
        bool ISaveTool<QuizRootSaveData>.hasSave()
        {
            // FIXME
            return false;
        }

        void ISaveTool<QuizRootSaveData>.lazyInitOnGameCreate()
        {
            // FIXME
        }

        QuizRootSaveData ISaveTool<QuizRootSaveData>.readRootSaveData()
        {
            // FIXME
            return null;
        }

        void ISaveTool<QuizRootSaveData>.writeRootSaveData(QuizRootSaveData saveData)
        {
            // FIXME
        }
    }

    protected override void createStage1()
    {
        // ------ for super ------
        this.modelContext = new JQuizViewModelContext(this);
        this.saveHandler = new JQuizSaveHandler();
        // ------ for self ------
        this.quizLibBridge = new QuizLibBridge(this);
    }

    protected override void createStage3()
    {
        systemSettingLoadOrNew();

    }
}
