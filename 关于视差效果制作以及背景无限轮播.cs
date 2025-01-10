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

/*
对于 distanceMove 的理解：
假设汽车沿着一条笔直的道路行驶，我们将汽车的前进方向看作 x 轴。cam.transform.position.x 就相当于汽车当前所在位置的 x 坐标，也就是你眼睛的位置。
当 ParallaxEffect 为 0 时，就好像你直接看着离你最近的物体，比如汽车的仪表盘。仪表盘会随着汽车的移动同步移动，因为它与你（摄像机）的移动速度相同，没有视差效果。
在这种情况下，distanceMove = cam.transform.position.x * (1 - 0) = cam.transform.position.x，这意味着仪表盘（相当于游戏对象）的位置变化和你的视角（摄像机）完全一致，它们同步移动，就像仪表盘始终在你面前，位置不会相对变化。
当 ParallaxEffect 为 0.5 时，想象你在看远处的山脉。山脉相对于汽车的移动速度会显得比较慢，产生了视差效果。
这时 distanceMove = cam.transform.position.x * (1 - 0.5) = cam.transform.position.x * 0.5，这就好像山脉（游戏对象）的移动速度是汽车（摄像机）速度的一半。所以，当汽车向前移动一定距离时，山脉看起来移动的距离只有汽车移动距离的一半，从而产生了一种距离感和层次感，因为远处的物体看起来移动得更慢，给人一种视差的效果。
对于 distanceToMove 的理解：
继续上面的例子，distanceToMove 与 distanceMove 互补，共同决定了物体相对于摄像机的移动距离。
当 ParallaxEffect 为 0.3 时，distanceToMove = cam.transform.position.x * 0.3。假设你在看路边的电线杆，电线杆离你比山脉近一些，但还是有一定距离。
这个 distanceToMove 的计算就表示电线杆相对于汽车（摄像机）的移动距离。因为 ParallaxEffect 是 0.3，电线杆的移动速度是汽车（摄像机）速度的 30%。
再回到汽车的场景，distanceMove 代表了物体跟随汽车移动的一部分速度，而 distanceToMove 代表了物体因为视差效果而产生的额外的相对移动部分。
它们加起来等于汽车的移动速度，但是各自的比例不同，从而使不同距离的物体以不同的速度移动，模拟出了现实世界中我们观察到的近快远慢的视觉效果。
例如，汽车向前移动了 10 米，如果 ParallaxEffect 为 0.3，distanceMove 就是 7 米（汽车移动距离的 70%），而 distanceToMove 是 3 米（汽车移动距离的 30%）。
电线杆会根据这两个距离进行移动，产生一种相对移动的效果，离我们越近的电线杆看起来移动得越快，越远的电线杆看起来移动得越慢，从而产生了层次感和深度感，就像在现实世界中我们看到的那样，为我们提供了一种自然的视觉体验。
*/
