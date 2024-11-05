using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Health = 100;

    void Update()
    {
        if (Health <= 0)
            Destroy(gameObject);
    }
}
