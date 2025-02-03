using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FightStarter : MonoBehaviour
{
    [SerializeField] private Sun _boss;
    [SerializeField] private GameObject _blockingBarrier;
    [SerializeField] private CameraMover _defaultCameraMover;
    [SerializeField] private BossCameraMover _bossCameraMover;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _defaultCameraMover.enabled = false;
            _bossCameraMover.enabled = true;
            _blockingBarrier.SetActive(true);
            _boss.StartFight();

            Destroy(gameObject);
        }
    }
}
