using System.Collections;
using UnityEngine;

public class SkillQ : MonoBehaviour
{
    Transform _skillQTransform;

    [SerializeField] GameObject _skillQSprite;

    Vector2 _skillQShotDirection;
    float _skillShotAngle;

    private void Awake()
    {
        _skillQTransform = GetComponent<Transform>();
    }

    void Update()
    {
        SkillQUse();
    }

    private void SkillQUse()
    {
        // q를 눌렀을때만 진입
        if (!Input.GetKeyDown(KeyCode.Q)) { return; }

        // 발사 방향 계산
        _skillQShotDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _skillQTransform.position;

        // 발사 각도 계산
        _skillShotAngle = Mathf.Rad2Deg * Mathf.Acos(Vector2.Dot(Vector2.up, (_skillQShotDirection).normalized));

        // 반전되는 각도 재조정
        if (_skillQShotDirection.x > 0) { _skillShotAngle = 360 - _skillShotAngle; }

        // 해당 방향으로 각도조정
        _skillQTransform.rotation = Quaternion.Euler(0, 0, _skillShotAngle);

        // 잠시 노출
        StopAllCoroutines();
        StartCoroutine(ShowSkillQ());

        IEnumerator ShowSkillQ()
        {
            _skillQSprite.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            _skillQSprite.SetActive(false);
        }
    }
}
