using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxbackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float ParallaxEffect;  //�Ӳ�Ч��  Խ����ֵԽ��ͬ����Ϊ0��ԽԶ��ֵԽС

    private float xPosition;
    private float length;   //��¼�߽�ֵ
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;  //��ȡ��ǰ�����SpriteRenderer�����X�߽�ֵ
        xPosition = transform.position.x;   
    }


    void Update()
    {
        float distanceMove = cam.transform.position.x * (1 - ParallaxEffect);          //�����Ӳ�Ч�����ƶ�����
        float distanceToMove = cam.transform.position.x * ParallaxEffect;       //���������xλ�ú��Ӳ�Ч������������ƶ�����

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);     //�������¶���λ��
    

        if(distanceMove > xPosition + length)       //��ɫ�����ƶ����ñ���
        {
            xPosition = xPosition + length;
        }else if (distanceMove < xPosition - length)    //��ɫ�����ƶ����ñ���
        {
            xPosition = xPosition - length;
        }
    }
}
