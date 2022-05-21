using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailBullet : MonoBehaviour
{
    public float damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        // 生成されてから削除されるまで10秒
        Destroy(this.gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player" || obj.gameObject.tag == "PlayerRings")
        {
            obj.GetComponent<PlayerHPUI>().Damage(damage);
        }
        if (obj.gameObject.tag == "Enemy" || obj.gameObject.tag == "EnemyRings")
        {
            obj.GetComponent<EnemyHPUI>().Damage(damage, false);
        }
    }
}
