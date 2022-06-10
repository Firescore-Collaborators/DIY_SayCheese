using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicer;
using BzKovSoft.ObjectSlicer.Samples;

[DisallowMultipleComponent]
public class Sliceable : MonoBehaviour
{
    IBzSliceableNoRepeat _sliceableAsync;
    public static System.Action<GameObject> sliced;
    void Start()
    {
        _sliceableAsync = GetComponentInParent<IBzSliceableNoRepeat>();
    }

    void OnTriggerEnter(Collider other)
    {
        var knife = other.gameObject.GetComponent<BzKnife>();
        if (knife == null)
            return;

        StartCoroutine(Slice(knife));
    }

    private IEnumerator Slice(BzKnife knife)
    {
        // The call from OnTriggerEnter, so some object positions are wrong.
        // We have to wait for next frame to work with correct values
        yield return null;

        Vector3 point = GetCollisionPoint(knife);
        Vector3 normal = Vector3.Cross(knife.MoveDirection, knife.BladeDirection);
        Plane plane = new Plane(normal, point);

        if (_sliceableAsync != null)
        {
            _sliceableAsync.Slice(plane, knife.SliceID, (BzSliceTryResult result) =>
            {
                if (!result.sliced) return;
                sliced?.Invoke(result.outObjectPos);
            });
            knife.enabled = false;
        }
    }

    private Vector3 GetCollisionPoint(BzKnife knife)
    {
        Vector3 distToObject = transform.position - knife.Origin;
        Vector3 proj = Vector3.Project(distToObject, knife.BladeDirection);

        Vector3 collisionPoint = knife.Origin + proj;
        return collisionPoint;
    }

}
