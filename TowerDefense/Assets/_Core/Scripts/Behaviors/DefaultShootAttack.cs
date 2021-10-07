using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// Default attack behavior
/// </summary>
public class DefaultShootAttack : AttackBehavior
{
    [SerializeField]
    GameObject[] fires;
    private void OnEnable()
    {
        SetFireActive(false);
    }
    public override void Shoot(IDamageReceiver target)
    {
        transform.DOScale(Vector3.one * 1.05f, .1f).SetLoops(2, LoopType.Yoyo);
        target.TakeDamage(this, attackData.DamageAmount);
        StartCoroutine(ShowFire());
    }

    public void SetFireActive(bool active)
    {
        foreach (var fire in fires)
            fire.SetActive(active);
    }

    IEnumerator ShowFire()
    {
        SetFireActive(true);
        float elapsed = 0;
        while (elapsed < .1f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        SetFireActive(false);
    }

}
