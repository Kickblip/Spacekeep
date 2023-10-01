using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    bool clicked = false;
    GameObject closestInventory = null;
    Camera cam;
    bool canBeClicked = true;
    Vector3 startPosition;
    public void Start() {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void OnMouseDown() {
        Debug.Log("clicked");
        if (canBeClicked) {
            clicked = true;
        }
        startPosition = transform.position;
    }

    public void OnMouseUp() {
        //Debug.Log("unclicked");
        clicked = false;
        if (closestInventory != null) {
            float scale = ((closestInventory.transform.localScale.x)/10)*1.6f;
            Vector2 cellCoords = closestInventory.GetComponent<GridSystem>().getGridInt()*scale;
            transform.position = new Vector3(closestInventory.transform.position.x+cellCoords.x+(scale/2),closestInventory.transform.position.y+cellCoords.y+(scale/2),transform.position.z);
            transform.SetParent(closestInventory.transform,true);
            //Debug.Log(closestInventory.GetComponent<GridSystem>().getGridInt());
        } else {//move object back to start position
            if (transform.parent == null) {
                transform.position = startPosition;
            }
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
            Debug.Log("Inventory enter");
            closestInventory = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.name == "Grid") {
            closestInventory = null; 
            Debug.Log("Inventory exit");
        }
    }
}
