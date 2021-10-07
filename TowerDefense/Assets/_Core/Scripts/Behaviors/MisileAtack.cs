using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisileAtack : AttackBehavior
{
    [SerializeField]
    GameObject[] missile;
    private void OnEnable()
    {
        for (int i = 0; i < missile.Length; i++)
        {
            missile[i].SetActive(false);
            missile[i].transform.localPosition = Vector3.zero;
            missile[i].transform.localRotation = Quaternion.identity;
        }
    }
    public override void Shoot(IDamageReceiver target)
    {
        StartCoroutine(LaunchMisiles(target));
    }

    IEnumerator LaunchMisiles(IDamageReceiver target)
    {
        float elapsed = 0;
        float duration = .3f;
        for (int i = 0; i < missile.Length; i++)
        {
            missile[i].SetActive(true);
            missile[i].transform.localPosition = Vector3.zero;
            missile[i].transform.localRotation = Quaternion.identity;
        }
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float elapsedPercentage = elapsed / duration;
            for (int i = 0; i < missile.Length; i++)
            {
                Vector3 direction = target.GameObject.transform.position - missile[i].transform.position;
                missile[i].transform.up = direction;
                missile[i].transform.position = Vector3.Lerp(missile[i].transform.parent.position, target.GameObject.transform.position,elapsedPercentage);
                
            }
            yield return null;
        }
        if (attackData.ProjectileType == ProjectileType.splash)
        {
            var hits = Physics2D.CircleCastAll(target.GameObject.transform.position, 1, Vector3.forward);
            foreach (var enemy in hits)
            {
                IDamageReceiver damageReceiver = enemy.collider.gameObject.GetComponent<IDamageReceiver>();
                if (damageReceiver != null)
                    damageReceiver.TakeDamage(this, attackData.DamageAmount);
            }
        }
        else
            target.TakeDamage(this, attackData.DamageAmount);
        for (int i = 0; i < missile.Length; i++)
            missile[i].SetActive(false);
    }

}
