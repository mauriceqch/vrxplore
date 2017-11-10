using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class ViveSyncHelper {
    public static void syncPosition(GameObject o, VRNode node)
    {
        o.transform.position = InputTracking.GetLocalPosition(node);
    }
}
