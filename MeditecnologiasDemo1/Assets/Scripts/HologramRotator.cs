using MixedRealityToolkit.InputModule.EventData;
using MixedRealityToolkit.InputModule.InputHandlers;
using UnityEngine;

public class HologramRotator : MonoBehaviour, IInputClickHandler, IHoldHandler
{
    private bool isHolding;
    private Transform pivotToRotate;

    public Transform HologramToRotate;
    public int RotationSpeed = 50;
    public RotationDirectionEnum RotationDirection;

    public void OnHoldCanceled(HoldEventData eventData)
    {
        this.isHolding = false;
    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        this.isHolding = false;
    }

    public void OnHoldStarted(HoldEventData eventData)
    {
        this.isHolding = true;
    }

    private void Start()
    {
        this.pivotToRotate = GameObject.FindGameObjectWithTag("pivot").GetComponent<Transform>();
    }

    private void Update()
    {
        if (this.isHolding)
        {
            Rotate(this.RotationSpeed * 2);
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Rotate(this.RotationSpeed);
    }

    private void Rotate(int speed)
    {
        Vector3 direction = Vector3.zero;

        switch (this.RotationDirection)
        {
            case RotationDirectionEnum.Up:
                direction = Vector3.right;
                break;
            case RotationDirectionEnum.Down:
                direction = Vector3.left;
                break;
            case RotationDirectionEnum.Right:
                direction = Vector3.down;                
                break;
            case RotationDirectionEnum.Left:
                direction = Vector3.up;
                break;
            default:
                break;
        }

        this.HologramToRotate.RotateAround(this.pivotToRotate.position, direction, Time.deltaTime * speed);
    }
}

public enum RotationDirectionEnum
{
    Up,
    Down,
    Right,
    Left
}