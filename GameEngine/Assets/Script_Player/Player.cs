using System;
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

    [SerializeField] private bool JumpChecker;
    Vector2 MoveDirection;
    void Start()
    {
        JumpChecker = false;
        Rigidbody = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collider2D>();
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
        Debug.Log(MoveDirection.x + " " + MoveDirection.y);
    }
    void DefaultAttack_perform(InputAction.CallbackContext obj)
    {
        Debug.Log("DefaultAttack");
    }
    void Move(Vector2 Direction)
    {
        if (Direction.y > 0 && JumpChecker == false)
        {
            Rigidbody.AddForce(new Vector2(0, JumpPower));
            JumpChecker = true;
        }
        Rigidbody.linearVelocityX = Direction.x;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Map")
        {
            JumpChecker = false;
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