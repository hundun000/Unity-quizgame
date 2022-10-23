using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JQuizGdxGame : BaseHundunGame<JQuizRootSaveData>
{
    public static JQuizGdxGame INSTANCE { get; private set; }

    public QuizLibBridge quizLibBridge { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        JQuizGdxGame.INSTANCE = this;
        DontDestroyOnLoad(gameObject);

        this.saveTool = new TempSaveTool();
        libgdxGameCreate();
    }

    class TempSaveTool : ISaveTool<JQuizRootSaveData>
    {
        bool ISaveTool<JQuizRootSaveData>.hasSave()
        {
            // FIXME
            return false;
        }

        void ISaveTool<JQuizRootSaveData>.lazyInitOnGameCreate()
        {
            // FIXME
        }

        JQuizRootSaveData ISaveTool<JQuizRootSaveData>.readRootSaveData()
        {
            // FIXME
            return null;
        }

        void ISaveTool<JQuizRootSaveData>.writeRootSaveData(JQuizRootSaveData saveData)
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
