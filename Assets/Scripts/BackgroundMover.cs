using UnityEngine;

class BackgroundMover : MonoBehaviour
{
    public SpriteRenderer background1;
    public SpriteRenderer background2;

    void Start()
    {
        background2.transform.Translate(new Vector3(background2.bounds.size.x, 0, 0));
    }
}