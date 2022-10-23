using hundun.quizlib;
using hundun.quizlib.prototype.match;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class JMatchStrategySelectVM : MonoBehaviour
{
    // --- unity editor bind ----
    GameObject _game;

    // --- fake table ---
    //private UnityEngine.UI.Image _imageComponent;
    private VisualElement m_Root;
    private VisualElement m_SlotContainer;


    // --- java ----
    JQuizGdxGame game;

    MatchStrategyType currentType;
    List<MatchStrategyNode> nodes = new List<MatchStrategyNode>();


    IMatchStrategyChangeListener slotNumListener;

    // Start is called before the first frame update
    void Start()
    {
        // --- fake table ---
        //this._imageComponent = this.GetComponent<UnityEngine.UI.Image>();
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");


        // --- java ---

        //this.game = _game.GetComponent<JQuizGdxGame>();
        //this.slotNumListener = _game.GetComponent<JPrepareScreen>();
        //this._imageComponent.sprite = Resources.Load<Sprite>("Quiz/testNinePatch");

        initUI(JavaFeatureExtension.ArraysAsList(MatchStrategyType.PRE, MatchStrategyType.MAIN));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initUI(List<MatchStrategyType> teamPrototypes)
    {

        nodes.Clear();
        teamPrototypes.ForEach(it => {
            MatchStrategyNode vm = new MatchStrategyNode();
            vm.updatePrototype(it);
            nodes.Add(vm);

            m_SlotContainer.Add(vm);
        });
    }

    public void checkSlotNum(MatchStrategyType newType)
    {

        if (currentType != newType)
        {
            currentType = newType;
            // FIXME
            //slotNumListener.onMatchStrategyChange(newType);
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            MatchStrategyNode vm = nodes.get(i);
            vm.updateRuntime(vm.type == currentType);
        }
    }



    public interface IMatchStrategyChangeListener
    {
        void onMatchStrategyChange(MatchStrategyType newType);
    }

    private class MatchStrategyNode : VisualElement
    {
        // --- unity ---
        public UnityEngine.UIElements.Image Icon;
        public string ItemGuid = "";

        // --- java ---
        int SIGN_SIZE = 50;
        internal UnityEngine.UIElements.Image signSlotImage;
        internal Sprite signDrawable;
        internal int NAME_WIDTH = 200;
        internal Text nameLabel;
        
        internal MatchStrategyType type;

        public MatchStrategyNode()
        {
            //Create a new Image element and add it to the root
            Icon = new UnityEngine.UIElements.Image();
            Add(Icon);

            //Add USS style properties to the elements
            Icon.AddToClassList("slotIcon");
            AddToClassList("slotContainer");

            //Register event listeners
            //RegisterCallback<PointerDownEvent>(OnPointerDown);
            this.Icon.sprite = Resources.Load<Sprite>("Quiz/playScreenUI/systemButton");
        }

        internal void updatePrototype(MatchStrategyType type)
        {
            this.type = type;
            //nameLabel.text = (JMatchStrategyInfoVM.toMatchStrategyTypeChinese(type));
        }

        internal void updateRuntime(bool isCurrent)
        {
            if (isCurrent)
            {
                signSlotImage.sprite = (signDrawable);
            }
            else
            {
                signSlotImage.sprite = (null);
            }
        }
    }
}


