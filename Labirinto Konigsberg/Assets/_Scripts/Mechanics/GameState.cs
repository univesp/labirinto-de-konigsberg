using UnityEngine;

public class GameState : MonoBehaviour
{
    //Variáveis de controle
    [SerializeField] private int tries;
    private int bridgeCount = 0;

    //Referências dos objetos que mudam de acordo com a jogada
    [SerializeField] private GameObject timerScreen;
    [SerializeField] private GameObject bridgeObject;

    //Telas iniciais
    [SerializeField] private GameObject firstStart;
    [SerializeField] private GameObject secondStart;
    [SerializeField] private GameObject thirdStart;

    //Telas finais
    [SerializeField] private GameObject firstEnding;
    [SerializeField] private GameObject secondEnding;
    [SerializeField] private GameObject thirdEndingWrong;
    [SerializeField] private GameObject thirdEndingTrue;

    //Música final
    [SerializeField] private AudioClip _finalBGM;

    public static GameState Instance;

    private void Awake()
    {
        Instance = this;
        tries = PlayerPrefs.GetInt("tries_total", 0);
    }

    private void Start()
    {
        StartGameText();
    }

    private void StartGameText()
    {
        switch (tries)
        {
            case 0:
                firstStart.SetActive(true);
                break;

            case 1:
                secondStart.SetActive(true);
                break;

            case 2:
                thirdStart.SetActive(true);
                break;
        }
    }

    public void StartGame()
    {
        switch (tries)
        {
            case 1:
                //Inicia o jogo com um timer de 30 segundos
                timerScreen.SetActive(true);
                break;

            case 2:
                //Inicia o jogo sem a ponte central
                bridgeObject.SetActive(false);
                break;
        }
    }

    public void EndGame()
    {
        AudioPlayer.Instance.PlayBGM(_finalBGM);
        switch(tries)
        {
            case 0:
                firstEnding.SetActive(true);
                tries = 1;
                PlayerPrefs.SetInt("tries_total", tries);
                break;

            case 1:
                secondEnding.SetActive(true);
                tries = 2;
                timerScreen.SetActive(false);
                PlayerPrefs.SetInt("tries_total", tries);
                break;

            case 2:
                if (bridgeCount == 6)
                {
                    thirdEndingTrue.SetActive(true);
                    PlayerPrefs.SetInt("tries_total", 0);
                }
                else
                {
                    thirdEndingWrong.SetActive(true);
                }
                break;
        }
    }

    public void AddBridgeCount()
    {
        bridgeCount++;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("tries_total");
    }
}
