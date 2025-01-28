using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject cude;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var obj = GameObject.Instantiate(cude);
            obj.transform.position = new Vector3(0, 10, -1);
            float r = Random.Range(0,1f);
            float g = Random.Range(0,1f);
            float b = Random.Range(0,1f);
            obj.GetComponent<Renderer>().material.color = new Color(r,g,b,1);
        }

    }
}
