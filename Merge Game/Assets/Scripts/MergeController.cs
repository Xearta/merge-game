using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeController : MonoBehaviour
{
    // List of images that represent the prefab for each level of merge
    [SerializeField] List<Sprite> listOfSpriteImages = new List<Sprite>();
    [SerializeField] LayerMask layer;

    public int prefabLevel = 0;
    public GameObject parentTile;

    private GameObject currentRaycastObject;
    private Vector3 currentPrefabPosition;
    private Vector3 newPrefabPosition;
    private Camera cam;
    private RaycastHit2D hit2d;
    private MergeSpawnManager manager;

    private void Awake()
    {
        cam = Camera.main; // Get the main camera from the scene
        manager = FindObjectOfType<MergeSpawnManager>(); // Find the manager in the level
    }

    private void OnMouseDown()
    {
        currentPrefabPosition = transform.position; // Get the current position of the prefab and set the variable to that value
        gameObject.layer = 0; // Set the crate layer to default
    }

    //! The crates can be hard to drag on mobile - box collider
    private void OnMouseDrag() 
    {
        // Move the object in the x,y position (Leave the Z position alone)
        transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
    }

    private void OnMouseUp() 
    {
        hit2d = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), cam.transform.forward, 5f, layer);

        // if you collide with another collidable object (crate or platform)
        if (hit2d)
        {
            // Get the new position
            // newPrefabPosition = hit2d.collider.transform.position;
            newPrefabPosition = new Vector2(hit2d.collider.transform.position.x, hit2d.collider.transform.position.y + 0.8f);
            currentRaycastObject = hit2d.collider.gameObject;

            // layer 6 => platform
            if (currentRaycastObject.layer == 6)
            {
                // Move the crate to the new tile and set the previous tile as available
                transform.position = newPrefabPosition;
                gameObject.layer = 7;
                parentTile.layer = 6;
                manager.listOfAvailableTiles.Add(parentTile);
                manager.listOfAvailableTiles.Remove(currentRaycastObject);
                parentTile = currentRaycastObject;
                currentRaycastObject.layer = 0;
            } 
            // layer 7 => crate && same crate level
            else if (currentRaycastObject.layer == 7 && currentRaycastObject.GetComponent<MergeController>().prefabLevel == prefabLevel)
            {
                // Check if it is at the last crate
                if (currentRaycastObject.GetComponent<MergeController>().prefabLevel >= listOfSpriteImages.Count-1)
                {
                    OnNullResult();
                    return;
                }

                // Increase the crate level and sprite, re-add the tile as available and destroy the game object
                currentRaycastObject.GetComponent<MergeController>().prefabLevel++;
                currentRaycastObject.GetComponent<SpriteRenderer>().sprite = listOfSpriteImages[currentRaycastObject.GetComponent<MergeController>().prefabLevel];
                manager.listOfAvailableTiles.Add(parentTile);
                parentTile.layer = 6;
                Destroy(gameObject);
            }
            else
            {
                OnNullResult();    
            }

        } else
        {
            OnNullResult();
        }
    }

    // Don't allow movement if we have no valid location to move to
    private void OnNullResult()
    {
        transform.position = currentPrefabPosition;
        gameObject.layer = 7;
    }
}
