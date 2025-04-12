using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] private InputActionAsset Input;
    [SerializeField] private double Maxspeed; // �̵��ӵ� ������ ���� ����
    [SerializeField] private int Atkdmg;
    public int CharacterValue;

    private InputAction MoveAction; // �̵� (�Ŵ������� Y�� �̵��� ���� ������ �ʿ�)
    private InputAction AttackAction; // �⺻����
    private InputAction Skill1;
    private InputAction Skill2;
    Rigidbody2D Rigidbody;

    Vector2 MoveDirection;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        MoveAction = Input.FindActionMap("Player").FindAction("Move");
        MoveAction.performed += Move_perform;

        AttackAction = Input.FindActionMap("Player").FindAction("Attack");
        AttackAction.performed += DefaultAttack_perform;

        Skill1 = Input.FindActionMap("Player").FindAction("Skill1");
        Skill2 = Input.FindActionMap("Player").FindAction("Skill2");

        if (CharacterValue == 1) {
            Skill1.performed += SwordSkill1;
            Skill2.performed += SwordSkill2;
        }
    }
    void FixedUpdate()
    {
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
        Rigidbody.linearVelocityX = Direction.x;
    }

    void SwordSkill1(InputAction.CallbackContext obj)
    {

    }
    void SwordSkill2(InputAction.CallbackContext obj)
    {

    }

    //void Damaged()
}