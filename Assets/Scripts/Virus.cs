using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public Material defaultMaterial;
    public Material activeMaterial;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        active = true;
        GetComponent<Renderer>().material = activeMaterial;
    }

    public void Deactivate()
    {
        active = false;
        GetComponent<Renderer>().material = defaultMaterial;
    }
}
