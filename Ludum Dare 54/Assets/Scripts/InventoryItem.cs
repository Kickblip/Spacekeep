using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    bool clicked = false;
    GameObject closestInventory = null;
    GridSystem grid = null;
    Camera cam;
    bool canBeClicked = true;
    Vector3 startPosition;
    bool hasHitItemCollider = false;
    GameObject hitItem = null;
    public string identifier = "";
    public void Start() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    //used to check if item will be out of inventory on edges
    [SerializeField] Vector2 sizing = new Vector2(1,1);

    public void OnMouseDown() {
        //Debug.Log("clicked");
        if (canBeClicked) {
            clicked = true;
            gameObject.tag = "Untagged";
            if (grid != null) {
                grid.items.Remove(gameObject);
                Debug.Log("Item removed: " + gameObject.name);
                onRemoved();
            }
        }
        startPosition = transform.position;
    }
    /*
        I think this whole inventory system would have been better if i used individual grid cells rather than one big one
        That way i wouldn't have to mess with coordinates that much and occupied cells would just be a mask array
        But maybe someday i'll get around to making it better
    */

    public void OnMouseUp() {
        clicked = false;
        if (closestInventory != null) {
            
            //bool occupied = false;
            GameObject[] gos = GameObject.FindGameObjectsWithTag("InInventory");

            float scale = ((closestInventory.transform.localScale.x)/10)*1.6f;
            Vector2 rawCoords = new Vector2(
                Mathf.Clamp(grid.getGridInt().x,0,grid.width-1),
                Mathf.Clamp(grid.getGridInt().y,0,grid.height-1)
            );

            Vector2 cellCoords = rawCoords*scale;
            Vector3 newPosition = new Vector3(closestInventory.transform.position.x+cellCoords.x+(scale/2),closestInventory.transform.position.y+cellCoords.y+(scale/2),transform.position.z);

            if (!hasHitItemCollider && (sizing.x+rawCoords.x <= grid.width) && (rawCoords.y >= sizing.y-1)) {
                transform.position = newPosition;
                transform.SetParent(closestInventory.transform,true);
                gameObject.tag = "InInventory";
                grid.items.Add(gameObject);
                Debug.Log("Item added: " + gameObject.name);
                onPlaced();
                return;
            }
        }
        //move object back to start position
        //only if code above doesn't return

        if (closestInventory != null) {
            transform.position = startPosition; 
        } else {
            transform.SetParent(null, true);
        }
        hasHitItemCollider = false;
    }

    public virtual void onPlaced() {}

    public virtual void onRemoved() {}

    void Update()
    {
        if (clicked) {
            transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x,cam.ScreenToWorldPoint(Input.mousePosition).y,transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Inventory") {
            Debug.Log("Enter: " + col.name);
            closestInventory = col.gameObject;
            grid = closestInventory.GetComponent<GridSystem>();
        } else if (col.tag == "InInventory") {
            hasHitItemCollider = true;
            Debug.Log("HIT ITEM: " + col.name);
            hitItem = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Inventory") {
            if (col.gameObject == closestInventory) {
                closestInventory = null; 
                Debug.Log("Exit: " + col.name);
            }
        } else if (col.tag == "InInventory") {
            if (col.gameObject == hitItem) {
                hasHitItemCollider = false;
                Debug.Log("OUT OF ITEM: " + col.name);
            }
        }
    }
}
