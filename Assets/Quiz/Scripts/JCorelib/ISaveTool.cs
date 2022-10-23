public interface ISaveTool<T_SAVE> {
    void lazyInitOnGameCreate();
    bool hasSave();
    void writeRootSaveData(T_SAVE saveData);
    T_SAVE readRootSaveData();
}