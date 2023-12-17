using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TankEditor : MonoBehaviour
{
    public static List<PartData> objectsInEditor = new List<PartData>();
    private JSONSaving jsonSaving;
    private GameObject myGameObject;
    private RectTransform point;
    private RectTransform container;
    public Canvas canvas;
    private GameManager gameManager;
    MessageBox msg;
    Action action;
    int i = 0;

    const int PLAYER_LAYER = 7;
    private void Start()
    {
        gameManager = GameManager.gameManager;
        jsonSaving = JSONSaving.jsonSaving;
        point = Dragger.builder;
        container = Dragger.container;
    }
    public void JSONSave()
    {
        objectsInEditor.Clear();
        msg = UIManager.Instance.CreateMessageBox();
        action = () => {
            foreach (Transform child in point)
            {
                if (child.gameObject.name.Contains("(Base)"))
                {
                    child.gameObject.name = child.name.Replace("(Base)", "");
                }
                RectTransform childRect = child.GetComponent<RectTransform>();
                objectsInEditor.Add(new PartData(
                    child.gameObject.name,
                    child.transform.localPosition.x,
                    child.transform.localPosition.y,
                    childRect.sizeDelta.x,
                    childRect.sizeDelta.y,
                    childRect.localScale.x,
                    childRect.localScale.y,
                    child.GetComponent<Part>().partCost));
            }
            jsonSaving.SaveData();
        };
        msg.Init(UIManager.Instance.mainCanvas,
        "Your tank design has been saved",
        "OK",
        "OK",
        action,
        false);
    }
    public void Save()
    {
        PlayerController.playerController.turrets.Clear();
        PlayerController.player.transform.rotation = Quaternion.identity;
        foreach (Transform child in PlayerController.player.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in point)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle((RectTransform)canvas.transform, child.transform.position, canvas.worldCamera, out Vector3 pos);
            if (child.gameObject.tag == "Hull")
            {
                myGameObject = Instantiate(Resources.Load(@"Prefabs\Hull\" + child.gameObject.name, typeof(GameObject)), pos, child.transform.rotation) as GameObject;
            }
            else
            {
                myGameObject = Instantiate(Resources.Load(@"Prefabs\Weapons\Player\" + child.gameObject.name, typeof(GameObject)), pos, child.transform.rotation) as GameObject;
                //SIDENOTE: get weapons by their tag please
                PlayerController.playerController.turrets.Add(myGameObject);
            }
            myGameObject.transform.localScale = child.transform.localScale;
            myGameObject.transform.SetParent(PlayerController.player.transform);
            myGameObject.GetComponent<SpriteRenderer>().sortingOrder = i;
            myGameObject.layer = PLAYER_LAYER;
            i++;
        }
        i = 0;   
    }
    public void Load()
    {
        
        msg = UIManager.Instance.CreateMessageBox();
        action = () =>
        {
            foreach (Transform child in point)
            {
                gameManager.bubbles += child.GetComponent<Part>().partCost;
                Destroy(child.gameObject);
            }
            jsonSaving.LoadData();
            Dragger.allSelectable.Clear();
            Dragger.currentlySelected.Clear();
            for (int i = 0; i < jsonSaving.loadedData.Count; i++)
            {
                GameObject item = Instantiate(new GameObject(jsonSaving.loadedData[i].name));
                item.name = item.name.Replace("(Clone)", "");
                item.transform.SetParent(point);
                item.transform.localPosition = new Vector2(jsonSaving.loadedData[i].xPos, jsonSaving.loadedData[i].yPos);
                item.transform.localScale = new Vector3(jsonSaving.loadedData[i].xScale, jsonSaving.loadedData[i].xScale, 1);
                ///To make Loading from JSON easier, all gameobjects should share the same name with sprites, to make reconstruction possible
                //TODO: make an if statement,so script would know from which folder should take sprites (hull,weapon etc.), or dump everything in one folder.
                if (item.name.Contains("(Base)")) item.AddComponent<Image>().sprite = Resources.Load<Sprite>(@"Images/Hull/" + jsonSaving.loadedData[i].name.Replace("(Base)", ""));
                else item.AddComponent<Image>().sprite = Resources.Load<Sprite>(@"Images/Hull/" + jsonSaving.loadedData[i].name);

                item.AddComponent<Dragger>();
                item.AddComponent<Part>().partCost = jsonSaving.loadedData[i].cost;
                item.GetComponent<RectTransform>().sizeDelta = new Vector2(jsonSaving.loadedData[i].rWidth, jsonSaving.loadedData[i].rHeigth);
                item.tag = "Hull";
                item.layer = PLAYER_LAYER;

                GameObject selectionBox = Instantiate(new GameObject("SelectionBox"));
                selectionBox.AddComponent<Image>().sprite = Resources.Load<Sprite>(@"Images/SelectionBorder");
                selectionBox.GetComponent<Image>().type = Image.Type.Sliced;
                selectionBox.transform.SetParent(item.transform);
                selectionBox.name = selectionBox.name.Replace("(Clone)", "");
                selectionBox.GetComponent<RectTransform>().sizeDelta = new Vector2(jsonSaving.loadedData[i].rWidth, jsonSaving.loadedData[i].rHeigth);
                selectionBox.transform.localScale = new Vector3(jsonSaving.loadedData[i].xScale, jsonSaving.loadedData[i].xScale, 1);
                selectionBox.transform.localPosition = new Vector2(0, 0);
                selectionBox.layer = PLAYER_LAYER;
                selectionBox.SetActive(false);

                Dragger.allSelectable.Add(item.GetComponent<Dragger>());
                Dragger.currentlySelected.Add(item.GetComponent<Dragger>());

                gameManager.bubbles -= jsonSaving.loadedData[i].cost;
            }
            gameManager.scoreText.text = "Bubbles: "+gameManager.bubbles.ToString();
        };
        msg.Init(UIManager.Instance.mainCanvas,
        "Are you shure do you want to load tank design from JSON? This action will delete, and refund every part in editor",
        "Yes",
        "No",
        action,
        true);
    }
}
