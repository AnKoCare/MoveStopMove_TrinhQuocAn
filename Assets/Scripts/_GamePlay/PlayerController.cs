using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (BoxCollider))]
public class PlayerController : MonoBehaviour
{
    // [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Character character;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;
    private void Start() 
    {
        
    }

    private void FixedUpdate()
    {
 
        _characterController.Move(new Vector3(_joystick.Horizontal * _moveSpeed * Time.fixedDeltaTime, _gravity, _joystick.Vertical * _moveSpeed * Time.fixedDeltaTime));

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical));
            character.ChangeAnim("Patrol");
            character.IsIdle = false;
        }
        else if(_joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            character.ChangeAnim("Idle");
            character.IsIdle = true;
        }
    }

}