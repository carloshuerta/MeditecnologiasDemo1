using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class HologramZoom : MonoBehaviour, IInputClickHandler, IHoldHandler
{
    private bool isHolding;
    public Transform HologramToZoom;
    public float ZoomSpeed = 0.1f;
    public ZoomDirectionEnum ZoomDirection;

    // Update is called once per frame
    void Update()
    {
        if (this.isHolding)
        {
            MakeZoom();
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        MakeZoom();
    }

    private void MakeZoom()
    {
        if (this.ZoomDirection == ZoomDirectionEnum.In)
        {
            HologramToZoom.localScale += new Vector3(ZoomSpeed, ZoomSpeed, ZoomSpeed);
        }
        else
        {
            HologramToZoom.localScale -= new Vector3(ZoomSpeed, ZoomSpeed, ZoomSpeed);
        }
    }

    public void OnHoldStarted(HoldEventData eventData)
    {
        this.isHolding = true;
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        this.isHolding = false;
    }

    public void OnHoldCanceled(HoldEventData eventData)
    {
        this.isHolding = false;
    }
}

public enum ZoomDirectionEnum
{
    In,
    Out
}