using UnityEngine;

public class ImpMovement : MonoBehaviour
{
    public Vector2 _direction;
    public float _moveSpeed = 0.1f;
    public Transform player;

    private void Start()
    {
        //¬ысчитываетс€ положение Player и задаетс€ направление движени€ Imp'а в его направлении
        Vector3 targetPos = player.position;

        targetPos.z = 0f;
        targetPos.x = targetPos.x - transform.position.x;
        targetPos.y = targetPos.y - transform.position.y;

        float x = targetPos.x / (Mathf.Abs(targetPos.x) + Mathf.Abs(targetPos.y));
        float y = targetPos.y / (Mathf.Abs(targetPos.x) + Mathf.Abs(targetPos.y));

        _direction = new Vector2(x, y);
        Rotate(_direction.x);
    }

    private void FixedUpdate()
    {
        //ƒвижение
        transform.Translate(_direction * _moveSpeed);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //≈сли направление почти по пр€мой то задаютс€ случайные значени€ направлени€
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
                    Rotate(_direction.x);
                }
                if (Mathf.Abs(_direction.y) > 0.9f)
                {
                    float y = _direction.y < 0 ? Random.Range(0, 1f) : Random.Range(-1f, 0);
                    int sign = Random.Range(0, 2);
                    if (sign == 0)
                        sign = -1;
                    float x = sign * (1 - Mathf.Abs(y));
                    _direction = new Vector2(x, y);
                    Rotate(_direction.x);
                }

            }
            //ќтскок от стены под тем же углом
            else
            {
                Vector2 inDirection = _direction;
                Vector2 inNormal = collision.contacts[0].normal;
                _direction = Vector2.Reflect(inDirection, inNormal);
                Rotate(_direction.x);
            }
        }
    }

    void Rotate(float x)
    {
        Vector3 scaler = transform.localScale;
        if (x > 0)
        {
            scaler.x = Mathf.Abs(scaler.x);
            transform.localScale = scaler;
        }
        else
        {
            scaler.x = -Mathf.Abs(scaler.x);
            transform.localScale = scaler;
        }
    }
}
