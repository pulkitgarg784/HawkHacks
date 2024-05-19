using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class playerHealth : MonoBehaviour
{
	public float health = 5;

	[SerializeField] private float knockBackPwr;
	public Text healthtext;

	void Start()
    {
		healthtext.text = "Health: " + health;
    }
    
	void Die()
	{
		if (health==0)
		{
			Debug.Log("Died");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
	public void DeductHealth(float dmg)
	{
		health -= dmg;
		gameObject.GetComponent<Animation>().Play("Damage");
		healthtext.text = "Health: " + health;
		
		//transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		//transform.position = new Vector3(0, 0, 0);

		Die();
	}
	public IEnumerator KnockBack(float knockDur,Vector3 knockBackDir)
	{
		float timer = 0;
		transform.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.GetComponent<Rigidbody2D>().velocity.x, 0);
		while (knockDur>timer)
		{
			timer += Time.deltaTime;
			transform.GetComponent<Rigidbody2D>().AddForce(new Vector3(knockBackDir.x -200 , knockBackDir.y + knockBackPwr, transform.position.z));

		}
		yield return 0;
	}
}
