using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Attack behavior using laser type
/// </summary>
public class LaserAttack : AttackBehavior
{
    [SerializeField]
    Transform shootPoint;
    [SerializeField]
    LineRenderer lineRenderer;
    private void OnEnable()
    {
        lineRenderer.positionCount = 0;
    }
    private void OnDisable()
    {
        lineRenderer.positionCount = 0;
    }
    public override void Shoot(IDamageReceiver target)
    {
        StartCoroutine(ShootLaser(target));
    }


    IEnumerator ShootLaser(IDamageReceiver target)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, shootPoint.position);
        if (attackData.ProjectileType == ProjectileType.laser)
        {
            Vector3 direction = target.GameObject.transform.position - shootPoint.position;
            direction.Normalize();
            lineRenderer.SetPosition(1, shootPoint.position + direction * attackData.Range * 2);
            var hits = Physics2D.RaycastAll(shootPoint.position, direction, attackData.Range * 2);
            foreach(var hit in hits)
            {
                IDamageReceiver damageReceiver = hit.collider.gameObject.GetComponent<IDamageReceiver>();
                if (damageReceiver != null)
                    damageReceiver.TakeDamage(this,attackData.DamageAmount);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, target.GameObject.transform.position);
            target.TakeDamage(this, attackData.DamageAmount);
        }
        yield return new WaitForSeconds(0.1f);
        lineRenderer.positionCount = 0;
    }
}
