using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text score;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    public GameObject gameOverText;

    public GameObject playButton;
    
    private void Awake() => Instance = this;

    public void UpdateHealthVisual()
    {
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        switch (GameManager.Instance.CurrentHealth)
        {
            case 2:
                life3.SetActive(false);
                break;
            case 1:
                life3.SetActive(false);
                life2.SetActive(false);
                break;
            case 0:
                life1.SetActive(false);
                life2.SetActive(false);
                life3.SetActive(false);
                gameOverText.SetActive(true);
                break;
        }
    }

    public void ModifyScoreVisual(int amount)
    {
        score.text = amount.ToString();
    }

    public void DeactivateButton()
    {
        playButton.SetActive(false);
    }
}
