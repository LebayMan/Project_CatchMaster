using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public bool isGoodBall = true;
    public int scoreValue = 1;
    public float spinSpeed = 200f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.angularVelocity = Random.onUnitSphere * spinSpeed * Mathf.Deg2Rad;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Catcher"))
        {
            //GameManager.Instance.AddScore(isGoodBall ? scoreValue : -Mathf.Abs(scoreValue));
            Destroy(gameObject);
        }
        else if (other.CompareTag("Bottom"))
        {
            Destroy(gameObject);
        }
    }
}
