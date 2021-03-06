using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCircle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public float speed;

    public ToolIcon tool;

    public Animator seedPickerAnim;

    public ToolCircleActivator activator;

    void Start()
    {
        activator = transform.parent.GetComponent<ToolCircleActivator>();
        tool = GameObject.Find("Glove").GetComponent<ToolIcon>();
        tool.isSelected = true;
        FindObjectOfType<SwapTools>().SwitchTool(tool.name);
#if UNITY_EDITOR
        speed = 50;
#else
        speed /= 10;
#endif
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        activator.ActivateMe();
    }

    private void OnMouseDown()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //activator.ActivateMe();
        //if (tool != null)
        //{
        //    seedPickerAnim.SetBool("IsActive", false);
        //    tool.GetComponent<Collider2D>().enabled = true;
        //    tool.isSelected = false;
        //    tool = null;
        //}
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.Rotate(new Vector3(0, 0, -eventData.delta.x) * speed * Time.deltaTime);
        if (tool.name == "Seed")
        {
            seedPickerAnim.SetBool("IsActive", false);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        activator.DeactivateMe();
        tool = FindObjectOfType<DetectTool>().detectedTool;
        tool.isSelected = true;
        FindObjectOfType<SwapTools>().SwitchTool(tool.name);
        tool.GetComponent<Collider2D>().enabled = false;
        if (tool.name == "Seed")
        {
            seedPickerAnim.SetBool("IsActive", true);
        }
        else
        {
            seedPickerAnim.SetBool("IsActive", false);
        }
        //Debug.Log(transform.localEulerAngles + new Vector3(0f, 0f, Vector3.SignedAngle(tool.transform.position - transform.position, Vector3.up, Vector3.forward)));
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(0f, 0f, Vector3.SignedAngle(tool.myRotationPicker.position - transform.position, Vector3.up, Vector3.forward)));
        //transform.Rotate(transform.localEulerAngles + new Vector3(0f, 0f, Vector3.SignedAngle(tool.transform.position - transform.position, Vector3.up, Vector3.forward)));
        //transform.DOLocalRotate(transform.localEulerAngles + new Vector3(0f, 0f, Vector3.SignedAngle(detector.lastFocused.position - transform.position, Vector3.up, Vector3.forward)), 0.2f);
    }
}
