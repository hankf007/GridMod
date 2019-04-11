using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridMaker : MonoBehaviour
{  
    public const int WIDTH = 5; 
    public const int HEIGHT = 7;

    float xOffset = WIDTH / 2f - 0.5f;
    float yOffset = HEIGHT / 2f - 0.5f;

    public GameObject[,] tiles;

    public GameObject tilePrefab;

    public GameObject playerPrefab;

    public Vector2 playerLocation;

    GameObject gridHolder;

    int score;
    public Text scoreText;


    


    public static float slideLerp = -1;
    public float lerpSpeed = 0.25f;

    public ParticleSystem particles;

    


    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[WIDTH, HEIGHT];

        gridHolder = new GameObject();
        gridHolder.transform.position = new Vector3(-1f, -0.5f, 0);

        for (int x = 0; x < WIDTH; x++)

        { for (int y = 0; y < HEIGHT; y++)
            {
                if (x == 2 && y == 3) //make the player in center
                {
                    GameObject player = Instantiate(playerPrefab);

                    player.transform.parent = gridHolder.transform;
                   
                    player.transform.localPosition = new Vector2(WIDTH - x - xOffset, HEIGHT - y - yOffset);

                    tiles[x, y] = player;

                    Vector2 playerLocation = new Vector2(WIDTH / 2, HEIGHT / 2);

                    player.GetComponent<Player>().playerPosition = playerLocation;

                   
                }
                else //popualte the grid
                {
                    GameObject newTile = Instantiate(tilePrefab);

                    newTile.transform.parent = gridHolder.transform;

                    newTile.transform.localPosition = new Vector2(WIDTH - x - xOffset, HEIGHT - y - yOffset);

                    tiles[x, y] = newTile;



                    Tiles tileScript = newTile.GetComponent<Tiles>();
                    tileScript.SetType(Random.Range(0, tileScript.tileColors.Length));
                    

                }
                
                
            }
        

        }
        //check for matches
        while (HasMatch())
        {
            startHasMatch();
        }


    }

    // Update is called once per frame
    void Update()
    {


        scoreText.text = "SCORE:" + score;



        if (slideLerp < 0 && !Repopulate() && HasMatch())
        {
          //  Debug.Log("match");
            RemoveMatches();
        }
        else if (slideLerp >= 0)
        {
          

            slideLerp += Time.deltaTime / lerpSpeed;

            if (slideLerp >= 1)
            {
                slideLerp = -1;
            } 
        }

        



    }

    public Tiles HasMatch()//check for matches
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                Tiles tileScript = tiles[x, y].GetComponent<Tiles>();

                if (tileScript != null)
                {
                    if (x < WIDTH - 2 && tileScript.isMatch(tiles[x + 1, y], tiles[x + 2, y]))
                    {
                       

                        return tileScript;
                    }
                    if (y < HEIGHT - 2 && tileScript.isMatch(tiles[x, y + 1], tiles[x, y + 2]))
                    {
                       

                        return tileScript;
                    }
                }
            }
        }
        return null;
    }

    public void startHasMatch()//check for matches before game start
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                Tiles tileScript = tiles[x, y].GetComponent<Tiles>();

                if (tileScript != null)
                {//set to another random color if matched
                    if (x < WIDTH - 2 && tileScript.isMatch(tiles[x + 1, y], tiles[x + 2, y]))
                    {
                        tileScript.SetType(Random.Range(0, tileScript.tileColors.Length));
                    }
                    if (y < HEIGHT - 2 && tileScript.isMatch(tiles[x, y + 1], tiles[x, y + 2]))
                    {
                        tileScript.SetType(Random.Range(0, tileScript.tileColors.Length));
                    }
                }
            }
        }
    }

    public void RemoveMatches()
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                Tiles tileScript = tiles[x, y].GetComponent<Tiles>();

                if (tileScript != null)
                { //matches 3 consecutive horizontal tiles
                    if (x < WIDTH - 2 && tileScript.isMatch(tiles[x + 1, y], tiles[x + 2, y]))
                    {
                        Instantiate(particles, tiles[x, y].transform.position, Quaternion.identity);
                        Instantiate(particles, tiles[x+1, y].transform.position, Quaternion.identity);
                        Instantiate(particles, tiles[x+2, y].transform.position, Quaternion.identity);

                        score += 3;

                        Destroy(tiles[x, y]);
                        Destroy(tiles[x + 1, y]);
                        Destroy(tiles[x + 2, y]);

                    }
                    if (y < HEIGHT - 2 && tileScript.isMatch(tiles[x, y + 1], tiles[x, y + 2]))
                    {//matches 3 consecutive vertical tiles
                        Instantiate(particles, tiles[x, y].transform.position, Quaternion.identity);
                        Instantiate(particles, tiles[x, y + 1].transform.position, Quaternion.identity);
                        Instantiate(particles, tiles[x, y + 2].transform.position, Quaternion.identity);

                        score += 3;

                        Destroy(tiles[x, y]);
                        Destroy(tiles[x, y + 1]);
                        Destroy(tiles[x, y + 2]);

                    }
                }
            }
        }


    }
    public bool Repopulate()
    {
        bool repop = false;

        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                if(tiles[x,y] == null)
                {
                    repop = true;

                    if (y == 0)
                    {
                       
                        tiles[x, y] = Instantiate(tilePrefab);

                        Tiles tileScript = tiles[x, y].GetComponent<Tiles>();

                        tileScript.SetType(Random.Range(0, tileScript.tileColors.Length));

                        tiles[x, y].transform.parent = gridHolder.transform;

                        tiles[x, y].transform.localPosition = new Vector2(WIDTH - x - xOffset,HEIGHT - y - yOffset);

                        

                    }
                    else
                    {
                        
                        slideLerp = 0;
                        tiles[x, y] = tiles[x, y - 1];
                        Tiles tileScript = tiles[x, y].GetComponent<Tiles>();

                        if (tileScript != null)
                        {
;                            tileScript.SetupSlide(new Vector2(WIDTH - x - xOffset, HEIGHT - y - yOffset));

                        }
                        Player playerScript = tiles[x, y].GetComponent<Player>();
                        if (playerScript != null)
                        {
                            playerScript.playerPosition.Set(x, y);
                        }

                        tiles[x, y - 1] = null;
                    }
                }
            }

        }
        return repop;
    }



}

