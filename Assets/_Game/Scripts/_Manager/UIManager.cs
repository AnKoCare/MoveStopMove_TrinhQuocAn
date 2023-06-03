///
/// Create by linh soi - Abi Game studio
/// mentor Minh tito - CTO Abi Game studio
/// 
/// Manage list UI canvas for easy to use
/// Member nen inherit UI canvas
/// 
/// Update: 09-10-2020 
///             manage UI with Generic
///         09-10-2021 
///             Open, Close UI with Typeof(T)
///         28/11/2022
///             Close All UI
///             Close delay time
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    //dict for quick query UI prefab
    //dict dung de lu thong tin prefab canvas truy cap cho nhanh
    private Dictionary<UIID, UICanvas> uiCanvasPrefab = new();

    //list from resource
    //list load ui resource
    private UICanvas[] uiResources;

    //dict for UI active
    //dict luu cac ui dang dung
    private Dictionary<UIID, UICanvas> uiCanvas = new();

    //canvas container, it should be a canvas - root
    //canvas chua dung cac canvas con, nen la mot canvas - root de chua cac canvas nay
    public Transform CanvasParentTF;

    #region Canvas

    public override void OnInit()
    {
        UICanvas[] uiResources = Resources.LoadAll<UICanvas>("UI/");
        for (int i = 0; i < uiResources.Length; i++)
        {
            uiCanvasPrefab.Add(uiResources[i].ID, uiResources[i]);
        }
    }
    private void Start()
    {
        OpenUI(UIID.MainMenu);
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    //open UI
    //mo UI canvas
    public UICanvas OpenUI(UIID ID) 
    {
        UICanvas canvas = GetUI(ID);

        //canvas.Setup(ID);
        canvas.Open();

        return canvas;
    }

    //close UI directly
    //dong UI canvas ngay lap tuc
    public void CloseUI(UIID ID)
    {
        if (IsOpened(ID))
        {
            GetUI(ID).CloseDirectly();
        }
    }   
    
    //close UI with delay time
    //dong ui canvas sau delay time
    public void CloseUI(UIID ID,float delayTime) 
    {
        if (IsOpened(ID))
        {
            GetUI(ID).Close(delayTime);
        }
    }

    //check UI is Opened
    //kiem tra UI dang duoc mo len hay khong
    public bool IsOpened(UIID ID) 
    {
        return IsLoaded(ID) && uiCanvas[ID].gameObject.activeInHierarchy;
    }

    //check UI is loaded
    //kiem tra UI da duoc khoi tao hay chua
    public bool IsLoaded(UIID ID)
    {      
        return uiCanvas.ContainsKey(ID) && uiCanvas[ID] != null;
    }

    //Get component UI 
    //lay component cua UI hien tai

    public T GetUI<T>(UIID ID) where T : UICanvas
    { 
        if (!IsLoaded(ID))
        {
            UICanvas canvas = Instantiate(GetUIPrefab(ID), CanvasParentTF);
            uiCanvas[canvas.ID] = canvas;
        }
        return uiCanvas[ID] as T;
    }
    
    public UICanvas GetUI(UIID ID)
    { 
        if (!IsLoaded(ID))
        {
            UICanvas canvas = Instantiate(GetUIPrefab(ID), CanvasParentTF);
            uiCanvas[canvas.ID] = canvas;
        }
        return uiCanvas[ID];
    }
    

    //Close all UI
    //dong tat ca UI ngay lap tuc -> tranh truong hop dang mo UI nao dong ma bi chen 2 UI cung mot luc
    public void CloseAll()
    {
        foreach (var item in uiCanvas)
        {
            if (item.Value != null && item.Value.gameObject.activeInHierarchy)
            {
                item.Value.CloseDirectly();
            }
        }
    }

    //Get prefab from resource
    //lay prefab tu Resources/UI 
    private UICanvas GetUIPrefab(UIID ID)
    {
        if (!uiCanvasPrefab.ContainsKey(ID))
        {
            if (uiResources == null)
            {
                uiResources = Resources.LoadAll<UICanvas>("UI/");
            }

            for (int i = 0; i < uiResources.Length; i++)
            {
                if (uiResources[i])
                {
                    uiCanvasPrefab[ID] = uiResources[i];
                    break;
                }
            }
        }

        return uiCanvasPrefab[ID];
    }


    #endregion

    #region Back Button

    private Dictionary<UICanvas, UnityAction> BackActionEvents = new Dictionary<UICanvas, UnityAction>();
    private List<UICanvas> backCanvas = new List<UICanvas>();
    UICanvas BackTopUI {
        get
        {
            UICanvas canvas = null;
            if (backCanvas.Count > 0)
            {
                canvas = backCanvas[backCanvas.Count - 1];
            }

            return canvas;
        }
    }


    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Escape) && BackTopUI != null)
        {
            BackActionEvents[BackTopUI]?.Invoke();
        }
    }

    public void PushBackAction(UICanvas canvas, UnityAction action)
    {
        if (!BackActionEvents.ContainsKey(canvas))
        {
            BackActionEvents.Add(canvas, action);
        }
    }

    public void AddBackUI(UICanvas canvas)
    {
        if (!backCanvas.Contains(canvas))
        {
            backCanvas.Add(canvas);
        }
    }

    public void RemoveBackUI(UICanvas canvas)
    {
        backCanvas.Remove(canvas);
    }

    /// <summary>
    /// CLear backey when comeback index UI canvas
    /// </summary>
    public void ClearBackKey()
    {
        backCanvas.Clear();
    }

    #endregion
}
