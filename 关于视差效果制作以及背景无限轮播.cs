using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxbackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float ParallaxEffect;  //视差效果  越近数值越大，同步设为0，越远数值越小

    private float xPosition;
    private float length;   //记录边界值
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;  //获取当前对象的SpriteRenderer组件的X边界值
        xPosition = transform.position.x;   
    }


    void Update()
    {
        float distanceMove = cam.transform.position.x * (1 - ParallaxEffect);          //制作视差效果的移动距离
        float distanceToMove = cam.transform.position.x * ParallaxEffect;       //基于摄像机x位置和视差效果计算出来的移动距离

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);     //反复更新对象位置
    

        if(distanceMove > xPosition + length)       //角色向右移动重置背景
        {
            xPosition = xPosition + length;
        }else if (distanceMove < xPosition - length)    //角色向左移动重置背景
        {
            xPosition = xPosition - length;
        }
    }
}
