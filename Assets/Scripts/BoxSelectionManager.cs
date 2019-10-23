using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.MessageTypes.RosSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

public class BoxSelectionManager : MonoBehaviour
{
    public List<SelectableItem> selectableBoxes;
    public RosConnector rosConnector;
    public Camera mainCamera;
    public UnityEngine.Object box3D;

    private GameObject _box3D;

    private SelectableItem _selectedItem;
    private Image _selectedMark;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var box in selectableBoxes)
        {
            box.HasBox = true;
        }
        _box3D = (GameObject)GameObject.Instantiate(box3D, transform);
        var highlightScript = _box3D.GetComponent<HighlightBox>();
        highlightScript.mainCamera = mainCamera;
        _selectedMark = transform.Find("SelectedMark").GetComponent<Image>();
        _selectedMark.enabled = false;
        _box3D.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public SelectableItem FindSelectableItemByPosition(Vector3 pos)
    {
        foreach (var box in selectableBoxes)
        {
            if (box.gameObject.GetComponent<Collider>().bounds.Contains(pos))
            {
                return box;
            }
        }
        return null;
    }

    public void ClickOnMenu(PointerEventArgs e)
    {
        Debug.Log($"click on menu item: {_selectedItem?.id}");
        if (_selectedItem != null && _selectedItem.HasBox)
        {
            SendPickBoxMsg(_selectedItem);
            _box3D.SetActive(false);
            _selectedItem.BoxImage.enabled = true;
        }
        UpdateSelectedMark();
    }

    private void UpdateSelectedMark()
    {
        if (_selectedItem != null && _selectedItem.HasBox)
        {
            _selectedMark.rectTransform.parent = _selectedItem.BoxImage.rectTransform;
            _selectedMark.rectTransform.localPosition = Vector3.zero;
            _selectedMark.enabled = true;
        }
        else
        {
            _selectedMark.enabled = false;
        }
    }

    public void HoverOnMenuStart(PointerEventArgs e)
    {
        var item = FindSelectableItemByPosition(e.target.position);
        if (item != null)
        {
            item.Highlight(true);
            Debug.Log($"start hover on menu item: {item.id}");
            if (item.HasBox)
            {
                item.BoxImage.enabled = false;
                _box3D.transform.position = item.BoxImage.transform.position;
                _box3D.transform.rotation = item.BoxImage.transform.rotation;
                _box3D.SetActive(true);
            }
            _selectedItem = item;
        }
    }

    public void HoverOnMenuEnd(PointerEventArgs e)
    {
        if (_selectedItem != null)
        {
            _selectedItem.Highlight(false);
            Debug.Log($"end hover on menu item: {_selectedItem.id}");
            if (_selectedItem.HasBox)
            {
                _box3D.SetActive(false);
                _selectedItem.BoxImage.enabled = true;
            }
            _selectedItem = null;
        }
    }

    private void SendPickBoxMsg(SelectableItem box)
    {
        rosConnector.RosSocket.CallService<PickGiftRequest, PickGiftResponse>("/ros_sharp/PickGift", new ServiceResponseHandler<PickGiftResponse>((response) =>
        {
            if (response.result == 1)
            {
                _selectedMark.enabled = false;
                box.HasBox = false;
            }
            else
            {
                Debug.LogError("Call pick gift service failed!");
            }
        }), new PickGiftRequest(box.id));
    }
}
