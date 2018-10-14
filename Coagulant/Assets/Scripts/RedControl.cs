using UnityEngine;

public class RedControl : MonoBehaviour
{
    public GameObject White;
    public Transform BruiseAreaA;
    public Transform BruiseAreaB;
    public bool _isCollected;
    public bool _isSaved;
    public Sprite[] Sprites;

    private WhiteControl _WhiteControl;

    void Start()
    {
        _WhiteControl = White.GetComponent<WhiteControl>();
        GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length)];
    }

    void Update()
    {
        if (!_isSaved)
        {
            if (_isCollected)
            {
                transform.position = White.transform.position + new Vector3(.5f, -.9f, 0);
            }

            if (transform.position.x > BruiseAreaA.position.x && transform.position.x < BruiseAreaB.position.x &&
                transform.position.y < BruiseAreaA.position.y && transform.position.y > BruiseAreaB.position.y)
            {
                _isSaved = true;
                _WhiteControl.DropRed();
                AudioManager.Instance.PlaySfxRedShout();
                GameManager.Instance.AddRedSaved();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isSaved)
        {
            if (collision.CompareTag("White"))
            {
                if (_WhiteControl.CanCollect())
                {
                    _WhiteControl.CollectRed(gameObject);
                    _isCollected = true;
                    AudioManager.Instance.PlaySfxRedShout();
                }
            }
        }
    }



}