using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Arrow : MonoBehaviour 
{
    private LineRenderer _lineRenderer = null;

    public Vector3 Start
    {
        get { return _linePositions[0]; }
        set { _linePositions[0] = value + Vector3.up; }
    }

    public Vector3 End
    {
        get { return _linePositions[1]; }
        set { _linePositions[1] = value + Vector3.up; }
    }

    private Vector3[] _linePositions = new Vector3[5];

    private void Awake()
    {
        _lineRenderer = this.GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _linePositions.Length;
        SetActive(false);
    }

    private void Update()
    {
        if (_lineRenderer.enabled)
        {
            UpdateEndpoint();
            UpdateCap();
            _lineRenderer.SetPositions(_linePositions);

            Debug.Log($"Start: {Start} End: {End}");
        }
    }

    public void SetActive(bool isActive)
    {
        if (gameObject.activeSelf)
        {
            _lineRenderer.enabled = isActive;
        }
    }

    private void SetCapPosition()
    {
        Vector3 relativePos = End - Start;

    }

    private void UpdateCap()
    {
        _linePositions[2] = (Quaternion.Euler(new Vector3(0, 45f, 0)) * (Start - End).normalized) + End;
        _linePositions[3] = (Quaternion.Euler(new Vector3(0, -45f, 0)) * (Start - End).normalized) + End;
        _linePositions[4] = End;
    }

    public void UpdateEndpoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            End = hit.point;
        }
    }
}
