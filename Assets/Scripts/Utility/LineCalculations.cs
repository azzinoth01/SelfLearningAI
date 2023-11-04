using System.Collections.Generic;
using UnityEngine;

public static class LineCalculations {
    public static Vector3 GetClosestPointOnLine(Vector3 start, Vector3 end, Vector3 point) {
        Vector3 line = end - start;
        Vector3 pointLine = point - start;
        float dot = Mathf.Clamp(Vector3.Dot(pointLine, line.normalized), 0, line.magnitude);
        Vector3 dotline = line.normalized * dot;
        Vector3 dotPos = start + dotline;

        return dotPos;
    }

    public static int GetIndexOfClosesPositionWithinList(List<Vector3> positionList, Vector3 position) {
        if (positionList == null || positionList.Count < 0) {
            return -1;
        }

        int index = 0;
        float currentDistance;
        float distance = (position - positionList[index]).magnitude;

        for (int i = 1; i < positionList.Count; i++) {
            currentDistance = (position - positionList[i]).magnitude;
            if (currentDistance < distance) {
                distance = currentDistance;
                index = i;
            }
        }

        return index;
    }

    public static float GetSignedDistance(Vector3 targetPosition, Vector3 position, Vector3 direction) {
        float sign = Vector3.Dot((targetPosition - position).normalized, direction);
        return Vector3.Distance(position, targetPosition) * sign;
    }
}
