using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class pig : MonoBehaviour


{

    public Animator anim;

    public GridMaker gridmaker;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gridmaker = GameObject.Find("GridMaker").GetComponent<GridMaker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gridmaker.HasMatch())
        {
            Debug.Log("x");
            anim.SetBool("combo", true);

        }
    }
}
