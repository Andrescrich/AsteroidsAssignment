using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text score;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    private void Awake() => Instance = this;

    public void ModifyHealthVisual(int amount)
    {
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        if (amount == 2)
            life3.SetActive(false);
        else if (amount == 1)
        {
            life3.SetActive(false);
            life2.SetActive(false);
        }
        else if (amount == 0)
        {
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(false);
        }
    }

    public void ModifyScoreVisual(int amount)
    {
        score.text = amount.ToString();
    }
}
