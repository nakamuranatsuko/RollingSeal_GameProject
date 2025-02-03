using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IceBreak : MonoBehaviour
{
    GameObject Player;
    Player_Ishiyama _script;

    public UnityEvent OnDestoryed = new UnityEvent();

    private void Start()
    {
        Player = GameObject.Find("Player");
        _script = Player.GetComponent<Player_Ishiyama>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        int PlayerSpeed = _script.Speed;

        if (PlayerSpeed >= 2)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        Debug.Log("iceBreak");
        OnDestoryed.Invoke();
    }
}
