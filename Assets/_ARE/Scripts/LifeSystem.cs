using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private AudioClip dyingSoundClip;

    public GameObject[] lifes;
    public float minimumFall = 2f;
    public float mediumFall = 10;
    public float fallToKill = 30f;
    private int currentLife;
    private int totalLife;

    private PlayerState _playerState;

    bool wasGrounded;
    bool wasFalling;
    float startOfFall;

    [HideInInspector] public float fallDistance;

    private void Start()
    {
        _playerState = GetComponent<PlayerState>();
        totalLife = lifes.Length;

        foreach(GameObject life in lifes)
        {
            if (life.activeSelf)
                currentLife++;
        }
    }

    private void Update()
    {
        bool isGrounded = _playerState.InGroundedState();


        bool isFalling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling;

        if (!wasFalling && isFalling)
        {
            startOfFall = transform.position.y;
        }

        if (!wasGrounded && isGrounded)
        {
            fallDistance = startOfFall - transform.position.y;

            if (fallDistance > minimumFall && fallDistance < mediumFall)
                TakeDamage(1);
            else if (fallDistance > mediumFall && fallDistance < fallToKill)
                TakeDamage(2);
            else if (fallDistance > fallToKill)
                TakeDamage(4);

            Debug.Log("Player fell " + fallDistance + " units");
        }

        wasGrounded = isGrounded;
        wasFalling = isFalling;
    }

    public void TakeDamage(int d)
    {
        Debug.Log(currentLife);
        for (int i = 1; i <= d; d--)
        {
            if (currentLife - i == 0)
            {
                SoundFXManager.instance.PlaySoundFXClip(dyingSoundClip, transform, 1f);
                currentLife -= i;
                lifes[currentLife].SetActive(false);
                return;
            }

            else if (currentLife <= 0)
                return;

            currentLife -= i;
            lifes[currentLife].SetActive(false);
        }
    }

    public void RecoverLife(int r)
    {
        for (int i = 1; i <= r; r--)
        {
            if (currentLife + i > totalLife)
                return;

            lifes[currentLife].SetActive(true);
            currentLife += i;
        }
    }
}
