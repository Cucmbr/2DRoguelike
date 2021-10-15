using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody2D _rigidbody;
    [SerializeField] public FixedJoystick _joystick;
    public bool flipRot = true;
    public float _moveSpeed;

    private void FixedUpdate()
    {
        //Только при использовании джостика возможно движение
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            //Вычисляется изменение положения стика 
            float horizontal = _joystick.Direction.x;
            float vertical = _joystick.Direction.y;
            //Вычисление нового угла для поворота игрока
            float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            angle = flipRot ? -angle : angle;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.position += new Vector3(_joystick.Horizontal * _moveSpeed, _joystick.Vertical * _moveSpeed, 0);
        }
    }
}