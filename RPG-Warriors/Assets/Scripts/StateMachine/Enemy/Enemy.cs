using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D EnemyRigidbody { get; private set; }
    public Animator EnemyAnimator { get; private set; }

    public EnemyStateMachine EnemyStMachine { get; private set; }

    private void Awake()
    {
        EnemyStMachine = new EnemyStateMachine();
    }

    private void Update()
    {
        EnemyStMachine._currentEnemyState.Update();
    }
}
