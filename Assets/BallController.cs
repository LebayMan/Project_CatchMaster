using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public bool isGoodBall = true;
    public int scoreValue = 1;
    public float spinSpeed = 200f;
    public SerialController serialController;

    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject catcher = GameObject.FindWithTag("Catcher");
        serialController = catcher.GetComponent<SerialController>();
        rb.useGravity = true;
        rb.angularVelocity = Random.onUnitSphere * spinSpeed * Mathf.Deg2Rad;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Catcher"))
        {
            GameManager.Instance.AddScore(isGoodBall ? scoreValue : -Mathf.Abs(scoreValue));
            if(isGoodBall)
            {
                serialController.CallScore();
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Bottom"))
        {
            Destroy(gameObject);
        }
    }
}
