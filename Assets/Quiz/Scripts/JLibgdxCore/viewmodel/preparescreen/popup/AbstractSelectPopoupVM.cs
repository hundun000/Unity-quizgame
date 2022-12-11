using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public abstract class AbstractSelectPopoupVM<T> : MonoBehaviour
{


    List<T> candidateVMs;
    GameObject _scrollViewContent;

    // ------ unity adapter member ------
    public GameObject candidateVMPrefab;
    

    protected virtual void Awake()
    {
        this._scrollViewContent = this.transform.Find("Scroll View").GetComponent<ScrollRect>().content.gameObject;
    }

    public void updateScrollPane(List<T> candidateVMs, List<GameObject> candidateVMGameObjects)
    {
        foreach (Transform child in _scrollViewContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        this.candidateVMs = candidateVMs;

        candidateVMGameObjects.ForEach(it => {
            it.transform.SetParent(_scrollViewContent.transform);
        });
    }
}
