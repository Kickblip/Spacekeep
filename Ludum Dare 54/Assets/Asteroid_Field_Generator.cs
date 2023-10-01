using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid_Field_Generator : MonoBehaviour
{
    public GameObject[] asteroids;
    public float outer_boundry;
    public float inner_boundry;
    public float generation_density;

    private void Start()
    {
        float asteroid_count = generation_density * outer_boundry - inner_boundry;
        for (int i = 0; i < asteroid_count; i++)
        {
            bool good_gen_pos = false;
            while (!good_gen_pos)
            {
                Vector2 random_position = new Vector2(Random.Range(-outer_boundry, outer_boundry), Random.Range(-outer_boundry, outer_boundry));
                float distance_from_center = Mathf.Sqrt(Mathf.Pow(random_position.x, 2) + Mathf.Pow(random_position.y, 2));
                if (distance_from_center < outer_boundry && distance_from_center > inner_boundry)
                {
                    //select astroid type *0 index is temp*
                    Instantiate(asteroids[0], random_position, transform.rotation); // change to be random rotation
                    good_gen_pos = true;
                }
            }
        }
    }
}
