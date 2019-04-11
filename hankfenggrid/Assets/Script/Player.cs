﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Player: MonoBehaviour


{
    public GridMaker gridmaker;

    public Vector2 playerPosition;

    public int moves;

    public Text moveText;



   



    // Start is called before the first frame update
    void Start()
    {
        gridmaker = GameObject.Find("GridMaker").GetComponent<GridMaker>();

        moves = 6;
       


    }

    // Update is called once per frame
    void Update()
    {

        moveText.text = "" + moves;
        Debug.Log("" + moves);


        if (moves == 0)
        {
            SceneManager.LoadScene("End");
        }


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
           

            Swap(0, -1);

            if (gridmaker.HasMatch())
            { //reset moves to 6
                moves = 6;
            }
            else
            {
                moves--;
                
            }
        }


        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {

           

            Swap(0, 1);

            if (gridmaker.HasMatch())
            {
                moves = 6;
            }
            else
            {
                moves--;
                
            }
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Swap(1, 0);

            if (gridmaker.HasMatch())
            {
                moves = 6;
            }
            else
            {
                moves--;
                
            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Swap(-1, 0);

            if (gridmaker.HasMatch())
            {
                moves = 6;
            }
            else
            {
                moves--;
                
            }

        }
    }


    public void Swap(int x, int y)
    {
        Vector2 oldLocation = new Vector2(playerPosition.x, playerPosition.y);
        Vector2 newLocation = new Vector2(playerPosition.x + x, playerPosition.y + y);


        if((int)newLocation.x<GridMaker.WIDTH &&
            (int)newLocation.x>=0&&
            (int)newLocation.y<GridMaker.HEIGHT&&
            (int)newLocation.y >= 0)
        {
            GameObject swapTile = gridmaker.tiles[(int)newLocation.x, (int)newLocation.y];
        
            Vector3 swapPosition = swapTile.transform.localPosition;

            swapTile.transform.localPosition = transform.localPosition;
            transform.localPosition = swapPosition;

            gridmaker.tiles[(int)oldLocation.x, (int)oldLocation.y] = swapTile;
            gridmaker.tiles[(int)newLocation.x, (int)newLocation.y] = gameObject;

            playerPosition = newLocation;



        }


    }


 
}
