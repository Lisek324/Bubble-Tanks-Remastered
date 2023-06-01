using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class SearchScript : MonoBehaviour
{
    public List<GameObject> ContentHolder;
    public List<GameObject> Elements;
    public GameObject SearchBar;
    public int totalElements;
    
    void Start()
    {
        for (int i = 0; i < ContentHolder.Count; i++)
        {
            totalElements += ContentHolder[i].transform.childCount;
            for(int j = 0; j < ContentHolder[i].transform.childCount; j++)
            {
                Elements.Add(ContentHolder[i].transform.GetChild(j).gameObject);
            }
        }
    }

    public void Search()
    {
        string SearchText = SearchBar.GetComponent<TMP_InputField>().text;

        int searchedElements = 0;
        foreach (GameObject element in Elements)
        {
            searchedElements += 1;

            if (element.transform.GetChild(0).name.Length >= SearchText.Length)
            {
                if (SearchText.ToLower().Contains(element.transform.GetChild(0).name.Substring(0, SearchText.Length).ToLower()))
                {
                    element.SetActive(true);
                }
                else
                {
                    element.SetActive(false);
                }
            }
        }
    }
}
