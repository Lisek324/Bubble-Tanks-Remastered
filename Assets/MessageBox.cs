using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MessageBox : MonoBehaviour
{
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private TextMeshProUGUI buttonText1;
    [SerializeField] private TextMeshProUGUI buttonText2;
    [SerializeField] private TextMeshProUGUI popText;

    public void Init(Transform canvas, string popupMessage, string b1text, string b2text, Action action, bool Btn2Active)
    {
        popText.text = popupMessage;
        buttonText1.text = b1text;
        buttonText2.text = b2text;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;

        if (Btn2Active) button2.gameObject.SetActive(true);
        else button2.gameObject.SetActive(false);

        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;
        ///Yes
        button1.onClick.AddListener(() =>
        {
            action();
            Destroy(gameObject);
        });
        ///No
        button2.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });
    }
}
