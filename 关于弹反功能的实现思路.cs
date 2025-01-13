/*
1、在敌人进行攻击前的抬手动画中加入关键帧和对应函数（提示可以进行弹反的函数）
2、并且在该函数中设置相对应的控制敌人是否可以被弹反的语句
3、在玩家方面设置进入弹反状态，在该状态的Update语句中判断攻击范围内是否存在敌人
4、若存在敌人并且该敌人是处于可以被弹反的状态（出现弹反提示），随即进入弹反成功的动画，并且立即关闭弹反提示窗口结束敌人可以被弹反的状态
*/

/*例子*/

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterAttackWindow.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;   
        counterAttackWindow.SetActive(false);               
    }

    public virtual bool CanbeStunned()  //选择公开目的是为了玩家能够调用这部分，懒得再创一个脚本反正目前就用这么一次，后面再看看要不要再单独创建
    {
        if (canBeStunned)   //如果可以进行反击。则打开反击提速窗口并且返回真
        {
            OpenCounterAttackWindow();
            return true;
        }
        return false;
    }


    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();   //进入反击状态不能移动，增加游戏难度

        Collider2D[] collider = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);  //绘制圆形区域用于检测攻击范围内的目标
        foreach (var hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)  //若在攻击范围内存在敌人
            {
                if(hit.GetComponent<Enemy>().CanbeStunned())    //并且该敌人可以被弹反（出现弹反窗口）
                {
                    stateTimer = 10;    //随便设一个大于1的,确保反击动作执行完毕
                    player.anim.SetBool("SuccessfulCounterAttack", true);   //随即进入弹反成功动画
                    hit.GetComponent<Enemy>().CloseCounterAttackWindow();   //最后立即关闭弹反窗口结束敌人可以被弹反的状态
                } 
            }
        }

        if(stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

