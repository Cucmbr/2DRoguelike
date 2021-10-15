using UnityEngine;

public class ImpMovement : MonoBehaviour
{
    public Vector2 _direction;
    public float _moveSpeed;
    public bool flipRot = true;
    //public Transform rotationTarget;

    private void Start()
    {
        //Задаются случайные координаты направления движения и присваиваются _direction
        float x = Random.Range(-1f, 1f);
        int sign = Random.Range(0, 1);
        if (sign == 0)
            sign = -1;
        float y = sign * (1 - Mathf.Abs(x));
        _direction = new Vector2(x, y);
    }

    private void FixedUpdate()
    {
        //Движение
        transform.Translate(_direction * _moveSpeed);

        //Высчитывание угла для поворота спрайта по направлению движения самого объекта
        float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
        angle = flipRot ? -angle : angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Отскок от стен под тем же углом
            Vector2 inDirection = _direction;
            Vector2 inNormal = collision.contacts[0].normal;
            _direction = Vector2.Reflect(inDirection, inNormal);
        }
    }
}
