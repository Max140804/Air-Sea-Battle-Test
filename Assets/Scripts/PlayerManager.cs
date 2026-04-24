using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("Gun States")]
    [SerializeField] GameObject player_30;
    [SerializeField] GameObject player_60;
    [SerializeField] GameObject player_90;

    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int maxBullets = 5;

    Vector2 currentDirection;

    GameObject currentGun;

    List<GameObject> activeBullets = new List<GameObject>();

    void Start()
    {
        SetAngle60();
    }

    void Update()
    {
        if(GameManager.Instance.isGameOver) return;

        HandleAngleInput();
        HandleFireInput();
    }

    void HandleAngleInput()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            SetAngle30();
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            SetAngle90();
        }
        else
        {
            SetAngle60();
        }
    }

    void HandleFireInput()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        if (activeBullets.Count >= maxBullets) return;

        Transform spawnPoint = GetCurrentSpawnPoint();

        GameObject bullet = Instantiate(
            bulletPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        activeBullets.Add(bullet);

        Bullet b = bullet.GetComponent<Bullet>();

        currentDirection = spawnPoint.up;
        b.SetDirection(currentDirection);
    }

    Transform GetCurrentSpawnPoint()
    {
        return currentGun.transform.Find("SpawnPoint");
    }

    void SetAngle30()
    {
        ActivateGun(player_30);
    }

    void SetAngle60()
    {
        ActivateGun(player_60);
    }

    void SetAngle90()
    {
        ActivateGun(player_90);
    }

    void ActivateGun(GameObject gun)
    {
        player_30.SetActive(false);
        player_60.SetActive(false);
        player_90.SetActive(false);

        gun.SetActive(true);
        currentGun = gun;
    }

    public void OnBulletDestroyed(GameObject bullet)
    {
        activeBullets.Remove(bullet);
    }
}