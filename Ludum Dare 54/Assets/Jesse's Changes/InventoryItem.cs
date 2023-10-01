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
    public void Start() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void OnMouseDown() {
        //Debug.Log("clicked");
        if (canBeClicked) {
            clicked = true;
            gameObject.tag = "Untagged";
            if (grid != null) {
                grid.items.Remove(gameObject);
                Debug.Log("Item removed: " + gameObject.name);
            }
        }
        startPosition = transform.position;
    }

    public void OnMouseUp() {
        clicked = false;
        if (closestInventory != null) {
            
            bool occupied = false;
            GameObject[] gos = GameObject.FindGameObjectsWithTag("InInventory");

            float scale = ((closestInventory.transform.localScale.x)/10)*1.6f;
            Vector2 cellCoords = new Vector2(
                Mathf.Clamp(grid.getGridInt().x,0,grid.width-1),
                Mathf.Clamp(grid.getGridInt().y,0,grid.height-1)
            )*scale;
            Vector3 newPosition = new Vector3(closestInventory.transform.position.x+cellCoords.x+(scale/2),closestInventory.transform.position.y+cellCoords.y+(scale/2),transform.position.z);

            foreach(GameObject go in gos) {
                if (Vector3.Distance(go.transform.position,newPosition) < 0.1) {
                    occupied = true;
                }
            }

            if (occupied == false) {
                transform.position = newPosition;
                transform.SetParent(closestInventory.transform,true);
                gameObject.tag = "InInventory";
                grid.items.Add(gameObject);
                Debug.Log("Item added: " + gameObject.name);
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
    }

    void Update()
    {
        if (clicked) {
            transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x,cam.ScreenToWorldPoint(Input.mousePosition).y,transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "Grid") {
            //Debug.Log("Inventory enter");
            closestInventory = col.gameObject;
            grid = closestInventory.GetComponent<GridSystem>();
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.name == "Grid") {
            closestInventory = null; 
            //Debug.Log("Inventory exit");
        }
    }
}
