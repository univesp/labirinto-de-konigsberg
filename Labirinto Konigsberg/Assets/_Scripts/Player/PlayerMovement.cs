using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //Variáveis do movimento
    private Transform[] _waypoints;
    private int _index = 0;

    private float _movementSpeed = 6;
    private bool _canMove;

    private float _secondaryIdleTimer = 6;

    //Variáveis da rotação
    private Quaternion _rotateAngle;
    private float _rotationSpeed = 150;

    //Waypoints iniciais
    [SerializeField] private Transform _upWaypoint;
    [SerializeField] private Transform _upWaypoint2;
    [SerializeField] private Transform _upWaypoint3;
    [SerializeField] private Transform _downWaypoint;

    //Estados do player
    [SerializeField] private GameObject _idle;
    [SerializeField] private GameObject _secondaryIdle;
    [SerializeField] private GameObject _walking;

    private bool isFirstTime = true;

    public static PlayerMovement Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Move();
    }

    public void SetWaypointsAndStartMoving(Transform[] newWaypoints)
    {
        _waypoints = newWaypoints;
        _canMove = true;
    }

    private void Move()
    {
        if(_canMove)
        {
            //Se for a primeira vez, deixa o jogador no primeiro waypoint
            if (isFirstTime)
            {
                if (_waypoints[0].localPosition.z > 50)
                {
                    if (_waypoints[0].localPosition.x < -10)
                    {
                        transform.localPosition = _upWaypoint.position;
                    }
                    else if(_waypoints[0].localPosition.x > -10 && _waypoints[0].localPosition.x < 5)
                    {
                        transform.localPosition = _upWaypoint2.position;
                    }else
                    {
                        transform.localPosition = _upWaypoint3.position;
                    }
                }
                else
                {
                    transform.localPosition = _downWaypoint.position;
                }
                //transform.localPosition = _waypoints[0].localPosition;
                isFirstTime = false;
            }

            //Ativa animação de movimento
            _idle.SetActive(false);
            _secondaryIdle.SetActive(false);
            _walking.SetActive(true);
            //Movimento
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _waypoints[_index].localPosition, _movementSpeed * Time.deltaTime);
            //Rotação
            _rotateAngle = Quaternion.LookRotation(_waypoints[_index].localPosition - gameObject.transform.localPosition);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _rotateAngle, _rotationSpeed * Time.deltaTime);

            //Anda pelos elementos do Waypoint
            if (Vector3.Distance(transform.localPosition, _waypoints[_index].localPosition) <= 0.05f)
            {
                //Ao chegar no final do caminho, ele rotaciona de acordo com o último waypoint e vai para o próximo
                _index++;                
                
                //Se terminar de andar por todos, ele reinicia as pontes e as variáveis
                if (_index >= _waypoints.Length)
                {
                    _index = 0;
                    _canMove = false;
                    BridgesCentral.Instance.StartNextChoice();
                    _idle.SetActive(true);
                    _secondaryIdle.SetActive(false);
                    _walking.SetActive(false);
                }
            }
        }
        else
        {
            _secondaryIdleTimer -= Time.deltaTime;
            
            if(_secondaryIdleTimer > 0 && _secondaryIdleTimer < 4)
            {
                _idle.SetActive(true);
                _secondaryIdle.SetActive(false);
                _walking.SetActive(false);
            }

            if(_secondaryIdleTimer <= 0)
            {
                _idle.SetActive(false);
                _secondaryIdle.SetActive(true);
                _walking.SetActive(false);
                _secondaryIdleTimer = 6;
            }
        }
    }
}
