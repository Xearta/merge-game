using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeSpawnManager : MonoBehaviour
{
    [SerializeField] float cooldownTime;
    [SerializeField] Image cooldownBarImage;
    [SerializeField] GameObject prefabToSpawn;

    private float currentCooldownTime;
    private bool cooldown;
    public List<GameObject> listOfAvailableTiles = new List<GameObject>();
    private GameObject tileToSpawnOn;

    // Start is called before the first frame update
    void Start()
    {
        // Find all objects with 'platform' tag and add them to the list
        listOfAvailableTiles.AddRange(GameObject.FindGameObjectsWithTag("platform"));
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown && listOfAvailableTiles.Count != 0)
        {
            currentCooldownTime += Time.deltaTime;
            cooldownBarImage.fillAmount = currentCooldownTime / cooldownTime;

            // Reset system after cooldown has been completed
            if (currentCooldownTime >= cooldownTime)
            {
                cooldown = false;
                currentCooldownTime = 0;
                cooldownBarImage.fillAmount = currentCooldownTime;
            }
        } else
        {
            if (listOfAvailableTiles.Count != 0)
            {
                //! Spawn in order instead of radnomly
                // tileToSpawnOn = listOfAvailableTiles[Random.Range(0, listOfAvailableTiles.Count - 1)];
                tileToSpawnOn = listOfAvailableTiles[0];

                //! The crates are not correctly on the mouse cursor
                GameObject currentPrefab = Instantiate(prefabToSpawn, new Vector3(tileToSpawnOn.transform.position.x, tileToSpawnOn.transform.position.y + 0.8f, -0.1f), tileToSpawnOn.transform.rotation);


                listOfAvailableTiles.Remove(tileToSpawnOn);
                tileToSpawnOn.layer = 0;
                cooldown = true;
                currentPrefab.GetComponent<MergeController>().parentTile = tileToSpawnOn;
            }
        }
    }
}
