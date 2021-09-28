using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody2D _rigidbody;
    [SerializeField] public FixedJoystick _joystick;

    public float _moveSpeed;

    private void FixedUpdate()
    {
        //_rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);
        //transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        transform.position += new Vector3(_joystick.Horizontal * _moveSpeed, _joystick.Vertical * _moveSpeed,0);
    }
}