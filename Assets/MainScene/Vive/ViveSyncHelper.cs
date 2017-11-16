using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class ViveSyncHelper {
    public static void syncPosition(Transform transform, VRNode node)
    {
        transform.localPosition = InputTracking.GetLocalPosition(node);
        transform.localRotation = InputTracking.GetLocalRotation(node);
    }
}
