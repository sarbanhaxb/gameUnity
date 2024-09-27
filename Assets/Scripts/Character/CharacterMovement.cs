using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Unity.VisualScripting;

public class CharacterMovement : MonoBehaviour
{
    // НУЖНО РАЗОБРАТЬСЯ С ВЫЛЕТОМ ДВУХ ПУЛЬ ИЗ-ЗА ВКЛЮЧЕНИЯ ОДНОВРЕМЕННО ДВУХ АНИМАЦИЙ

    [SerializeField]
    // скорость движения персонажа
    float moveSpeed = 5f;

    CharacterController controller;
    public Vector2 moveDirection;

    public float moveX;
    public float moveY;

    Vector3 mouseDirection;

    public GameObject bulletPref;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // вызывает все рабочие методы
    void Update()
    {
        HandleMovementInput();
    }

    //для опитмизации
    private void FixedUpdate()
    {
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    //определяет направление движения
    void HandleMovementInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    //возвращает позицию клика курсора
    public Vector3 GetShootingDirection()
    {
        mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection.z = 0f;
        return (mouseDirection - transform.position).normalized;
    }
}
