using System;
using System.Collections;
using UnityEngine;

public class BaseHundunScreen : MonoBehaviour
{

    public LogicFrameHelper logicFrameHelper;

    // ------ unity adapter member ------
    public QuizGdxGame game;
    public GameObject _popoupRoot;
    public GameObject _uiRoot;
    public GameObject _templates;

    virtual protected void Awake()
    {
        this._popoupRoot = this.transform.Find("_popupRoot").gameObject;
        this._uiRoot = this.transform.Find("_uiRoot").gameObject;
        this._templates = this.transform.Find("_templates").gameObject;
    }

    /// <summary>
    /// 供Screen尚未完成Awake时，子组件Awake使用。
    /// </summary>
    public GameObject GetTemplatesDuringAwake()
    {
        return this.transform.Find("_templates").gameObject;
    }

    virtual protected void Start()
    {
        this.game = QuizGdxGame.INSTANCE;
    }

    virtual protected void Update()
    {
        float delta = Time.deltaTime;

        if (logicFrameHelper != null)
        {
            bool isLogicFrame = logicFrameHelper.logicFrameCheck(delta);
            if (isLogicFrame)
            {
                onLogicFrame();
            }
        }
    }

    virtual protected void onLogicFrame()
    {
        // base-class do nothing
    }
}