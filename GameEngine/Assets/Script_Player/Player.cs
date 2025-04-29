using System;
using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField] private InputActionAsset Input;
    [SerializeField] private float Maxspeed; // �̵��ӵ� ������ ���� ����
    [SerializeField] private float JumpPower; // ������
    [SerializeField] private int Atkdmg; // ���ݷ�
    [SerializeField] private GameObject AttackRange;
    private Collider2D AttackCollider;
    private Transform RangeTransform;

    public int CharacterValue; // ���ӸŴ������� Ȯ��&�����ϱ� ���� public

    private InputAction MoveAction; // �̵� (�Ŵ������� Y�� �̵��� ���� ������ �ʿ�)
    private InputAction AttackAction; // �⺻����
    private InputAction Skill1;
    private InputAction Skill2;

    Collider2D collision;
    Rigidbody2D Rigidbody;
    Animator PlayerAnimator;
    SpriteRenderer sprite;

    Vector2 MoveDirection;

    // default type ����
    [SerializeField] float MaxHp; // �÷��̾� �⺻ ü��
    float Hp;
    [SerializeField] float AtkDmg; // �⺻ ���ݷ�

    float Atkpos;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collider2D>();
        PlayerAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        AttackCollider = AttackRange.GetComponent<Collider2D>();
        RangeTransform = AttackRange.GetComponent<Transform>();

        PlayerAnimator.SetBool("Jump", false); // ���۽� �������°� �ƴϹǷ� false�� �ʱ�ȭ

        MoveAction = Input.FindActionMap("Player").FindAction("Move"); //�̵��Լ� �ݹ�
        MoveAction.performed += Move_perform;

        AttackAction = Input.FindActionMap("Player").FindAction("Attack"); // �����Լ� �ݹ�
        AttackAction.performed += DefaultAttack_perform;

        Skill1 = Input.FindActionMap("Player").FindAction("Skill1"); // ��ų1
        Skill2 = Input.FindActionMap("Player").FindAction("Skill2"); // ��ų2

        if (CharacterValue == 1) // ĳ���� �߰� ������ ���
        {
            Skill1.performed += SwordSkill1;
            Skill2.performed += SwordSkill2;
        }

        AttackCollider.enabled = false;
    }
    void FixedUpdate()
    {
        MoveDirection = MoveAction.ReadValue<Vector2>();
        Move(MoveDirection);
    }

    void Move_perform(InputAction.CallbackContext obj) //�ݹ� ȣ��� ���� �̵� �и�
    {
        MoveDirection = obj.ReadValue<Vector2>();
        PlayerAnimator.SetBool("Walk", true);
        Debug.Log(MoveDirection.x + " " + MoveDirection.y);
    }
    void DefaultAttack_perform(InputAction.CallbackContext obj)
    {
        if (PlayerAnimator.GetBool("Attack") == false)
        {
            Debug.Log("DefaultAttack");
            PlayerAnimator.SetBool("Attack", true);
            
            StopCoroutine("AttackCoroutine");
            StartCoroutine("AttackCoroutine");
        }
    }



    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        AttackCollider.enabled = true;
        yield return new WaitForSeconds(0.3f);
        AttackCollider.enabled = false;
        PlayerAnimator.SetBool("Attack", false);
        yield break;
    }

    void Move(Vector2 Direction)
    {
        if (Direction.y > 0 && PlayerAnimator.GetBool("Jump") == false)
        {
            Rigidbody.AddForce(new Vector2(0, JumpPower));
            PlayerAnimator.SetBool("Jump", true);
        }
        if (PlayerAnimator.GetBool("Attack") == false)
            Rigidbody.linearVelocityX = Direction.x;

        if (Direction.x > 0)
        {
            RangeTransform.localPosition = new Vector3(0.25f, 1, 0);
            sprite.flipX = true;
        }
        else if (Direction.x == 0) {PlayerAnimator.SetBool("Walk", false); }
        else
        {
            RangeTransform.localPosition = new Vector3(-0.25f, 1, 0); 
            sprite.flipX = false;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Map")
        {
            PlayerAnimator.SetBool("Jump", false);
        }
        if (collision.tag == "Enemy")
        {
            float Dmg = 10;// = collision.gameObject.GetComponent<Enemy>().Dmg
            Damaged(Dmg);
        }
    }
    void SwordSkill1(InputAction.CallbackContext obj)
    {

    }
    void SwordSkill2(InputAction.CallbackContext obj)
    {

    }

    void Damaged(float Dmg)
    {
        Hp -= Dmg;
        if (Hp > 0 && PlayerAnimator.GetBool("Attack") == true) PlayerAnimator.SetBool("Hit", true);
        else { PlayerAnimator.SetBool("Dead", true); }
    }
}