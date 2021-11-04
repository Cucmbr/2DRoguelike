using UnityEngine;

public class ImpMovement : MonoBehaviour
{
    public Vector2 _direction;
    public float _moveSpeed = 0.1f;
    public bool flipRot = true;
    public Transform rotationTarget;
    public Transform player;

    private void Start()
    {
        //Высчитывается положение Player и задается направление движения Imp'а в его направлении
        player = GameObject.Find("Player").transform;
        Vector3 targetPos = player.position;

        targetPos.z = 0f;
        targetPos.x = targetPos.x - transform.position.x;
        targetPos.y = targetPos.y - transform.position.y;

        float x = targetPos.x / (Mathf.Abs(targetPos.x) + Mathf.Abs(targetPos.y));
        float y = targetPos.y / (Mathf.Abs(targetPos.x) + Mathf.Abs(targetPos.y));

        _direction = new Vector2(x, y);

        //Высчитывание угла для поворота спрайта по направлению движения самого объекта
        float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
        angle = flipRot ? -angle : angle;
        rotationTarget.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
    }

    private void FixedUpdate()
    {
        //Движение
        transform.Translate(_direction * _moveSpeed);

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Если направление почти по прямой то задаются случайные значения направления
            if (Mathf.Abs(_direction.x) > 0.9f || Mathf.Abs(_direction.y) > 0.9f)
            {
                if (Mathf.Abs(_direction.x) > 0.9f)
                {
                    float x = _direction.x < 0 ? Random.Range(0, 1f) : Random.Range(-1f, 0);
                    int sign = Random.Range(0, 2);
                    if (sign == 0)
                        sign = -1;
                    float y = sign * (1 - Mathf.Abs(x));
                    _direction = new Vector2(x, y);
                }
                if (Mathf.Abs(_direction.y) > 0.9f)
                {
                    float y = _direction.y < 0 ? Random.Range(0, 1f) : Random.Range(-1f, 0);
                    int sign = Random.Range(0, 2);
                    if (sign == 0)
                        sign = -1;
                    float x = sign * (1 - Mathf.Abs(y));
                    _direction = new Vector2(x, y);
                }

                //Высчитывание угла для поворота спрайта по направлению движения самого объекта
                float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
                angle = flipRot ? -angle : angle;
                rotationTarget.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
            }
            //Отскок от стены под тем же углом
            else
            {
                Vector2 inDirection = _direction;
                Vector2 inNormal = collision.contacts[0].normal;
                _direction = Vector2.Reflect(inDirection, inNormal);
                //Высчитывание угла для поворота спрайта по направлению движения самого объекта
                float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
                angle = flipRot ? -angle : angle;
                rotationTarget.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
            }
        }
    }
}
