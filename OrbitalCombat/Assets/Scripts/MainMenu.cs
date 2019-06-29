using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject menuParent;
    public GameObject creditsParent;
    public GameObject optionsParent;
    public Slider playerSlider;

    public GameObject startButton;
    public GameObject backButtonCredits;
    public GameObject backButtonOptions;
    public GameObject creditsButton;
    public GameObject optionsButton;
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

    public void GoToOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        menuParent.SetActive(false);
        optionsParent.SetActive(true);
        backButtonOptions.GetComponent<Animator>().SetTrigger("Normal");
        EventSystem.current.SetSelectedGameObject(backButtonOptions);
    }

    public void GoBackFromOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        optionsParent.SetActive(false);
        menuParent.SetActive(true);
        optionsButton.GetComponent<Animator>().SetTrigger("Normal");
        EventSystem.current.SetSelectedGameObject(optionsButton);
    }

    public void SetNumberOfPlayers ()
    {
        switch (playerSlider.value)
        {
            case 0:
                // TODO: Set player number to two
                Debug.Log("2 players!");
                break;
            case 1:
                // TODO: Set player number to four
                Debug.Log("4 players!");
                break;
        } 
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