using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public GameObject barrelPrefab;
    public SpriteRenderer fence;

    public string[] barrel_names;
    public Sprite[] barrel_sprites;

    public int coins;

    void Awake()
    {
        gameManager = this;
    }

    void Start()
    {
        SpawnBarrel();
    }

    void Update()
    {

    }

    //TODO CHANGE THIS to spawn the barrels at the next available spot instead of randomly
    public void SpawnBarrel()
    {
        // Vector3 position = new Vector3(Random.Range(fence.bounds.extends.x - 15, (fence.bounds.extends.x * -1) + 15), Random.Range(fence.bounds.extends.y - 25, (fence.bounds.extends.y * -1) + 25), 0);
        // Instantiate(barrelPrefab, position, Quaternion.identity, null);
    }
}
 