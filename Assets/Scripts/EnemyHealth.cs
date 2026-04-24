using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public GameObject audioObj;

    private bool isDead = false;

    private void Start()
    {
       
        audioObj.SetActive(false);
    }

    public void TakeDamage()
    {
        if (isDead) return;
        Die();
       
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        GameManager.Instance.Score();

        audioObj.SetActive(true);

        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(gameObject, 0.2f);
    }
}