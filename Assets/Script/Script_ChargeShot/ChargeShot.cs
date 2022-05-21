using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShot: MonoBehaviour
{
    public int TotalDamage;     // �`���[�W���Ɏ󂯂��_���[�W�̑���
    public float ChargeTime;    // �`���[�W��������
    public  bool bCharge;       // �`���[�W�����ǂ���(PlayerHPUI�ɔ����n������)

    public GameObject PlayerBullet; // �`���[�W�V���b�g�̃I�u�W�F�N�g

    [SerializeField] GameObject ChargeEffect;           // �`���[�W���̃G�t�F�N�g
    private bool chargeEffect;


    // �T�E���h
    GameObject SE;
    SoundSE soundSE;

    // Start is called before the first frame update
    void Start()
    {
        TotalDamage = 0;
        ChargeTime = 0.0f;

        // SE�I�u�W�F�N�g�擾
        SE = GameObject.FindWithTag("SE");
    }

    // Update is called once per frame
    void Update()
    {
        // �G�t�F�N�g�Ǐ]
        ChargeEffect.transform.position = transform.position;

        if (Input.GetKey("e") || Input.GetKey("joystick button 3")) //Y�{�^��        �ύX�_(�O�c)
        {
            bCharge = true;
            ChargeTime += Time.deltaTime;


            // �܂��G�t�F�N�g���o�Ă��Ȃ��Ȃ�
            if (!chargeEffect)
            {
                // �`���[�W�G�t�F�N�g
                ChargeEffect = Instantiate(ChargeEffect, transform.position, transform.rotation);

                chargeEffect = true;

                // �G�t�F�N�g�\��
                ChargeEffect.SetActive(true);
            }
        }

        // E�L�[�𗣂����u�ԂɁA�_���[�W���󂯂Ă���Ȃ�`���[�W�V���b�g������
        if (Input.GetKeyUp("e") || Input.GetKeyUp("joystick button 3") && TotalDamage > 0)  //�ύX�_(�O�c)
        {
            // �Ȃ��Ă��鉹���~�߂�
            SE.GetComponent<SoundSE>().Stop();

            chargeEffect = false;

            // �G�t�F�N�g��\��
            ChargeEffect.SetActive(false);

            // �`���[�WSE
            SE.GetComponent<SoundSE>().PlaySE(6);

            Instantiate(PlayerBullet, transform.position, transform.rotation);
            bCharge = false;
        }
        else if (Input.GetKeyUp("e") || Input.GetKeyUp("joystick button3") && TotalDamage == 0) //�ύX�_(�O�c)
        {
            bCharge = false;
            InitStatus();
        }
    }

    /*
     * SetDamage�֐�
     * �v���C���[�����ۂɃ_���[�W���󂯂�Ƃ���ŁAe�L�[��������Ă���Ȃ�_���[�W�����߂�悤�ɂ���
     * ���� : damage ���炤�_���[�W��
     * */
    public void SetDamage(int damage)
    {
        // �`���[�WSE
        SE.GetComponent<SoundSE>().PlaySE(5);

        TotalDamage += damage * 5;
    }

    /*
     * InitStatus�֐�
     * TotalDamage��AChargeTime�̒l���󂯓n������ɁA���ꂼ��̒l�������������邽�߂ɌĂ΂���B
     */
    public void InitStatus()
    {
        TotalDamage = 0;
        ChargeTime = 0.0f;
    }
}
