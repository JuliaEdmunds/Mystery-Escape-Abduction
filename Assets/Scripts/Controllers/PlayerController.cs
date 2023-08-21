﻿using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT_NAME = "Horizontal";
    private const string VERTICAL_INPUT_NAME = "Vertical";

    [SerializeField, Range(2f, 4f)] private float movementSpeed;

    private CharacterController m_CharController;
    private bool m_IgnoreNextMouseMovement = false;

    private void Awake()
    {
        m_CharController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float vertInput = Input.GetAxis(VERTICAL_INPUT_NAME) * movementSpeed;
        float horizInput = Input.GetAxis(HORIZONTAL_INPUT_NAME) * movementSpeed;

        Vector3 movement = (transform.forward * vertInput) + (transform.right * horizInput);

        m_CharController.SimpleMove(movement);
    }
}
