using System.Collections;
using UnityEngine;

public class BaseHundunScreen : MonoBehaviour
{



    // ------ unity adapter member ------
    protected JQuizGdxGame game;
    protected GameObject _popoupRoot;
    protected GameObject _uiRoot;
    protected GameObject _templates;

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
        this.game = JQuizGdxGame.INSTANCE;
    }
}