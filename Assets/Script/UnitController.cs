using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using anogamelib;
using UnityEngine.Events;
using TMPro;

public class UnitController: StateMachineBase<UnitController>
{
    public InputAction InputMove;
    public Vector2 Movevalue;
    private Rigidbody rb;
    public float moveSpeed = 3.0f;
    private Animator Anim;
    public bool isBattle;
    private UnityEvent AttackHitHandler = new UnityEvent();
    private UnityEvent AttackEndHandler = new UnityEvent();
    private UnityEvent FreezeHandler = new UnityEvent();
    public bool CanWalk;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        SetState(new UnitController.Idle(this));
    }

    public void OnAttackHit()
    {
        AttackHitHandler.Invoke();
    }
    public void OnAttackEnd()
    {
        AttackEndHandler.Invoke();
    }
    public void OnFreeze()
    {
        FreezeHandler.Invoke();
    }

    private void OnEnable()
    {
        //Debug.Log("Enable");
        InputMove.Enable();
    }

    private void OnDisable()
    {
        //Debug.Log("Disable");
        InputMove.Disable();
    }
    protected override void OnUpdatePrev()
    {
        base.OnUpdatePrev();
        Movevalue = InputMove.ReadValue<Vector2>();

        /*if (Input.GetMouseButtonDown(0))
        {
            Anim.SetTrigger("AttackTrigger");
        }*/

    }

    public void FightButtonDown()
    {
        isBattle = true;
        Anim.SetBool("isBattle", isBattle);
    }

    public void EscapeButtonDown()
    {
        isBattle = false;
        Anim.SetBool("isBattle", isBattle);
    }

    private void FixedUpdate()
    {
        if (CanWalk)
        {
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            Vector3 moveForward = cameraForward * Movevalue.y + Camera.main.transform.right * Movevalue.x;

            rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }

            if (Anim != null)
            {
                Anim.SetBool("isWalk", 0 < Movevalue.sqrMagnitude);
            }
        }
        else
        {
            Anim.SetBool("isWalk", false);
        }
    }

    private class Idle : StateBase<UnitController>
    {
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            machine.CanWalk = true;
            if (machine.isBattle)
            {
                machine.SetState(new UnitController.Search(machine));
            }
            if (DataManager.Instance.UnitPlayer.HP <= 0)
            {
                //machine.rb.isKinematic = true;
                machine.CanWalk = false;
                machine.SetState(new UnitController.DeadState(machine));
            }
        }
        public Idle(UnitController _machine) : base(_machine)
        {
        }
    }

    private class Search : StateBase<UnitController>
    {
        public override void OnEnterState()
        {
            base.OnEnterState();
            //Debug.Log("search");
        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            
            if (!machine.isBattle)
            {
                machine.SetState(new UnitController.Idle(machine));
            }
            foreach(EnemyController enemy in EnemyManager.Instance.EnemyList)
            {
                if (enemy.IsFind() && enemy.isAlive())
                {
                    machine.CanWalk = false;
                    machine.SetState(new UnitController.Battle(machine, enemy));
                    break;
                }
            }
        }
        public Search(UnitController _machine) : base(_machine)
        {
        }
    }

    private class Battle : StateBase<UnitController>
    {
        private EnemyController enemy;
        private float Timer;
        public float AttackSpan;
        public GameDirector director;

        public override void OnEnterState()
        {
            base.OnEnterState();
            GameDirector.Instance.EscOff();
            //machine.rb.isKinematic = true;
            //Debug.Log("battle");
            AttackSpan = 1.0f;
        }

        public override void OnUpdateState()
        {
            base.OnUpdateState();
            machine.transform.LookAt(enemy.transform);
            Timer += Time.deltaTime;
            if (Timer >= AttackSpan)
            {
                machine.SetState(new UnitController.Attack(machine, enemy));
            }
            
            if (DataManager.Instance.UnitPlayer.HP <= 0)
            {
                machine.SetState(new UnitController.DeadState(machine));
            }
        }
        public Battle(UnitController machine, EnemyController enemy):base(machine)
        {
            this.machine = machine;
            this.enemy = enemy;
        }
    }

    private class Attack : StateBase<UnitController>
    {
        private EnemyController enemy;
        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.AttackHitHandler.AddListener(() =>
            {
                int attack = DataManager.Instance.UnitPlayer.GetTotalAttack();
                if (enemy.Damage(attack))
                {
                    GameDirector.Instance.AddGold(10);
                };
                GameObject damage = Instantiate(PrefabHolder.Instance.DamageView) as GameObject;
                damage.transform.position = enemy.transform.position;
                damage.GetComponent<TextMeshPro>().text = $"{attack}";
            });
            machine.AttackEndHandler.AddListener(() =>
            {
                machine.SetState(new UnitController.Battle(machine, enemy));
            });
            machine.Anim.SetTrigger("AttackTrigger");
            //Debug.Log("attack");

        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            if (!enemy.isAlive())
            {
                MasterEnemyParam masterenemy = DataManager.Instance.masterenemy.list.Find(p => p.Enemy_ID == enemy.Enemy_ID);
                GameDirector.Instance.EscOn();
                machine.CanWalk = true;
                GameDirector.Instance.GetEXP(masterenemy.Base_EXP);
                machine.SetState(new UnitController.Search(machine));
            }
        }
        public override void OnExitState()
        {
            base.OnExitState();
            machine.AttackHitHandler.RemoveAllListeners();
            machine.AttackEndHandler.RemoveAllListeners();
        }
        public Attack(UnitController machine, EnemyController enemy) : base(machine)
        {
            this.machine = machine;
            this.enemy = enemy;
        }
    }

    private class DeadState : StateBase<UnitController>
    {
        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.FreezeHandler.AddListener(() => {
                machine.SetState(new UnitController.Freeze(machine));
            });
            machine.Anim.SetTrigger("DieTrigger");
        }
        public DeadState(UnitController _machine) : base(_machine)
        {
        }
    }

    private class Freeze : StateBase<UnitController>
    {
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            if (DataManager.Instance.UnitPlayer.HP > 0)
            {
                //Debug.Log("revive");
                machine.Anim.SetTrigger("ReviveTrigger");
                machine.SetState(new UnitController.Idle(machine));
            }
        }
        public override void OnExitState()
        {
            base.OnExitState();
        }
        public Freeze(UnitController _machine) : base(_machine)
        {
        }
    }
}
