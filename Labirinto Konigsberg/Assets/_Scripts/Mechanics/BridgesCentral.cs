using UnityEngine;
using UnityEngine.UI;

public class BridgesCentral : MonoBehaviour
{
    //Delegate que guarda o método de desativar pontes
    public delegate void BridgeOperations();
    public BridgeOperations BridgeDeactivatorDelegate;

    //Variáveis da ponte selecionada
    [SerializeField] private Bridge _currentBridge;

    //Variáveis próprios desse método
    private int _islandDestination;

    public static BridgesCentral Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCurrentBridge(Bridge currentBridge)
    {
        _currentBridge = currentBridge;

        BridgeDeactivatorDelegate();
        _islandDestination = _currentBridge.GetDestination(_islandDestination);
        PlayerMovement.Instance.SetWaypointsAndStartMoving(_currentBridge.GetWaypoints(_islandDestination));
    }

    public void StartNextChoice()
    {
        _currentBridge.BreakBridge();
        _currentBridge.ActivateBridges(_islandDestination);   
    }
}