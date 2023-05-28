using UnityEngine;

public class TrapManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.instace.UpdateScore();
        }
    }
}
