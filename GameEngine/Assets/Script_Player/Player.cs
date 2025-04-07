using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] private InputActionAsset Input;
    [SerializeField] private double Maxspeed; // �̵��ӵ� ������ ���� ����
    private InputAction MoveAction; // �̵� (�Ŵ������� Y�� �̵��� ���� ������ �ʿ�)
    Rigidbody2D Rigidbody;

    Vector2 MoveDirection;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        MoveAction = Input.FindActionMap("Player").FindAction("Move");
        MoveAction.performed += Move_perform;
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
    void Move(Vector2 Direction)
    {
        Rigidbody.linearVelocityX = Direction.x;
    }
}