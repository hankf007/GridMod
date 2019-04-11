using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{

    public int type;
    public Color[] tileColors;

    public bool inSlide = false;

    public Vector3 startPosition;
    public Vector3 destPosition;

    

    // Start is called before the first frame update
    void Start()
    {
      if(GetComponent<Player>()){
           type = -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (inSlide)
        {
            if (GridMaker.slideLerp < 0)
            {
                transform.localPosition = destPosition;
                inSlide = false;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(startPosition, destPosition, GridMaker.slideLerp);
            }
        }



   
    }



    public void SetType(int i)
    {
        type = i;

        GetComponent<SpriteRenderer>().color = tileColors[type];

        
    }
        
    public bool isMatch(GameObject gameObject1, GameObject gameObject2)
    {
        Tiles ts1 = gameObject1.GetComponent<Tiles>();  
        Tiles ts2 = gameObject2.GetComponent<Tiles>();
        return ts1!=null && ts2!=null && type == ts1.type && type == ts2.type;
    }

    public void SetupSlide(Vector2 newDestPos)
    {
        inSlide = true;
        startPosition = transform.localPosition;
        destPosition = newDestPos;
    }

}
