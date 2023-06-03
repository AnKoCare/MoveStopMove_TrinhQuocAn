using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CvGameplay : UICanvas
{
    [SerializeField] private Transform NoticeHold;
    [SerializeField] private NoticeKiller PrefabsNotice;
    private void Update() 
    {
        
    }

    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.Gameplay);
        CameraFollow.Ins.SetupGamePlay();
    }

    public void ButtonSettingGame()
    {
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI(UIID.UIPauseGame);
    }

    public void SpawnNotice(string killer, string victim)
    {
        NoticeKiller noticeKiller = SimplePool.Spawn<NoticeKiller>(PoolType.NoticeKiller);
        noticeKiller.nameKiller.text = killer;
        noticeKiller.nameVictim.text = victim;
        noticeKiller.transform.SetParent(NoticeHold);
    }
}
