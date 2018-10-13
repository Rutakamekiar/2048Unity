using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public GameObject elementObj;
    public static object[,] sceneElem;
    private static bool generateNew;
    private Vector2 startPos, endPos, direction;
    void Start()
    {
        generateNew = true;
        sceneElem = new object[4, 4];
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

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    for (int k = 0; k < sceneElem.GetLength(0); k++)
                    {
                        for (int i = 0; i < sceneElem.GetLength(0); i++)
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
                        for (int i = sceneElem.GetLength(0) - 1; i > 0; i--)
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
                for (int i = 0; i < sceneElem.GetLength(0); i++)
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
                for (int i = sceneElem.GetLength(0) - 1; i > 0; i--)
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

    bool IsElement(int x, int y)
    {
        //print(x + " " + y);
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
    void GenerateElem()
    {
        if (generateNew)
        {
            int x, y;
            while (true)
            {
                x = RerurnRandomPos();
                y = RerurnRandomPos();
                if (!(sceneElem[x, y] is Element))
                {
                    GameObject obj = ReturnNewGameObject(ReturnNewObjPos(x, y));
                    sceneElem[x, y] = new Element(obj, obj.transform.localPosition, 2);
                    generateNew = false;
                    break;
                }
                else if (IsFullMatrix())
                {
                    print("full mass");
                    generateNew = false;
                    break;
                }
            }
        }
    }
    bool IsFullMatrix()
    {
        int count = 0;
        for (int i = 0; i < sceneElem.GetLength(0); i++)
        {
            for (int j = 0; j < sceneElem.GetLength(1); j++)
            {
                if (sceneElem[i, j] is Element) count++;
                if (sceneElem.GetLength(0) * sceneElem.GetLength(1) == count)
                {
                    return true;
                }
            }
        }
        return false;
    }
    int RerurnRandomPos()
    {
        return Random.Range(0, 4);
    }
    GameObject ReturnNewGameObject(Vector2 position)
    {
        GameObject obj = Instantiate(elementObj, position, Quaternion.identity);
        obj.transform.SetParent(GameObject.Find("GameObjs").transform);
        obj.transform.localPosition = position;
        obj.transform.localScale = obj.transform.lossyScale;
        return obj;
    }
    void AA(int i, int j, int stepForUpDown, int stepForLeftRight)
    {
        if (IsElement(i, j))
        {
            if (!IsElement(i + stepForLeftRight, j + stepForUpDown)) //сверху нет елемента?
            {
                sceneElem[i + stepForLeftRight, j + stepForUpDown] = sceneElem[i, j];
                sceneElem[i, j] = null;
                AsElement(i + stepForLeftRight, j + stepForUpDown).gameObj.transform.localPosition
                    = ReturnNewObjPos(i + stepForLeftRight, j + stepForUpDown);
                generateNew = true;
            }
            else if (IsElement(i + stepForLeftRight, j + stepForUpDown) && IsElement(i, j))
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