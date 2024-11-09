using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cf.Util
{
    public static class UtilRect
    {
        #region > Obb

        public static class Obb 
        {
            private static Vector3[] GetCorners(RectTransform rt)
            {
                var corners = new Vector3[4];
                rt.GetWorldCorners(corners);

                return corners;
            }

            private static Vector3 GetEdge(Vector3 l, Vector3 r)
            {
                return (r - l).normalized;
            }

            private static bool OverlapOnAxis(Vector3[] l, Vector3[] r, Vector3 axis)
            {
                ProjectOntoAxis(l, axis, out var lMin, out var lMax);
                ProjectOntoAxis(r, axis, out var rMin, out var rMax);

                return !(rMin > lMax || lMin > rMax);
            }

            private static void ProjectOntoAxis(Vector3[] corners, Vector3 axis, out float min, out float max)
            {
                min = max = Vector3.Dot(corners[0], axis);

                for (var i = 1; i < corners.Length; i++)
                {
                    var projection = Vector3.Dot(corners[i], axis);
                    if (projection < min)
                        min = projection;
                    if (projection > max)
                        max = projection;
                }
            }


            public static bool ObbCollision(List<RectTransform> sourceList, RectTransform target)
            {
                foreach (var rt in sourceList)
                {
                    if (rt == target)
                    {
                        continue;
                    }

                    if (ObbCollision(rt, target))
                    {
                        return true;
                    }
                }

                return false;
            }

            public static bool ObbCollision(List<RectTransform> sourceList, RectTransform target, out int colCount)
            {
                colCount = 0;

                foreach (var rt in sourceList)
                {
                    if (rt == target)
                    {
                        continue;
                    }

                    if (ObbCollision(rt, target))
                    {
                        colCount++;
                    }
                }

                return colCount > 0;
            }

            public static bool ObbCollision(RectTransform l, RectTransform r)
            {
                var cornerL = GetCorners(l);
                var cornerR = GetCorners(r);

                var axes = new Vector3[4]
                {
                    GetEdge(l: cornerL[0], r: cornerL[1]),
                    GetEdge(l: cornerL[1], r: cornerL[2]),
                    GetEdge(l: cornerR[0], r: cornerR[1]),
                    GetEdge(l: cornerR[1], r: cornerR[2]),
                };

                foreach (var axis in axes)
                {
                    if (!OverlapOnAxis(cornerL, cornerR, axis))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        #endregion
    }
}