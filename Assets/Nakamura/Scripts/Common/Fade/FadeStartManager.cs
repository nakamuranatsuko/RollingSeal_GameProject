using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeStartManager : MonoBehaviour
{
    private async void Start()
    {
        await FadeManager.Inctance.FadeIn();
    }
}
