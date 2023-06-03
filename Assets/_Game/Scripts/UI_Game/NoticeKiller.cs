using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoticeKiller : GameUnit
{
    public TextMeshProUGUI nameKiller;
    public TextMeshProUGUI nameVictim;

    private void Start()
    {
        Invoke("DestroyNotice", 2f);
    }

    private void DestroyNotice()
    {
        SimplePool.Despawn(this);
    }

    public override void OnDespawn()
    {

    }

    public override void OnInit()
    {

    }
}
