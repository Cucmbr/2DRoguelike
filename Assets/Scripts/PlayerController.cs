using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody2D _rigidbody;
    [SerializeField] public FixedJoystick moving_joystick;
    [SerializeField] public FixedJoystick attack_joystick;
    public bool flipRot = true;
    public float _moveSpeed;

    private void FixedUpdate()
    {
        _moveSpeed = PlayerScript.MovementSpeed;
        //������ ��� ������������� �������� �������� ��������
        if (moving_joystick.Horizontal != 0 || moving_joystick.Vertical != 0)
        {
            //����������� ��������� ��������� ����� 
            float horizontal = moving_joystick.Direction.x;
            float vertical = moving_joystick.Direction.y;
            //���������� ������ ���� ��� �������� ������
            float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            angle = flipRot ? -angle : angle;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.position += new Vector3(moving_joystick.Horizontal * _moveSpeed, moving_joystick.Vertical * _moveSpeed, 0);
        }
        if (attack_joystick.Horizontal != 0 || attack_joystick.Vertical != 0)
        {
            float horizontal = attack_joystick.Direction.x;
            float vertical = attack_joystick.Direction.y;
            //���������� ������ ���� ��� �������� ������
            float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            angle = flipRot ? -angle : angle;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        }
}