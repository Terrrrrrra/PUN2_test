using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] ObjectPool _objectPool;
    [SerializeField] Transform _playerTransform;

    GameObject _curEnemy;

    [SerializeField] float _spawnCoolTime;
    float _spawnCoolDown;

    private void FixedUpdate()
    {
        EnemySpawn();
    }

    private void EnemySpawn()
    {
        // 일정시간마다 생성 판단
        _spawnCoolDown += Time.fixedDeltaTime;

        if (_spawnCoolDown < _spawnCoolTime) { return; }

        _spawnCoolDown = 0;

        // 오브젝트풀에서 추출
        _curEnemy = _objectPool.GetObject("enemy");

        if (_curEnemy == null) { return; }

        // 조건에 맞게 스폰
        _curEnemy.transform.position = (Vector2)_playerTransform.position + GetRandomPos();
        _curEnemy.SetActive(false);
        _curEnemy.SetActive(true);
    }

    Vector2 GetRandomPos()
    {
        // 범위내 랜덤한 위치 산정

        float Num1 = Random.Range(15f, 20f);

        float Num2 = Random.Range(0, 360f);

        Vector2 pos = Quaternion.Euler(0, 0, Num2) * Vector2.up;

        return pos.normalized * Num1;
    }
}
