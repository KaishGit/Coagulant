using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //Musica
    [SerializeField] private AudioSource AudioSourceBackground;
    [SerializeField] private AudioClip[] ClipsBackground;
    //SFX            
    [SerializeField] private AudioSource AudioSourceSFX;
    [SerializeField] private AudioClip[] RedShout;
    [SerializeField] private AudioClip[] WhiteDeath;
    [SerializeField] private AudioClip[] ShotSound;
    [SerializeField] private AudioClip[] EnemyDeath;
    [SerializeField] private AudioClip[] RecoverLife;
    [SerializeField] private AudioClip ClickButton;
    [SerializeField] private AudioClip Win;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        AudioSourceBackground.clip = GetClipBackground();
        AudioSourceBackground.Play();
    }

    private AudioClip GetClipBackground()
    {
        return ClipsBackground[Random.Range(0, ClipsBackground.Length)];
    }

    public void PlaySfxRedShout()
    {
        AudioSourceSFX.PlayOneShot(RedShout[Random.Range(0, RedShout.Length)]);
    }

    public void PlaySfxWhiteDeath()
    {
        AudioSourceSFX.PlayOneShot(WhiteDeath[Random.Range(0, WhiteDeath.Length)]);
    }

    public void PlaySfxShotSound()
    {
        AudioSourceSFX.PlayOneShot(ShotSound[Random.Range(0, ShotSound.Length)]);
    }

    public void PlaySfxEnemyDeath()
    {
        AudioSourceSFX.PlayOneShot(EnemyDeath[Random.Range(0, EnemyDeath.Length)]);
    }

    public void PlaySfxRecoverLife()
    {
        AudioSourceSFX.PlayOneShot(RecoverLife[Random.Range(0, RecoverLife.Length)]);
    }

    public void PlaySfxClickButton()
    {
        AudioSourceSFX.PlayOneShot(ClickButton);
    }

    public void PlaySfxWin()
    {
        AudioSourceSFX.PlayOneShot(Win);
    }

}