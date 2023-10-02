using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarnController : MonoBehaviour
{
    [SerializeField] GameObject Mothership;
    MothershipController ctrl;
    float lasthp;
    float timer = 0;
    Image img;
    void Start() {
        ctrl = Mothership.GetComponent<MothershipController>();
        lasthp = ctrl.hp;
        img = GetComponent<Image>();
    }

    void Update() {
        if (Mothership != null) {
            if (lasthp != ctrl.hp) {
                timer = 5;
            }
            lasthp = ctrl.hp;
        }
        if (timer > 0) {
            timer -= 1 * Time.deltaTime;
            img.enabled = true;
            //get sin between values (super useful formula for all sorts of stuff)
            float max = 0.9f;
            float min = 0.5f;
            float sinVal = ((max-min)*Mathf.Sin(Time.time*7)+max+min)/2;
            transform.localScale = new Vector3(sinVal,sinVal,sinVal);
        } else {
            img.enabled = false;
        }
    }
}
