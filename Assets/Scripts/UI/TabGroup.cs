using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton tabSelected;
    public List<GameObject> objectToSwap;

    public void Subscribe(TabButton btn)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(btn);
    }

    public void OnTabEnter(TabButton btn)
    {
        ResetTabs();
        if (tabSelected == null || btn != tabSelected)
        {
            btn.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton btn)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton btn)
    {
        tabSelected = btn;
        ResetTabs();
        btn.background.sprite = tabActive;
        int index = btn.transform.GetSiblingIndex();
        for(int i = 0; i < objectToSwap.Count; i++)
        {
            if(i == index)
            {
                objectToSwap[i].SetActive(true);
            }
            else
            {
                objectToSwap[i].SetActive(false );
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton btn in tabButtons)
        {
            if(tabSelected!=null && btn == tabSelected) { continue; }
            btn.background.sprite = tabIdle;
        }
    }
}
