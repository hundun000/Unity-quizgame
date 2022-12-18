using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreen : BaseHundunScreen
{
    //int BUTTON_WIDTH = 100;
    //int BUTTON_BIG_HEIGHT = 100;
    //int BUTTON_SMALL_HEIGHT = 75;

    Image title;
    GameObject _buttonContinueGame;
    GameObject _buttonNewGame;
    GameObject _buttonHistoryScreen;

    // ------ unity adapter member ------
    protected GameObject _buttonAreaRoot;

    override protected void Awake()
    {
        base.Awake();

        this._buttonAreaRoot = this.UiRoot.transform.Find("_buttonAreaRoot").gameObject;

        this._buttonContinueGame = this.Templates.transform.Find("_buttonAreaRootTest/_buttonContinueGameCell").gameObject;
        this._buttonNewGame = this.Templates.transform.Find("_buttonAreaRootTest/_buttonNewGameCell").gameObject;
        this._buttonHistoryScreen = this.Templates.transform.Find("_buttonAreaRootTest/_buttonHistoryScreenCell").gameObject;

        this.title = this.UiRoot.transform.Find("_title").GetComponent<Image>();
    }

    private void initScene2d()
    {

        _buttonAreaRoot.transform.AsTableClear();

        if (game.gameHasSave())
        {
            var buttonContinueGameInstance = _buttonAreaRoot.transform.AsTableAddGameobject(_buttonContinueGame);
            buttonContinueGameInstance.GetComponentInChildren<Button>().onClick.AddListener(() => {
                game.gameLoadOrNew(true);
                SceneManager.LoadScene("PrepareScene");
            });
        }
        var buttonNewGameInstance = _buttonAreaRoot.transform.AsTableAddGameobject(_buttonNewGame);
        buttonNewGameInstance.GetComponentInChildren<Button>().onClick.AddListener(() => {
            game.gameLoadOrNew(false);
            SceneManager.LoadScene("PrepareScene");
        });
        var buttonHistoryScreenInstance = _buttonAreaRoot.transform.AsTableAddGameobject(_buttonHistoryScreen);
        buttonHistoryScreenInstance.GetComponentInChildren<Button>().onClick.AddListener(() => {
            game.gameLoadOrNew(false);
            SceneManager.LoadScene("HistoryScene");
        });
    }

    override protected void Start()
    {
        base.Start();

        initScene2d();
    }
}