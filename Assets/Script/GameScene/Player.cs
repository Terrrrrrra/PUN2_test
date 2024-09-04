using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

    Vector2 _targetPos;
    Rigidbody2D _playerRigid;

    private void Awake()
    {
        _playerRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateTargetPos();
    }

    private void UpdateTargetPos()
    {
        // 우클릭을 했을때에만 진입
        if (!Input.GetKey(KeyCode.Mouse1)) { return; }

        // 클릭한 지점으로 목표 위치 변경
        _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 playerPos = _playerRigid.position;

        // 내 위치가 목표위치와 떨어져있다면 이동
        if (Vector2.Distance(playerPos, _targetPos) < 0.1) { return; }

        _playerRigid.MovePosition(_playerRigid.position + (_targetPos - playerPos).normalized * _moveSpeed * Time.fixedDeltaTime);
    }
}
