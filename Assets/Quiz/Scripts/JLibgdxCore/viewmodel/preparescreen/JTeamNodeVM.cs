using hundun.quizlib.prototype;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JTeamNodeVM : MonoBehaviour
{
    public const int NODE_WIDTH = 500;
    public const int NODE_HEIGHT = 100;

    public TeamPrototype data { get; private set; }
    
    Text _label;

    Text _banInfoLabel;
    Text _pickInfoLabel;

    // ------ unity adapter member ------
    public Button _selfAsButton { get; private set; }

    /// <summary>
    /// [unity adapter method]
    /// </summary>
    internal void postPrefabInitialization(TeamPrototype teamPrototype)
    {
        updateData(teamPrototype);
    }

    void Awake()
    {
        this._label = this.transform.Find("_label").GetComponent<Text>();
        this._banInfoLabel = this.transform.Find("banpickInfoGroup/_banInfoLabel").GetComponent<Text>();
        this._pickInfoLabel = this.transform.Find("banpickInfoGroup/_pickInfoLabel").GetComponent<Text>();
        this._selfAsButton = this.GetComponent<Button>();
    }

    private void updateData(TeamPrototype data)
    {
        this.data = data;

        _label.text = (data.name);
        _banInfoLabel.text = (data.banTags.Count.ToString());
        _pickInfoLabel.text = (data.pickTags.Count.ToString());
    }
}
