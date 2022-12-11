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

public class MatchStrategySelectVM : MonoBehaviour
{
    // --- unity editor bind ----
    public GameObject myPrefab;

    // --- java ----

    MatchStrategyType currentType;
    List<MatchStrategyNode> nodes = new List<MatchStrategyNode>();

    IMatchStrategyChangeListener slotNumListener;

    void Awake()
    {
        this.slotNumListener = this.GetComponentInParent<PrepareScreen>();

        initUI(JavaFeatureExtension.ArraysAsList(MatchStrategyType.PRE, MatchStrategyType.MAIN));
    }

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

    private void initUI(List<MatchStrategyType> teamPrototypes)
    {

        //nodes.Clear();
        teamPrototypes.ForEach(it => {
            
            GameObject nodeInstance = Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            MatchStrategyNode node = nodeInstance.GetComponent<MatchStrategyNode>();
            nodeInstance.transform.SetParent(this.transform);
            node.postPrefabInitialization(this);

            node.updatePrototype(it);
            nodes.Add(node);
        });
        
    }

    public void checkSlotNum(MatchStrategyType newType)
    {

        if (currentType != newType)
        {
            currentType = newType;
            slotNumListener.onMatchStrategyChange(newType);
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


}


