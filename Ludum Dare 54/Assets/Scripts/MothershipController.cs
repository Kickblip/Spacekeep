using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MothershipController : MonoBehaviour
{
    public float hp = 1000;

    void Update() {
        if (hp <= 0) {
            Debug.Log("Mothership Destroyed!");
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }
    }
}
