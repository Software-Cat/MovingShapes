using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Ranger : Enemy
{
    [SerializeField] protected float optimalDistanceToPlayer = 10f;
    [SerializeField] protected float unsafeDistanceToPlayer = 4f;
    [SerializeField] protected float reselectionInterval = 3f;

    protected void Start()
    {
        StartCoroutine(ReselectGoal());
    }

    protected override void Update()
    {
        if (Vector3.Distance(transform.position, Registry.PLAYER.transform.position) < unsafeDistanceToPlayer)
        {
            goal = transform.position + (transform.position - Registry.PLAYER.transform.position).normalized * optimalDistanceToPlayer;
        }

        base.Update();
    }

    private IEnumerator ReselectGoal()
    {
        Vector3 player = Registry.PLAYER.transform.position;

        Vector3 pointNearPlayer = player + (Random.onUnitSphere * optimalDistanceToPlayer);

        goal = pointNearPlayer;

        yield return new WaitForSeconds(reselectionInterval);
        StartCoroutine(ReselectGoal());
    }
}
