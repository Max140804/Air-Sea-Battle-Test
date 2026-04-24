using UnityEngine;

public class EnemyCrossDetector : MonoBehaviour
{
    [Header("Hearts UI (Assign in order)")]
    [SerializeField] GameObject[] hearts; // heart1, heart2, heart3

    private int enemiesCrossed = 0;
    private int heartsLost = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        enemiesCrossed++;

        Destroy(other.gameObject);

        if (enemiesCrossed % 2 == 0)
        {
            LoseHeart();
        }

        if (enemiesCrossed >= 6)
        {
            GameManager.Instance.isGameOver = true;
        }
    }

    void LoseHeart()
    {
        if (heartsLost >= hearts.Length) return;

        hearts[heartsLost].SetActive(false);
        heartsLost++;
    }
}