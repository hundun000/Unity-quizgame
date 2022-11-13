using System.Collections.Generic;
using System;
using static QuizRootSaveData;
using UnityEngine;

public class JQuizSaveHandler : AbstractSaveHandler<QuizRootSaveData>
{

    bool gameSaveInited = false;
    bool systemSettingInited = false;
    List<ISubGameSaveHandler> subGameSaveHandlers = new List<ISubGameSaveHandler>();
    List<ISubSystemSettingHandler> subSystemSettingHandlers = new List<ISubSystemSettingHandler>();

    public override void applyGameSaveData(QuizRootSaveData saveData)
    {
        gameSaveInited = true;
        if (saveData.gameSaveData != null)
        {
            subGameSaveHandlers.ForEach(it => it.applyGameSaveData(saveData.gameSaveData));
        }
    }

    public override void applySystemSetting(QuizRootSaveData rootSaveData)
    {
        systemSettingInited = true;
        if (rootSaveData.systemSetting != null)
        {
            subSystemSettingHandlers.ForEach(it => it.applySystemSetting(rootSaveData.systemSetting));
        }
    }

    public override QuizRootSaveData currentSituationToSaveData()
    {
        // FIXEM
        return null;
    }

    public override QuizRootSaveData genereateNewGameSaveData()
    {
        QuizRootSaveData rootSaveData = QuizRootSaveData.Factory.newGame();
        return rootSaveData;
    }

    public override void registerSubHandler(object objecz)
    {
        if (objecz is ISubGameSaveHandler) {
            subGameSaveHandlers.Add((ISubGameSaveHandler)objecz);
            Debug.LogFormat("[{0}] {1}", this.GetType().Name, objecz.GetType().Name + " register as ISubGameSaveHandler");
        }
        if (objecz is ISubSystemSettingHandler) {
            subSystemSettingHandlers.Add((ISubSystemSettingHandler)objecz);
            Debug.LogFormat("[{0}] {1}", this.GetType().Name, objecz.GetType().Name + " register as ISubSystemSettingHandler");
        }
    }

    public interface ISubGameSaveHandler
    {
        void applyGameSaveData(MyGameSaveData myGameSaveData);
        void currentSituationToSaveData(MyGameSaveData myGameSaveData);
    }

    public interface ISubSystemSettingHandler
    {
        void applySystemSetting(SystemSetting systemSetting);
        void currentSituationToSystemSetting(SystemSetting systemSetting);
    }
}