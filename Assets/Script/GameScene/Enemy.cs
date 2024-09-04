using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

    Rigidbody2D _enemyRigid;
    Rigidbody2D _playerRigid;

    float _thinkCoolTime;
    float _thinkCoolDown;
    Vector2 _targetPos;
    bool _findPlayer;


    private void Awake()
    {
        _enemyRigid = GetComponent<Rigidbody2D>();
        SetThinkCoolTime();
    }

    private void OnEnable()
    {
        _findPlayer = false;
        _targetPos = _enemyRigid.position;
    }

    void FixedUpdate()
    {
        if (!_findPlayer)
        {
            MoveChoice(); // 플레이어 감지전엔 배회
        }
        else
        {
            UpdatePlayerPos(); // 플레이어 감지후엔 추적
        }
        Move();

        //ViewTargetPos();
    }

    private void ViewTargetPos()
    {
        Debug.DrawRay(_targetPos, Vector2.up, Color.yellow);
        Debug.DrawRay(_enemyRigid.position, Vector3.up);
    }

    private void MoveChoice()
    {
        // 다음 판단까지의 텀
        _thinkCoolDown += Time.fixedDeltaTime;

        // 쿨이 안돌았으면 대기
        if (_thinkCoolTime > _thinkCoolDown) { return; }

        // 쿨 초기화
        _thinkCoolDown = 0;

        // 다음 쿨타임 무작위 산정
        SetThinkCoolTime();

        // 확률적으로 움직이지 않거나 다음 움직일 위치를 정한다
        if (Random.Range(0, 1f) > 0.7f) { return; }

        // 다음 움직일 위치 산정
        _targetPos = _enemyRigid.position + GetRandomPos();
    }

    private void UpdatePlayerPos()
    {
        _targetPos = _playerRigid.position;
    }

    private void Move()
    {
        Vector2 enemyPos = _enemyRigid.position;

        // 내 위치가 목표위치와 떨어져있다면 이동
        //Debug.Log(Vector2.Distance(enemyPos, _targetPos));
        if (Vector2.Distance(enemyPos, _targetPos) < 0.1) { return; }

        // 등속이동
        _enemyRigid.MovePosition(enemyPos + (_targetPos - enemyPos).normalized * _moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 불필요한 연산 제거
        if (_findPlayer == false)
        {
            // 플레이어 감지시 정보 저장
            if (collision.tag == "Player")
            {
                _findPlayer = true;
                _playerRigid = collision.GetComponent<Rigidbody2D>();
            }
        }
    }

    void SetThinkCoolTime()
    {
        // 다음 쿨타임 세팅
        _thinkCoolTime = Random.Range(2f, 4f);
    }

    Vector2 GetRandomPos()
    {
        float Num1 = Random.Range(0, 2f);

        float Num2 = Random.Range(0, 360f);

        Vector2 pos = Quaternion.Euler(0, 0, Num2) * Vector2.up;

        return pos.normalized * Num1;
    }
}
