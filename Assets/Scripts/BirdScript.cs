using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public float flapStrength;
    public Rigidbody2D birdBody;
    public CircleCollider2D birdCollider;
    public LogicScript logic;
    public float birdRotation;

    public Animator birdWingsAnim;
    public Animator birdBodyAnim;
    public AudioSource FlapAudio;

    int[] directionArray = { -1, 1, 2 };
    int randomIndex;
    int direction;

    float pitchMax;
    float pitchMin;

    public bool birdIsAlive = true;
    // Start is called before the first frame update
    void Awake()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        randomIndex = Random.Range(0, 2);
        direction = directionArray[randomIndex];
        Debug.Log("direction = " + direction);
        pitchMax = FlapAudio.pitch + 0.1f;
        pitchMin = FlapAudio.pitch - 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (birdBody.rotation > -40)
        {
            transform.Rotate(new Vector3(0, 0, -1 * birdRotation * Time.deltaTime));
        }

        if ((Input.GetKeyDown(KeyCode.Space) == true || Input.GetMouseButtonDown(0) == true) && birdIsAlive)
        {
            BirdFlap();
        }

        if ((transform.position.y > 25 || transform.position.y < -25) && birdIsAlive)
        {
            logic.GameOver();
            BirdDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdIsAlive)
        {
            logic.GameOver();
            BirdDeath();

        }
    }

    public void BirdFlap()
    {
        birdBody.linearVelocity = Vector2.up * flapStrength;
        birdWingsAnim.SetTrigger("Flap");
        if (birdBody.rotation < 30)
        {
            transform.Rotate(Vector3.forward * 20);
        }

        FlapAudio.pitch = Random.Range(pitchMin, pitchMax);
        FlapAudio.Play();
    }

    public void BirdDeath()
    {
        birdIsAlive = false;
        birdCollider.enabled = false;
        birdBodyAnim.SetTrigger("Death");
        birdBody.linearVelocity = new Vector2(direction, 2) * flapStrength / 2f;
        birdBody.angularVelocity = -10 * flapStrength * direction;
    }
}
