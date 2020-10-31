using UnityEngine;
using System.Collections;

public class UnityUtility
{
    /// <summary>
    /// Correct the position of the ui object to the specified 3D position in scene. 
    /// </summary>
    public static void ScenePositionToUIPosition(Camera sceneCamera,
                                                  Camera uiCamera,
                                                  Vector3 posInScene,
                                                  Transform uiTarget)
    {
        Vector3 viewportPos = sceneCamera.WorldToViewportPoint(posInScene);//unify the position to viewport point.
        Vector3 worldPos = uiCamera.ViewportToWorldPoint(viewportPos);
        uiTarget.position = worldPos;//set world position.
        Vector3 localPos = uiTarget.localPosition;
        localPos.z = 0f;//ignore z axis offset.
        uiTarget.localPosition = localPos;//correct the local position of the ui target.
    }

    /// <summary>
    /// Correct the position of the ui object to the specified 3D position      in scene. 
    /// </summary>
    public static void ScenePositionToUIPosition(Camera sceneCamera,
                                                  Camera uiCamera,
                                                  Vector3 posInScene,
                                                  Transform uiTarget,
                                                  Vector2 offset)
    {
        ScenePositionToUIPosition(sceneCamera, uiCamera, posInScene, uiTarget);
        uiTarget.localPosition += (Vector3)offset;
    }
}
