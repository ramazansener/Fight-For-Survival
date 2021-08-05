using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody playerRB;
    private GameObject focalPoint;


    public bool hasPowerup;
    private float powerUpStrength=15.0f;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();


        focalPoint = GameObject.Find("Focal Point");// sahnede ismi Focal Point olan gameobjeyi buluyor
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxisRaw("Vertical"); // vertical yani yukarý ve aþaðý eksendeki hareketin inputunu alýyoruz

        playerRB.AddForce(focalPoint.transform.forward*forwardInput* speed);// burada karakterimizi focalPoint adlý nesnenin forward dýna göre hareket ettiriyoruz

        powerupIndicator.transform.position = gameObject.transform.position+ new Vector3(0,-.5f,0);
    }
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
		{
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Powerup"))
		{
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
		}
	}
    IEnumerator PowerUpCountdownRoutine() 
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

   
    
}
