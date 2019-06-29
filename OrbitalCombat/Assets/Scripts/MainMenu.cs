using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject menuParent;
    public GameObject creditsParent;

    public GameObject startButton;
    public GameObject backButtonCredits;
    public GameObject creditsButton;
    public Text[] creditsText;

    private string[] names = new string[6]
    {
        "Carl-Philipp Hellmuth",
        "Michael Hochmuth",
        "Jan Milosch",
        "Marius Muehleck",
        "Daniel Schulz",
        "Ilona Treml"
    };
      
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void StartGame ()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToCredits()
    {
        EventSystem.current.SetSelectedGameObject(null);
        menuParent.SetActive(false);
        creditsParent.SetActive(true);
        Shuffle(names);
        for (int g = 0; g < names.Length; g++)
        {
            creditsText[g].text = names[g];
        }
        backButtonCredits.GetComponent<Animator>().SetTrigger("Normal");
        EventSystem.current.SetSelectedGameObject(backButtonCredits);
    }

    public void GoBackFromCredits()
    {
        EventSystem.current.SetSelectedGameObject(null);
        creditsParent.SetActive(false);
        menuParent.SetActive(true);
        creditsButton.GetComponent<Animator>().SetTrigger("Normal");
        EventSystem.current.SetSelectedGameObject(creditsButton);
    }

    private void Shuffle (string[] texts)
    {
        for (int t = 0; t < texts.Length; t++)
        {
            string tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }
}