using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour{

    public int number;
    public GameObject gameObj;

    public Element(GameObject obj,Vector2 position,int number)
    {
        gameObj = obj;
        this.number = number;
        gameObj.GetComponent<Text>().text = number.ToString();
        GamePlay.score += number;
    }
}
