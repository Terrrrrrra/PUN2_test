using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillQChild : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == true) { return; }

        if (collision.tag == "Enemy") { collision.gameObject.SetActive(false); }
    }
}
