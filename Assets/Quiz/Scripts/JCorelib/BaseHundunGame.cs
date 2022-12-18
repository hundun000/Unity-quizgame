using System;
using System.Collections;
using UnityEngine;

public abstract class BaseHundunGame<T_SAVE>
{

    protected ISaveTool<T_SAVE> saveTool;

    // ------ init in createStage1(), or keep null ------
    public BaseViewModelContext modelContext;
    public AbstractSaveHandler<T_SAVE> saveHandler;

    public BaseHundunGame(ISaveTool<T_SAVE> saveTool)
    {
        this.saveTool = saveTool;
    }

    protected abstract void createStage1();
    protected abstract void createStage3();

    // Use this for initialization
    protected void libgdxGameCreate()
    {
        createStage1();

        this.saveTool.lazyInitOnGameCreate();
        this.modelContext.lazyInitOnGameCreate();

        createStage3();

    }

    // ====== save & load ======
    public void systemSettingLoadOrNew()
    {

        T_SAVE saveData;
        if (saveTool.hasSave())
        {
            saveData = saveTool.readRootSaveData();
        }
        else
        {
            saveData = saveHandler.genereateNewGameSaveData();
        }

        saveHandler.applySystemSetting(saveData);
        Debug.LogFormat("[{0}] {1}", this.GetType().Name, "systemSettingLoad call");
    }


    public void gameLoadOrNew(bool load)
    {

        T_SAVE saveData;
        if (load && saveTool.hasSave())
        {
            saveData = saveTool.readRootSaveData();
        }
        else
        {
            saveData = saveHandler.genereateNewGameSaveData();
        }

        saveHandler.applyGameSaveData(saveData);
        Debug.LogFormat("[{0}] {1}", this.GetType().Name, load ? "load game done" : "new game done");
    }
    public void gameSaveCurrent()
    {
        Debug.LogFormat("[{0}] {1}", this.GetType().Name, "saveCurrent called");
        saveTool.writeRootSaveData(saveHandler.currentSituationToSaveData());
    }
    public bool gameHasSave()
    {
        return saveTool.hasSave();
    }
}
