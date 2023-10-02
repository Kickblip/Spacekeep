using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] public int width;
    [SerializeField] public int height;
    float tileSize = 0.16f;
    Camera cam;
    SpriteRenderer render;
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        render = GetComponent<SpriteRenderer>();
        render.size = new Vector2(width*tileSize,height*tileSize);

        Vector2 S = render.size;
        GetComponent<BoxCollider2D>().size = S;
        GetComponent<BoxCollider2D>().offset = new Vector2 ((S.x/2), (S.y/2));
    }

    //convert mouse coords to closest grid coordinates
    public Vector2 getGridInt() {
        Vector2 mp = cam.ScreenToWorldPoint(Input.mousePosition);

        float sizeCorrect = tileSize*transform.localScale.x;

        float posX = Mathf.Floor((mp.x - transform.position.x)/sizeCorrect);
        float posY = Mathf.Floor((mp.y - transform.position.y)/sizeCorrect);

        return new Vector2(posX,posY);
    }

    public void Update() {
        //Debug.Log(getGridInt());
    }
}