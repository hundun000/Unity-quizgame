using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JMenuScreen : BaseHundunScreen
{
    int BUTTON_WIDTH = 100;
    int BUTTON_BIG_HEIGHT = 100;
    int BUTTON_SMALL_HEIGHT = 75;

    GameObject _title;
    GameObject _buttonContinueGame;
    GameObject _buttonNewGame;
    GameObject _buttonHistorySreen;
    GameObject _backImage;

    // ------ unity adapter member ------
    protected GameObject _buttonAreaRoot;


    override protected void Awake()
    {
        base.Awake();

        this._buttonAreaRoot = _uiRoot.transform.Find("_buttonAreaRoot").gameObject;
    }


    //    public StarterMenuScreen(T_GAME game, 
    //            Actor title,
    //            Image backImage,
    //            Actor buttonContinueGame,
    //            Actor buttonNewGame,
    //            Actor buttonIntoSettingScreen
    //            ) {
    //        super(game);
    //        this.backImage = backImage;
    //        this.buttonContinueGame = buttonContinueGame;
    //        this.buttonNewGame = buttonNewGame;
    //        this.buttonIntoSettingScreen = buttonIntoSettingScreen;
    //        
    //
    //    }

    private void initScene2d()
    {

        _buttonAreaRoot.transform.AsTableClear();

        if (game.gameHasSave())
        {
            // TODO
        }

    }


    void Start()
    {
        initScene2d();
    }
}
