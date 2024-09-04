using UnityEngine;

public class MainCamera : MonoBehaviour
{
    GameObject _player;     // 찾은 플레이어를 저장하는 변수
    Transform _playerTransform; // 플레이어 위치정보
    Vector3 _smoothCamera;  // SmoothDamp 메소드 참조용(ref) 변수
    float _searchCooltime;  // 탐색 주기
    float _searchCooldown;  // 남은 탐색 쿨타임

    private void Awake()
    {
        _searchCooltime = 1; // 1초에 한번 찾기
    }

    void FixedUpdate()
    {
        if (_player == null)
        {
            // 플레이어를 못찾은 상태일때만 실행
            // 일정 주기로 플레이어를 찾음
            SearchingPlayer();
        }
    }

    private void Update()
    {
        if (_player != null)
        {
            // 플레이어를 찾은 상태일때만 실행
            // 매 프레임마다 카메라가 플레이어를 부드럽게 추적
            transform.position = Vector3.SmoothDamp(transform.position, _playerTransform.position + Vector3.back * 10, ref _smoothCamera, 0.1f);
        }
    }

    private void SearchingPlayer()
    {
        // FixedUpdate가 실행된 간격만큼 남은 쿨타임에 더함
        _searchCooldown += Time.fixedDeltaTime;



        // 쿨타임이 다 안차면 리턴
        if (_searchCooltime > _searchCooldown) { return; }

        // 이 줄에 진입시 쿨타임 초기화
        _searchCooldown = 0;

        // 플레이어를 찾아서 할당, 못찾으면 null 할당
        _player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 게임오브젝트를 찾으면 할당

        if (_player != null)
        {
            // 할당 성공시 Transform 정보 호출해서 저장
            _playerTransform = _player.GetComponent<Transform>();
        }
    }
}
