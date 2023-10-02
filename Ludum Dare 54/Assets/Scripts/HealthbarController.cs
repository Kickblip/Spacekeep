using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    Slider slider;
    [SerializeField] GameObject Mothership;
    MothershipController ctrl;
    void Start()
    {
        slider = GetComponent<Slider>();
        ctrl = Mothership.GetComponent<MothershipController>();
    }

    void Update()
    {
        if (Mothership != null) {
            slider.value = ctrl.hp;
        }
    }
}
