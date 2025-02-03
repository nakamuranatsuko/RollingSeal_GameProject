using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishAnim : MonoBehaviour
{
    private Animator anim;

    public IceBreak target;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnDisable()
    {
        target.OnDestoryed.RemoveAllListeners();
    }

    private void OnEnable()
    {
        target.OnDestoryed.AddListener(() =>
        {
            anim.SetBool("Bool", true);
            Invoke(nameof(DelayMethod), 1.0f);
        });
    }

    void DelayMethod()
    {
        Destroy(gameObject);
        Debug.Log("GoldFishGet");
    }
}
