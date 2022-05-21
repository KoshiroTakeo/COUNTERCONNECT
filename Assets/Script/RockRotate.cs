using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRotate : MonoBehaviour
{
    Vector3 random;
    
    // Start is called before the first frame update
    void Start()
    {
        random = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30));
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(random * Time.deltaTime, Space.World);
        
    }
}
