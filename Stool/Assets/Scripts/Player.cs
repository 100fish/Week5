using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform translator;
    public Animator rotate_Animator;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public float _speed = 0.01f;
    public float _overflow = 0.35f;


    // Update is called once per frame
    void FixedUpdate()
    {
        bool _moving = false;
        float _x_pos = 0f;
        float _z_pos = 0f;

        if (Input.GetAxisRaw("Vertical") > _overflow)
        {
            _z_pos -= _speed;
        }

        if (Input.GetAxisRaw("Vertical") < -_overflow)
        {
            _z_pos += _speed;
        }

        if (Input.GetAxisRaw("Horizontal") > _overflow)
        {
            _x_pos -= _speed;
        }

        if (Input.GetAxisRaw("Horizontal") < -_overflow)
        {
            _x_pos += _speed;
        }

        if (_x_pos != 0 || _z_pos != 0) _moving = true;
        animator.SetBool("moving", _moving);

        translator.position = new Vector3(translator.position.x + _x_pos, translator.position.y, translator.position.z + _z_pos);

        if (_x_pos > 0) _x_pos = 1;
        if (_x_pos < 0) _x_pos = -1;

        if (_z_pos > 0) _z_pos = 1;
        if (_z_pos < 0) _z_pos = -1;

        Vector2 _direction = new Vector2(_x_pos, _z_pos);
       

        if (_direction == Vector2.zero) return;


        print("grandson: " + _direction);

        rotate_Animator.SetFloat("X", _direction.x);
        rotate_Animator.SetFloat("Y", _direction.y);

        //if (_direction == new Vector2(0, 1)) transform.root.localEulerAngles = new Vector3(0, 0, 0);
        //else if (_direction == new Vector2(1, 1)) transform.root.localEulerAngles = new Vector3(0, 45, 0);
        //else if (_direction == new Vector2(1, 0)) transform.root.localEulerAngles = new Vector3(0, 90, 0);
        //else if (_direction == new Vector2(1, -1)) transform.root.localEulerAngles = new Vector3(0, 135, 0);
        //else if (_direction == new Vector2(0, -1)) transform.root.localEulerAngles = new Vector3(0, 180, 0);
        //else if (_direction == new Vector2(-1, -1)) transform.root.localEulerAngles = new Vector3(0, 225, 0);
        //else if (_direction == new Vector2(-1, 0)) transform.root.localEulerAngles = new Vector3(0, 270, 0);
        //else if (_direction == new Vector2(-1, 1)) transform.root.localEulerAngles = new Vector3(0, 315, 0);
    }
}
