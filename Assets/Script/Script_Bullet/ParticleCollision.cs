using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public float damage;
    void OnParticleCollision(GameObject obj)
    {
        Destroy(this.gameObject);

        if (obj.gameObject.tag == "Player" || obj.gameObject.tag == "Enemy")
        {
            //// 関数使うために取得
            ////DamageHP damageHP = obj.GetComponent<DamageHP>();
            //// バグ抑止
            //if(damageHP == null)
            //{
            //    Debug.Log("ダメージを与えたいやつがHPを持ってません");
            //    return;
            //}
            //// ダメージを与える
            //damageHP.Damage(damage , obj);
        }
    }
}
