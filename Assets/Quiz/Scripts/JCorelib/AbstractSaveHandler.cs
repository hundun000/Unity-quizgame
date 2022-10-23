using System;

public abstract class AbstractSaveHandler<T_SAVE> {
    public abstract void applySystemSetting(T_SAVE saveData);
    public abstract void applyGameSaveData(T_SAVE saveData);
    public abstract T_SAVE currentSituationToSaveData();
    public abstract T_SAVE genereateNewGameSaveData();
    public abstract void registerSubHandler(Object objecz);
}