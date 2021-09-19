﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using UnityEngine.Events;

public class EnemyController : StateMachineBase<EnemyController>
{
    public int Enemy_ID;
    public int Enemy_Level;
    private Transform TargetTransform;
    public float Distance;
    public Animator Anim;
    private UnityEvent AttackHitHandler = new UnityEvent();
    private UnityEvent AttackEndHandler = new UnityEvent();
    private UnityEvent FreezeHandler = new UnityEvent();
    private int Hp;
    public int Hp_max;
    Vector3 IdlePos;
    private int attack;
    public MasterEnemyParam usemasterparam;
    public AudioSource Audios;

    private void Start()
    {
        SetState(new EnemyController.Standby(this));
    }
    public void SetUp()
    {
        System.Type t = typeof(UnitController);
        TargetTransform = (FindObjectOfType(t) as UnitController).transform;
        SetState(new EnemyController.Idol(this));
        MasterEnemyParam param = DataManager.Instance.masterenemy.list.
            Find(p => p.Enemy_ID == Enemy_ID);
        Enemy_Level = Random.Range(param.LV_min, param.LV_max);
        usemasterparam = param.Build(Enemy_Level);
        if (Anim == null)
        {
            Anim = GetComponent<Animator>();
        }
        if (Enemy_ID > 0)
        {
            Hp_max = usemasterparam.HP;
        }
        else
        {
            Hp_max = 10;
        }
        Hp = Hp_max;
        EnemyManager.Instance.Add(this);
        IdlePos = transform.position;
        attack = usemasterparam.Attack;
        Audios = GetComponent<AudioSource>();
    }

    public bool IsFind()
    {
        Distance = (transform.position - TargetTransform.position).magnitude;
        return (transform.position - TargetTransform.position).magnitude < 3.5f;
    }
    
    public void OnAnimationEnd()
    {
        //Debug.Log("AnimationEnd");
        AttackEndHandler.Invoke();
    }

    public void OnAttackHit()
    {
        //Debug.Log("AttackHit");
        AttackHitHandler.Invoke();
    }

    public void Freeze()
    {
        //Debug.Log("freeze");
        FreezeHandler.Invoke();
    }

    public bool isAlive()
    {
        return (0 < Hp);
    }
    public bool Damage(int damage)
    {
        bool ret = false;
        Hp -= damage;
        if (Hp <= 0)
        {
            ret = true;
            SetState(new EnemyController.DeadState(this));
        }
        return ret;
    }
    private class Idol : StateBase<EnemyController>
    {
        public Idol(EnemyController _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            //Debug.Log("idol");
        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            if (machine.IsFind())
            {
                machine.SetState(new EnemyController.LookTarget(machine));
            }
        }
    }

    private class LookTarget : StateBase<EnemyController>
    {
        public float WaitTime;
        public float AttackSpan = 5.0f;

        public override void OnEnterState()
        {
            base.OnEnterState();
            //Debug.Log("look");
            machine.Audios.PlayOneShot(AudioManager.Instance.SE_Enemy[1]);
        }
        
        
        public LookTarget(EnemyController _machine) : base(_machine)
        {
        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            machine.transform.LookAt(machine.TargetTransform);
            WaitTime += Time.deltaTime;
            if (WaitTime > AttackSpan)
            {
                machine.SetState(new EnemyController.Attack(machine));
            }
            if (!machine.IsFind())
            {
                machine.SetState(new EnemyController.Idol(machine));
            }
            
        }
    }

    private class Attack : StateBase<EnemyController>
    {
        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.AttackHitHandler.AddListener(() =>
            {
                machine.Audios.PlayOneShot(AudioManager.Instance.SE_Enemy[0]);
                GameDirector.Instance.Damage(machine.attack);
                Debug.Log(machine.attack);
            });
            machine.AttackEndHandler.AddListener(() =>
            {
                machine.SetState(new EnemyController.LookTarget(machine));
            });
            machine.Anim.SetTrigger("AttackTrigger");
            //Debug.Log("attack");
        }
        public override void OnExitState()
        {
            base.OnExitState();
            machine.AttackHitHandler.RemoveAllListeners();
            machine.AttackEndHandler.RemoveAllListeners();
        }

        public Attack(EnemyController _machine) : base(_machine)
        {

        }
    }

    private class DeadState : StateBase<EnemyController>
    {
        public override void OnEnterState()
        {
            base.OnEnterState();
            //machine.Anim.SetTrigger("DieTrigger");
            machine.Anim.Play("Die");
            machine.FreezeHandler.AddListener(() =>
            {
                machine.SetState(new EnemyController.FreezeState(machine));
            });
            //Debug.Log("Dead");
        }
        public override void OnExitState()
        {
            base.OnExitState();
        }
        public DeadState(EnemyController _machine) : base(_machine)
        {

        }
    }

    private class FreezeState : StateBase<EnemyController>
    {
        public float RespawnTime;
        public float DiveSpeed;
        public override void OnEnterState()
        {
            base.OnEnterState();
            //Debug.Log("OnFreeze");
            DiveSpeed = -1.0f;
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();

            if (machine.transform.position.y > -10 && RespawnTime >= 1)
            {
                machine.transform.Translate(0, DiveSpeed/100, 0);
            }

            RespawnTime += Time.deltaTime;
            if (RespawnTime >= 10 && RespawnTime < 11)
            {
                machine.Anim.SetTrigger("RespawnTrigger");
            }
            else if (RespawnTime >= 11)
            {
                machine.gameObject.transform.position = machine.IdlePos;
                machine.SetUp();
                machine.SetState(new EnemyController.Idol(machine));
            }
        }
        public FreezeState(EnemyController _machine) : base(_machine)
        {
        }
    }

    private class Standby : StateBase<EnemyController>
    {
        public Standby(EnemyController _machine) : base(_machine)
        {
        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            System.Type t = typeof(UnitController);
            if ((FindObjectOfType(t) as UnitController) != null)
            {
                machine.SetUp();
            }
        }
    }
}
