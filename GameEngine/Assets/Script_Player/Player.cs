using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField] private InputActionAsset Input;
    [SerializeField] private float Maxspeed; // �̵��ӵ� ������ ���� ����
    [SerializeField] private float JumpPower; // ������
    [SerializeField] private int Atkdmg; // ���ݷ�

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
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collider2D>();
        PlayerAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        PlayerAnimator.SetBool("Jump", false);

        MoveAction = Input.FindActionMap("Player").FindAction("Move");
        MoveAction.performed += Move_perform;

        AttackAction = Input.FindActionMap("Player").FindAction("Attack");
        AttackAction.performed += DefaultAttack_perform;

        Skill1 = Input.FindActionMap("Player").FindAction("Skill1");
        Skill2 = Input.FindActionMap("Player").FindAction("Skill2");

        if (CharacterValue == 1)
        {
            Skill1.performed += SwordSkill1;
            Skill2.performed += SwordSkill2;
        }
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
        Debug.Log("DefaultAttack");
        PlayerAnimator.SetBool("Attack", true);
        StopCoroutine("AttackCoroutine");
        StartCoroutine("AttackCoroutine");
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

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
            sprite.flipX = true;
        }
        else if (Direction.x == 0) {PlayerAnimator.SetBool("Walk", false); }
        else
        {
            sprite.flipX = false;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Map")
        {
            PlayerAnimator.SetBool("Jump", false);
        }
    }
    void SwordSkill1(InputAction.CallbackContext obj)
    {

    }
    void SwordSkill2(InputAction.CallbackContext obj)
    {

    }

    //void Damaged()
}