using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MothershipController : MonoBehaviour
{
    public float hp = 500;
    public GameObject game_over_screen;

    void Update() {
        if (hp <= 0) {
            Debug.Log("Mothership Destroyed!");
            game_over_screen.SetActive(true);
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }
    }
}
