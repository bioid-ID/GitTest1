using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("ХыЧе UI")]
    public GameObject gameUI; 

    private GameObject startUI;
    private GameObject gameOverUI;
    private Slider hpSlider;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (gameUI != null)
        {
            Transform startTransform = gameUI.transform.Find("GameStartUI");
            Transform overTransform = gameUI.transform.Find("GameOverUI");
            Transform hpTransform = gameUI.transform.Find("HP_Bar");

            if (startTransform != null) startUI = startTransform.gameObject;
            if (overTransform != null) gameOverUI = overTransform.gameObject;
            if (hpTransform != null) hpSlider = hpTransform.GetComponent<Slider>();
        }

        Time.timeScale = 0f;
        if (startUI != null) startUI.SetActive(true);
        if (gameOverUI != null) gameOverUI.SetActive(false);
    }

    public void GameStart()
    {
        Time.timeScale = 1f;
        if (startUI != null) startUI.SetActive(false);
    }

    public void UpdateHpBar(int currentHp, int maxHp)
    {
        if (hpSlider != null)
        {
            hpSlider.value = (float)currentHp / maxHp;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverUI != null) gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
