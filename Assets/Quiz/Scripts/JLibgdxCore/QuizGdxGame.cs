using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuizGdxGame : BaseHundunGame<QuizRootSaveData>
{
    public static readonly int LOGIC_FRAME_PER_SECOND = 20;
    

    public QuizLibBridge quizLibBridge { get; private set; }


    // ------ unity adapter member ------
    private static QuizGdxGame instance;
    public static QuizGdxGame INSTANCE { 
        get 
        { 
            if (instance == null)
            {
                instance = new QuizGdxGame();
            }
            return instance;
        }
    }
    public HistoryScreenAsSubGameSaveHandlerDelegation historyScreenAsSubGameSaveHandlerDelegation { get; private set; }

    public QuizGdxGame() : base(new UnitySaveTool())
    {
        this.historyScreenAsSubGameSaveHandlerDelegation = new HistoryScreenAsSubGameSaveHandlerDelegation();
        libgdxGameCreate();
    }

    class UnitySaveTool : ISaveTool<QuizRootSaveData>
    {
        string fileName = "save.json";
        internal UnitySaveTool()
        {
        }

        bool ISaveTool<QuizRootSaveData>.hasSave()
        {
            return File.Exists(GetFilePath(fileName));
        }

        void ISaveTool<QuizRootSaveData>.lazyInitOnGameCreate()
        {
        }

        QuizRootSaveData ISaveTool<QuizRootSaveData>.readRootSaveData()
        {
            QuizRootSaveData data = new QuizRootSaveData();
            string json = ReadFromFIle(fileName);
            JsonUtility.FromJsonOverwrite(json, data);
            return data;
        }

        void ISaveTool<QuizRootSaveData>.writeRootSaveData(QuizRootSaveData saveData)
        {
            string json = JsonUtility.ToJson(saveData);
            WriteToFile(fileName, json);
        }

        private void WriteToFile(string fileName, string json)
        {
            string path = GetFilePath(fileName);
            FileStream fileStream = new FileStream(path, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }

        private string ReadFromFIle(string fileName)
        {
            string path = GetFilePath(fileName);
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string json = reader.ReadToEnd();
                    return json;
                }
            }
            else
            {
                Debug.LogWarning("File not found");
            }

            return "Success";
        }

        private string GetFilePath(string fileName)
        {
            return Application.persistentDataPath + "/" + fileName;
        }
    }

    protected override void createStage1()
    {
        // ------ for super ------
        this.modelContext = new QuizViewModelContext(this);
        this.saveHandler = new QuizSaveHandler();
        // ------ for self ------
        this.quizLibBridge = new QuizLibBridge(this);
    }

    protected override void createStage3()
    {
        systemSettingLoadOrNew();

    }
}
