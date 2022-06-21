using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //public static Canvas Canvas;

    [SerializeField] private RectTransform _rt;
    [SerializeField] private CanvasGroup _cg;
    [SerializeField] private Image _img;

    private Vector3 _originPos;
    private bool _drag;

    private PointerEventData _eventData;

    // ***
    public GameObject Prefab;
    
    private void Awake()
    {
        _originPos = _rt.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _eventData = eventData;
        _drag = true;
        _cg.blocksRaycasts = false;
        _img.maskable = true;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        _eventData = eventData;
        _rt.anchoredPosition += eventData.delta;// / Canvas.scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        _drag = false;
        _cg.blocksRaycasts = true;
        _img.maskable = true;
        _rt.anchoredPosition = _originPos;
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_eventData == null)
        {
            return;
        }

        if (!col.CompareTag("DragEndPanel"))
        {
            return;
        }

        //ShopManager.Current.ShopButtonOnClick();
        _drag = false;
        _cg.blocksRaycasts = true;
        _img.maskable = true;
        _rt.anchoredPosition = _originPos;
        
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(null);
        _eventData.pointerDrag = null;
        
        SpawnManager.Current.Spawn(Prefab);
        
        //GridBuildingSystem.Current.InitializeWithBuilding(Prefab);
    }

    private void OnEnable()
    {
        _drag = false;
        _cg.blocksRaycasts = true;
        _img.maskable = true;
        _rt.anchoredPosition = _originPos;
    }
}
