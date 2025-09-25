using UnityEngine;

public class PipeSpawner : MonoBehaviour
{

    public GameObject pipes;

    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 10;
    private float stepSize = 2;

    // Start is called before the first frame update
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0;
        }
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        float point = Random.Range(lowestPoint, highestPoint);
        float pointStep = Mathf.Floor(point / stepSize);


        Instantiate(pipes, new Vector3(transform.position.x, pointStep * stepSize), transform.rotation);
    }
}
