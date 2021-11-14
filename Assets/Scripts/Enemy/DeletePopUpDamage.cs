using UnityEngine;

public class DeletePopUpDamage : MonoBehaviour
{
     void TextDestroy()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
