using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WhiteControl : MonoBehaviour
{
    public float Speed;
    public GameObject Canhao;
    public GameObject WhiteShot;
    public float Life;
    public Slider SliderLife;
    public Transform RecoverTL;
    public Transform RecoverBR;

    private SpriteRenderer _SpriteRenderer;
    private Animator _Animator;
    private GameObject _Red;
    private bool _CanRecover;
    private bool _CanAttack;
    private bool _IsLive;

    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Animator = GetComponent<Animator>();
        SliderLife.maxValue = Life;
        UpdateSliderLife();
        _CanRecover = true;
        _CanAttack = true;
        _IsLive = true;
    }

    void Update()
    {
        if (_IsLive)
        {
            VerifyLife();
            VerifyRecover();
        }
    }

    private void FixedUpdate()
    {
        if (_IsLive)
        {
            Movement();
            Rotation();
            Attack();
        }
    }

    private void Movement()
    {

        if (Input.GetKey("w"))
        {
            transform.Translate(0, Speed * Time.deltaTime, 0);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(0, -Speed * Time.deltaTime, 0);
        }
        if (Input.GetKey("a"))
        {
            _SpriteRenderer.flipX = false;
            transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("d"))
        {
            _SpriteRenderer.flipX = true;
            transform.Translate(Speed * Time.deltaTime, 0, 0);
        }

    }

    private void Rotation()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direcao = mousePosition - Canhao.transform.position;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        Quaternion novaRotacao = Quaternion.AngleAxis(angulo - 90, Vector3.forward);
        Canhao.transform.rotation = Quaternion.Slerp(Canhao.transform.rotation, novaRotacao, Time.deltaTime * 100);

    }

    private void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            if (_CanAttack)
            {
                _CanAttack = false;
                StartCoroutine(_WaitingCanAttack());
                Instantiate(WhiteShot, Canhao.transform.position, Canhao.transform.rotation);
                AudioManager.Instance.PlaySfxShotSound();
            }

        }
    }

    public bool CanCollect()
    {
        if (_Red == null)
            return true;

        return false;
    }

    public void CollectRed(GameObject red)
    {
        _Red = red;
    }

    public void DropRed()
    {
        _Red = null;
    }

    private void VerifyLife()
    {
        if (Life == 0)
        {
            AudioManager.Instance.PlaySfxWhiteDeath();
            _IsLive = false;
            _Animator.Play("WhiteDeath");
            StartCoroutine(Restart());
        }
    }

    private void VerifyRecover()
    {
        if (transform.position.x > RecoverTL.position.x && transform.position.x < RecoverBR.position.x &&
                transform.position.y < RecoverTL.position.y && transform.position.y > RecoverBR.position.y)
        {
            if (_CanRecover)
            {
                _CanRecover = false;
                StartCoroutine(Recover());
            }
        }
    }

    public void CauseDamage(float damage)
    {
        if (_IsLive)
        {
            Life -= damage;

            _Animator.Play("WhiteDamage");

            if (Life < 0)
                Life = 0;

            UpdateSliderLife();
        }

    }

    public void AddLife(float extraLife)
    {
        if (Life < SliderLife.maxValue)
        {
            AudioManager.Instance.PlaySfxRecoverLife();
        }

        Life += extraLife;

        if (Life > SliderLife.maxValue)
            Life = SliderLife.maxValue;

        UpdateSliderLife();
    }

    private void UpdateSliderLife()
    {
        SliderLife.value = Life;
    }

    private IEnumerator _WaitingCanAttack()
    {
        yield return new WaitForSeconds(0.25f);
        _CanAttack = true;
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        UiManager.Instance.ShowPanelLose();
    }

    private IEnumerator Recover()
    {
        yield return new WaitForSeconds(1);
        AddLife(10);
        _CanRecover = true;
    }
}