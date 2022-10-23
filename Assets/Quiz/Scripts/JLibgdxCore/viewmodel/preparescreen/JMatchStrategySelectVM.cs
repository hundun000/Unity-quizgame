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
    VisualTreeAsset m_ListEntryTemplate;
    ListView m_CharacterList;

    // --- java ----
    JQuizGdxGame game;

    MatchStrategyType currentType;
    //List<MatchStrategyNode> nodes = new List<MatchStrategyNode>();


    IMatchStrategyChangeListener slotNumListener;

    // Start is called before the first frame update
    void Start()
    {
        // --- java ---

        //this.game = _game.GetComponent<JQuizGdxGame>();
        //this.slotNumListener = _game.GetComponent<JPrepareScreen>();
        //this._imageComponent.sprite = Resources.Load<Sprite>("Quiz/testNinePatch");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initUI(List<MatchStrategyType> teamPrototypesAKAm_AllCharacters)
    {

        //nodes.Clear();

        m_CharacterList.makeItem = () =>
        {
            var newListEntry = m_ListEntryTemplate.Instantiate();
            MatchStrategyNode vm = new MatchStrategyNode();
            newListEntry.userData = vm;
            vm.SetVisualElement(newListEntry);
            //nodes.Add(vm);
            return newListEntry;
        };

        m_CharacterList.bindItem = (item, index) =>
        {
            var vm = (item.userData as MatchStrategyNode);
            vm.updatePrototypeAKASetCharacterData(teamPrototypesAKAm_AllCharacters[index]);
        };

        m_CharacterList.itemsSource = teamPrototypesAKAm_AllCharacters;
    }

    public void checkSlotNum(MatchStrategyType newType)
    {

        if (currentType != newType)
        {
            currentType = newType;
            // FIXME
            //slotNumListener.onMatchStrategyChange(newType);
        }
        //for (int i = 0; i < nodes.Count; i++)
        //{
        //    MatchStrategyNode vm = nodes.get(i);
        //    vm.updateRuntime(vm.type == currentType);
        //}
    }

    internal void InitializeCharacterList(VisualElement root, VisualTreeAsset listEntryTemplate)
    {
        this.m_ListEntryTemplate = listEntryTemplate;

        this.m_CharacterList = root.Q<ListView>("character-list");

        initUI(JavaFeatureExtension.ArraysAsList(MatchStrategyType.PRE, MatchStrategyType.MAIN));
    }

    public interface IMatchStrategyChangeListener
    {
        void onMatchStrategyChange(MatchStrategyType newType);
    }

    private class MatchStrategyNode
    {
        // --- unity ---
        Label m_NameLabel;

        // --- java ---
        internal static int SIGN_SIZE = 50;
        internal UnityEngine.UIElements.Image signSlotImage;
        internal Sprite signDrawable;
        internal int NAME_WIDTH = 200;
        internal Text nameLabel;
        
        internal MatchStrategyType type;

        public MatchStrategyNode()
        {

        }


        public void SetVisualElement(VisualElement visualElement)
        {
            m_NameLabel = visualElement.Q<Label>("character-name");
        }

        internal void updatePrototypeAKASetCharacterData(MatchStrategyType type)
        {
            this.type = type;
            m_NameLabel.text = (JMatchStrategyInfoVM.toMatchStrategyTypeChinese(type));
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


