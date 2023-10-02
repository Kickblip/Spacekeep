using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MothershipController : MonoBehaviour
{
    public float hp = 500;

    void Update() {
        if (hp <= 0) {
            Debug.Log("Mothership Destroyed!");
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }
    }
}
