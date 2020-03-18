using UnityEngine;
using UnityEngine.UI;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;

    [SerializeField] private int _innerIsland;
    [SerializeField] private int _outerIsland;    

    [SerializeField] private Button[] _innerBridges;
    [SerializeField] private Button[] _outerBridges;

    [SerializeField] private Image _bridgeImage;
    [SerializeField] private Button _bridgeButton;

    public SpriteState _brokenSpriteState;

    [SerializeField] private AudioClip _breakSFX;

    private void Start()
    {
        //Chama a central das pontes e salva os métodos nos delegates
        BridgesCentral.Instance.BridgeDeactivatorDelegate += DeactivateBridge;
    }

    //Ativa as pontes de acordo com o destino do jogador. Ele pula as pontes quebradas
    public void ActivateBridges(int islandDestination)
    {
        bool isGameOver = true;

        if(islandDestination == _outerIsland)
        {
            for (int i = 0; i < _outerBridges.Length; i++)
            {
                if (_outerBridges[i].spriteState.disabledSprite != _outerBridges[i].GetComponent<Bridge>()._brokenSpriteState.disabledSprite)
                {
                    _outerBridges[i].interactable = true;
                    isGameOver = false;
                }
            }
        }else if(islandDestination == _innerIsland)
        {
            for (int i = 0; i < _innerBridges.Length; i++)
            {                
                if (_innerBridges[i].spriteState.disabledSprite != _innerBridges[i].GetComponent<Bridge>()._brokenSpriteState.disabledSprite)
                {
                    _innerBridges[i].interactable = true;
                    isGameOver = false;
                }
            }
        }

        if(isGameOver)
        {
            //Chama o fim de jogo
            GameState.Instance.EndGame();
        }
    }

    //Desativa a ponte
    public void DeactivateBridge()
    {
        _bridgeButton.interactable = false;
    }

    //Desativa a ponte definitivamente
    public void BreakBridge()
    {
        //Toca o som de quebrar
        AudioPlayer.Instance.PlaySFX(_breakSFX);

        //Muda a ponte desativada e a desativa
        _bridgeButton.spriteState = _brokenSpriteState;
        _bridgeButton.interactable = false;

        //Grava essa ponte no estado do jogo
        GameState.Instance.AddBridgeCount();

        //Tira essa ponte dos delegates
        BridgesCentral.Instance.BridgeDeactivatorDelegate -= DeactivateBridge;
    }

    //Retorna os waypoints que o jogador vai percorrer
    public Transform[] GetWaypoints(int islandDestination)
    {
        if(islandDestination == _innerIsland)
        {
            return _waypoints;
        }
        else
        {
            Transform[] reversedWaypoint = new Transform[_waypoints.Length];

            for(int i = _waypoints.Length - 1, j = 0; i >= 0; i--, j++)
            {
                reversedWaypoint[j] = _waypoints[i];
                //Rotaciona o caminho em 180 para passar a rotação certa para o jogador
                reversedWaypoint[j].localRotation *= Quaternion.Euler(0, 180.0f, 0);
            }

            return reversedWaypoint;
        }
    }

    public int GetDestination(int currentIsland)
    {
        //Retorna a ilha de destino do jogador
        if(currentIsland == _innerIsland)
        {
            return _outerIsland;
        }
        else
        {
            return _innerIsland;
        }
    }
}