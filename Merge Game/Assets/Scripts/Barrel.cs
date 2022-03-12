using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public int tier;

    // Start is called before the first frame update
    void Start()
    {
        SetBarrel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBarrel()
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.gameManager.barrel_sprites[tier];
    }
}
