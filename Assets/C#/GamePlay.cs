using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public GameObject elementObj, panel;
    public Text winOrLoseText, scoreText, bestScore;
    public static int score;
    private static bool generateNew;
    public static object[,] sceneElem = new object[4, 4];
    private Vector2 startPos, endPos, direction;
    void Start()
    {
        sceneElem = new object[4, 4];
        generateNew = true;
        score = 0;
        Download();
        if (score == 0)
            GenerateElem();
    }
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endPos = Input.GetTouch(0).position;
            direction = endPos - startPos;
            if (!panel.activeSelf && (Mathf.Abs(direction.x) > 100 || Mathf.Abs(direction.y) > 100))
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0)
                    {
                        for (int k = 0; k < sceneElem.GetLength(0); k++)
                        {
                            for (int i = sceneElem.GetLength(0) - 1; i >= 0; i--)
                            {
                                for (int j = 0; j < sceneElem.GetLength(1); j++)
                                {
                                    if (i != sceneElem.GetLength(0) - 1)
                                    {
                                        AA(i, j, 0, 1);
                                    }
                                }
                            }
                        }
                        GenerateElem();
                    }
                    else
                    {
                        for (int k = 0; k < sceneElem.GetLength(0); k++)
                        {
                            for (int i = 0; i < sceneElem.GetLength(0); i++)
                            {
                                for (int j = 0; j < sceneElem.GetLength(1); j++)
                                {
                                    if (i != 0)
                                    {
                                        AA(i, j, 0, -1);
                                    }
                                }
                            }
                        }
                        GenerateElem();
                    }
                }
                else
                {
                    if (direction.y > 0)
                    {
                        for (int k = 0; k < sceneElem.GetLength(0); k++)
                        {
                            for (int i = 0; i < sceneElem.GetLength(0); i++)
                            {
                                for (int j = sceneElem.GetLength(1) - 1; j >= 0; j--)
                                {
                                    if (j != sceneElem.GetLength(1) - 1)
                                    {
                                        AA(i, j, 1, 0);
                                    }
                                }
                            }
                        }
                        GenerateElem();
                    }
                    else
                    {
                        for (int k = 0; k < sceneElem.GetLength(0); k++)
                        {
                            for (int i = 0; i < sceneElem.GetLength(0); i++)
                            {
                                for (int j = 0; j < sceneElem.GetLength(1); j++)
                                {
                                    if (j != 0)
                                    {
                                        AA(i, j, -1, 0);
                                    }
                                }
                            }
                        }
                        GenerateElem();
                    }
                }
            }
        }
        if (!panel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                for (int k = 0; k < sceneElem.GetLength(0); k++)
                {
                    for (int i = 0; i < sceneElem.GetLength(0); i++)
                    {
                        for (int j = sceneElem.GetLength(1) - 1; j >= 0; j--)
                        {
                            if (j != sceneElem.GetLength(1) - 1)
                            {
                                AA(i, j, 1, 0);
                            }
                        }
                    }
                }
                GenerateElem();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                for (int k = 0; k < sceneElem.GetLength(0); k++)
                {
                    for (int i = 0; i < sceneElem.GetLength(0); i++)
                    {
                        for (int j = 0; j < sceneElem.GetLength(1); j++)
                        {
                            if (j != 0)
                            {
                                AA(i, j, -1, 0);
                            }
                        }
                    }
                }
                GenerateElem();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                for (int k = 0; k < sceneElem.GetLength(0); k++)
                {
                    for (int i = sceneElem.GetLength(0) - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < sceneElem.GetLength(1); j++)
                        {
                            if (i != sceneElem.GetLength(0) - 1)
                            {
                                AA(i, j, 0, 1);
                            }
                        }
                    }
                }
                GenerateElem();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                for (int k = 0; k < sceneElem.GetLength(0); k++)
                {
                    for (int i = 0; i < sceneElem.GetLength(0); i++)
                    {
                        for (int j = 0; j < sceneElem.GetLength(1); j++)
                        {
                            if (i != 0)
                            {
                                AA(i, j, 0, -1);
                            }
                        }
                    }
                }
                GenerateElem();
            }
        }
    }
    bool IsElement(int x, int y)
    {
        if (sceneElem[x, y] is Element)
            return true;
        return false;
    }
    Element AsElement(int x, int y)
    {
        return sceneElem[x, y] as Element;
    }
    Vector2 ReturnNewObjPos(int x, int y)
    {
        return new Vector2(x * 190, y * 190);
    }
    GameObject ReturnNewGameObject(Vector2 position)
    {
        GameObject obj = Instantiate(elementObj, position, Quaternion.identity);
        obj.transform.SetParent(GameObject.Find("GameObjs").transform);
        obj.transform.localPosition = position;
        obj.transform.localScale = obj.transform.lossyScale;
        return obj;
    }
    int RerurnRandomPos()
    {
        return Random.Range(0, sceneElem.GetLength(0));
    }
    void GenerateElem()
    {
        if (generateNew && !IsFullMatrix())
        {
            while (true)
            {
                int x = RerurnRandomPos();
                int y = RerurnRandomPos();
                if (!IsElement(x, y))
                {
                    GameObject obj = ReturnNewGameObject(ReturnNewObjPos(x, y));
                    sceneElem[x, y] = new Element(obj, obj.transform.localPosition, 2);
                    generateNew = false;
                    scoreText.text = score.ToString();
                    if(System.Int32.Parse(bestScore.text) < score)
                    {
                        bestScore.text = score.ToString();
                    }
                    break;
                }
            }            
        }
        if (winOrLose())
        {
            winOrLoseText.text = "You lose!!!\nTry Again";
            winOrLoseText.gameObject.SetActive(true);
            panel.SetActive(true);
            BestRecordSaving();
        }
    }
    bool IsFullMatrix()
    {
        int count = 0;
        for (int i = 0; i < sceneElem.GetLength(0); i++)
        {
            for (int j = 0; j < sceneElem.GetLength(1); j++)
            {
                if (IsElement(i, j))
                {
                    count++;
                    if (AsElement(i, j).number == 2048)
                    {
                        winOrLoseText.text = "You WIN!!!";
                        winOrLoseText.gameObject.SetActive(true);
                        panel.SetActive(true);
                        BestRecordSaving();
                    }
                }
                if (sceneElem.GetLength(0) * sceneElem.GetLength(1) == count)
                {
                    return true;
                }
            }
        }
        return false;
    }
    void AA(int i, int j, int stepForUpDown, int stepForLeftRight)
    {
        if (IsElement(i, j))
        {
            if (!IsElement(i + stepForLeftRight, j + stepForUpDown))
            {
                sceneElem[i + stepForLeftRight, j + stepForUpDown] = sceneElem[i, j];
                sceneElem[i, j] = null;
                AsElement(i + stepForLeftRight, j + stepForUpDown).gameObj.transform.localPosition
                    = ReturnNewObjPos(i + stepForLeftRight, j + stepForUpDown);
                generateNew = true;
            }
            else
            {
                if (IsElement(i + stepForLeftRight, j + stepForUpDown) && IsElement(i, j))
                {
                    if (AsElement(i + stepForLeftRight, j + stepForUpDown).number == AsElement(i, j).number)
                    {
                        Destroy(AsElement(i + stepForLeftRight, j + stepForUpDown).gameObj);
                        GameObject obj = ReturnNewGameObject(ReturnNewObjPos(i + stepForLeftRight, j + stepForUpDown));
                        sceneElem[i + stepForLeftRight, j + stepForUpDown]
                            = new Element(obj, obj.transform.localPosition, AsElement(i, j).number * 2);
                        AsElement(i + stepForLeftRight, j + stepForUpDown).gameObj.transform.localPosition
                            = ReturnNewObjPos(i + stepForLeftRight, j + stepForUpDown);
                        Destroy(AsElement(i, j).gameObj);
                        sceneElem[i, j] = null;
                        generateNew = true;
                    }
                }
            }
        }
    }
    bool winOrLose()
    {
        if (IsFullMatrix())
        {
            for (int i = 0; i < sceneElem.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < sceneElem.GetLength(1) - 1; j++)
                {
                    if (AsElement(i, j).number == AsElement(i + 1, j).number)
                    {
                        return false;
                    }
                    if (AsElement(j, i).number == AsElement(j, i + 1).number)
                    {
                        return false;
                    }
                }
            }
            for (int i = sceneElem.GetLength(0) - 1; i > 0; i--)
            {
                for (int j = sceneElem.GetLength(1) - 1; j > 0; j--)
                {
                    if (AsElement(i, j).number == AsElement(i - 1, j).number)
                    {
                        return false;
                    }
                    if (AsElement(j, i).number == AsElement(j, i - 1).number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }
    public void Saving()
    {
        for (int i = 0; i < sceneElem.GetLength(0); i++)
        {
            for (int j = 0; j < sceneElem.GetLength(1); j++)
            {
                if (IsElement(i, j))
                {
                     PlayerPrefs.SetInt(i + ", " + j, AsElement(i, j).number);
                }
            }
        }
        PlayerPrefs.SetString("score", scoreText.text);
        BestRecordSaving();
    }
    void Download()
    {
        for (int i = 0; i < sceneElem.GetLength(0); i++)
        {
            for (int j = 0; j < sceneElem.GetLength(1); j++)
            {
                if (PlayerPrefs.HasKey(i + ", " + j))
                {
                    int playerPrefs = PlayerPrefs.GetInt(i + ", " + j);
                    GameObject obj = ReturnNewGameObject(ReturnNewObjPos(i, j));
                    sceneElem[i, j] = new Element(obj, ReturnNewObjPos(i, j), playerPrefs);
                    PlayerPrefs.DeleteKey(i + ", " + j);
                }
            }
        }
        if (PlayerPrefs.HasKey("score"))
        {
            scoreText.text = PlayerPrefs.GetString("score");
            PlayerPrefs.DeleteKey("score");
        }
        else scoreText.text = "0";
        score = System.Convert.ToInt32(scoreText.text);
        if (PlayerPrefs.HasKey("best"))
        {
            bestScore.text = PlayerPrefs.GetString("best");
        }
    }
    void BestRecordSaving()
    {
        PlayerPrefs.SetString("best", bestScore.text);
        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        Saving();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Saving();
        }
    }
}