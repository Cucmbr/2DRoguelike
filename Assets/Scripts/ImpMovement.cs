using UnityEngine;

public class ImpMovement : MonoBehaviour
{
    public Vector2 _direction;
    public float _moveSpeed;
    public bool flipRot = true;
    public Transform rotationTarget;
    public Transform player;

    private void Start()
    {
        //������������� ��������� Player � �������� ����������� �������� Imp'� � ��� �����������
        Vector3 targetPos = player.position;

        targetPos.z = 0f;
        targetPos.x = targetPos.x - transform.position.x;
        targetPos.y = targetPos.y - transform.position.y;

        float x = targetPos.x / (Mathf.Abs(targetPos.x) + Mathf.Abs(targetPos.y));
        float y = targetPos.y / (Mathf.Abs(targetPos.x) + Mathf.Abs(targetPos.y));

        _direction = new Vector2(x, y);

        //������������ ���� ��� �������� ������� �� ����������� �������� ������ �������
        float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
        angle = flipRot ? -angle : angle;
        rotationTarget.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
    }

    private void FixedUpdate()
    {
        //��������
        transform.Translate(_direction * _moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //���� ����������� ����� �� ������ �� �������� ��������� �������� �����������
            if (Mathf.Abs(_direction.x) > 0.9f || Mathf.Abs(_direction.y) > 0.9f)
            {
                if (Mathf.Abs(_direction.x) > 0.9f)
                {
                    float x = _direction.x < 0 ? Random.Range(0.1f, 0.9f) : Random.Range(-0.9f, -0.1f);
                    int sign = Random.Range(0, 2);
                    if (sign == 0)
                        sign = -1;
                    float y = sign * (1 - Mathf.Abs(x));
                    _direction = new Vector2(x, y);
                }
                if (Mathf.Abs(_direction.y) > 0.9f)
                {
                    float y = _direction.y < 0 ? Random.Range(0.1f, 0.9f) : Random.Range(-0.9f, -0.1f);
                    int sign = Random.Range(0, 2);
                    if (sign == 0)
                        sign = -1;
                    float x = sign * (1 - Mathf.Abs(y));
                    _direction = new Vector2(x, y);
                }

                //������������ ���� ��� �������� ������� �� ����������� �������� ������ �������
                float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
                angle = flipRot ? -angle : angle;
                rotationTarget.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
            }
            //������ �� ����� ��� ��� �� �����
            else
            {
                Vector2 inDirection = _direction;
                Vector2 inNormal = collision.contacts[0].normal;
                _direction = Vector2.Reflect(inDirection, inNormal);
                //������������ ���� ��� �������� ������� �� ����������� �������� ������ �������
                float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
                angle = flipRot ? -angle : angle;
                rotationTarget.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
            }
        }
    }
}
