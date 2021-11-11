using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    FixedJoystick moving_joystick;
    FixedJoystick attack_joystick;
    public bool facingRight = false;
    public float _moveSpeed;
    [SerializeField] Animator animator;

    private void Awake()
    {
        _rigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        moving_joystick = GameObject.Find("MovementJoystick").GetComponent<FixedJoystick>();
        attack_joystick = GameObject.Find("AttackJoystick").GetComponent<FixedJoystick>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        _moveSpeed = PlayerScript.MovementSpeed;
        //Только при использовании джостика возможно движение
        if (moving_joystick.Horizontal != 0 || moving_joystick.Vertical != 0)
        {
            if(moving_joystick.Horizontal < 0 && facingRight)
            {
                Flip();
            }
            else if(moving_joystick.Horizontal > 0 && !facingRight)
            {
                Flip();
            }
            transform.position += new Vector3(moving_joystick.Horizontal * _moveSpeed * 0.15f, moving_joystick.Vertical * _moveSpeed * 0.15f, 0);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        if (attack_joystick.Horizontal != 0 || attack_joystick.Vertical != 0)
        {
            if (attack_joystick.Horizontal < 0 && facingRight)
            {
                Flip();
            }
            else if (attack_joystick.Horizontal > 0 && !facingRight)
            {
                Flip();
            }
            float horizontal = attack_joystick.Direction.x;
            float vertical = attack_joystick.Direction.y;
            //Вычисление нового угла для поворота игрока
            float angle = -Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        }
    void Flip()
    {
        facingRight = !facingRight;
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;     
    }
}