using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chestTrigger : MonoBehaviour
{
    public Animator animator;
    public bool isOpened;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("WOOHOO. I FOUND THE PLAYER");
            isOpened = true;
            animator.SetBool("IsOpened", isOpened);
            Invoke("NextLevel", 2f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("WOMP WOMP. I LOST THE PLAYER");
            isOpened = false;
            animator.SetBool("IsOpened", isOpened);
        }
    }
    public void NextLevel()

    {
        SceneManager.LoadScene("You Won");
    }
}
