using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay = 5f;


    private Animator anim;
    private float restartTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger(AnimatorParameters.GameManager.GameOver);
            restartTimer += Time.deltaTime;
            if(restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
