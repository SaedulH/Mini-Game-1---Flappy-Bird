using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public float deadzone = -45;
    public float pipeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * pipeSpeed) * Time.deltaTime;

        if (transform.position.x < deadzone)
        {
            Debug.Log("Pipe Deleted");
            Destroy(gameObject);
        }
    }
}
