using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteChargeShot : MonoBehaviour
{
    // ChargeShot�X�N���v�g����󂯎�郂�m(�_���[�W���ʂ��v�Z���邽��)
    private float ChargeTime;
    private int TotalDamage;

    private float damage;   // ���������G�ɗ^����_���[�W


    public float lifeTime;  // ������܂ł̕b��

    /*********** �Q�[���I�u�W�F�N�g,�X�N���v�g�擾 ***********/
    GameObject player;
    ChargeShot shot;

    [SerializeField] GameObject ChargeShotEffect;           // �G�t�F�N�g


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);  // ��������Ă���"lifeTime"�b��ɏ�����悤��(���ɂ�������Ȃ��������p�̕ی�)

        // ChargeShot�X�N���v�g�ŎZ�o���ꂽ�_���[�W���ʁA�`���[�W���Ԃ��擾
        player = GameObject.Find("Player");
        shot = player.GetComponent<ChargeShot>();
        ChargeTime = shot.ChargeTime;
        TotalDamage = shot.TotalDamage;

        damage = ChargeTime * TotalDamage / 10; // �_���[�W���� �^������_���[�W�̌v�Z�͍��̂Ƃ���K��

        // �󂯎�����猳�X�N���v�g�̐��l��������
        shot.InitStatus();

        // �G�t�F�N�g����
        ChargeShotEffect = Instantiate(ChargeShotEffect, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward);

        // �G�t�F�N�g�̒Ǐ]
        ChargeShotEffect.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider obj)
    {
        // �G�ɓ��������炻�̓G�ɑ����̃_���[�W��^����
        if (obj.gameObject.tag == "Enemy")
        {
            // �֐��g�����߂Ɏ擾
            EnemyHPUI damageHP = obj.GetComponent<EnemyHPUI>();
            // �o�O�}�~
            if (damageHP == null)
            {
                Debug.Log("�_���[�W��^���������HP�������Ă܂���");
                return;
            }

            // �_���[�W��^����
            damageHP.Damage(damage,true);

            // �G�ꂽ��I�u�W�F�N�g������
            Destroy(gameObject);

            // �G�t�F�N�g�폜
            Destroy(ChargeShotEffect);
        }
    }
}
