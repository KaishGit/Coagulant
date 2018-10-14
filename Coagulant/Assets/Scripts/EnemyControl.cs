using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public GameObject DirectionShot;
    public GameObject EnemyShot;
    public GameObject White;
    public float RangeToWhite;
    public float SpeedShot;
    public float Life;
    public float Speed;
    public Slider SliderLife;

    private bool _InAttack;
    private bool _CanAttack;
    private bool _InRecovery;
    private float CurrentMaxLife;
    private SpriteRenderer _SpriteRenderer;
    private Animator _Animator;
    private Collider2D _Collider2D;
    private Coroutine _CurrentAttack;

    void Start()
    {
        SliderLife.maxValue = Life;
        UpdateSliderLife();
        CurrentMaxLife = Life;
        _InAttack = false;
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Animator = GetComponent<Animator>();
        _Collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!_InAttack && !_InRecovery)
        {
            VerifyPosition();
        }

        VerifyLife();
    }

    private void FixedUpdate()
    {
        if (_InAttack && !_InRecovery)
        {
            Move();
            Rotation();
            if (_CanAttack)
            {
                _CurrentAttack = StartCoroutine(Attack());
                _CanAttack = false;
            }
        }
    }

    private void Rotation()
    {
        Vector2 direcao = White.transform.position - DirectionShot.transform.position;
        float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        Quaternion novaRotacao = Quaternion.AngleAxis(angulo - 90, Vector3.forward);
        DirectionShot.transform.rotation = Quaternion.Slerp(DirectionShot.transform.rotation, novaRotacao, Time.deltaTime * 50);
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, White.transform.position) > RangeToWhite - 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, White.transform.position, Speed * Time.deltaTime);
        }

        if(transform.position.x > White.transform.position.x)
        {
            _SpriteRenderer.flipX = true;
        }
        else
        {
            _SpriteRenderer.flipX = false
                ;
        }
    }

    private void VerifyPosition()
    {
        if (Vector2.Distance(transform.position, White.transform.position) < RangeToWhite)
        {
            _InAttack = true;
            _CanAttack = true;
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(SpeedShot);
        Instantiate(EnemyShot, DirectionShot.transform.position, DirectionShot.transform.rotation);
        AudioManager.Instance.PlaySfxShotSound();
        _CanAttack = true;
    }

    private void ActiveRecovery()
    {
        _InRecovery = true;
        CurrentMaxLife = CurrentMaxLife * 1.5f;
        SpeedShot = SpeedShot / 1.25f;
        StartCoroutine(WaitingRecovery());
    }

    private IEnumerator WaitingRecovery()
    {
        yield return new WaitForSeconds(10);
        _Animator.Play("Idle");
        Life = CurrentMaxLife;
        SliderLife.maxValue = Life;
        _InRecovery = false;
        UpdateSliderLife();
        _Collider2D.enabled = true;
        transform.localScale = transform.localScale * 1.1f;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, RangeToWhite);
    //}

    private void VerifyLife()
    {
        if (Life == 0 && !_InRecovery)
        {
            StopCoroutine(_CurrentAttack);
            AudioManager.Instance.PlaySfxEnemyDeath();
            _InAttack = false;
            _CanAttack = false;
            _Animator.Play("EnemyDeath");           
            ActiveRecovery();
            _Collider2D.enabled = false;
        }
    }

    public void CauseDamage(float damage)
    {
        Life -= damage;

        if (Life < 0)
            Life = 0;

        UpdateSliderLife();
    }

    private void UpdateSliderLife()
    {
        SliderLife.value = Life;
    }
}