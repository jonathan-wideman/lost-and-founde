using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace JW
{
    public static class GameUtil
    {
        private static Camera mainCamera;

        public static void SetMainCamera()
        {
            if (mainCamera == null) { mainCamera = Camera.main; }
        }

        public static Vector3? GetMouseWorldPositionWithHitNormalUp(int layerMask)
        {
            return GetMouseWorldPosition(layerMask, (RaycastHit hit) =>
            {
                // can only build on flat surfaces
                // check if normal is world up
                return (hit.normal == Vector3.up);
            });
        }

        public delegate bool RaycastHitRules(RaycastHit hit);
        public static Vector3? GetMouseWorldPosition(int layerMask)
        {
            return GetMouseWorldPosition(layerMask, (RaycastHit hit) => { return true; });
        }
        public static Vector3? GetMouseWorldPosition(int layerMask, RaycastHitRules rules)
        {
            SetMainCamera();

            // bail if mouse is over UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return null;
            }

            RaycastHit hit;
            if (Physics.Raycast(
                mainCamera.ScreenPointToRay(Input.mousePosition),
                out hit,
                1000f,
                layerMask,
                QueryTriggerInteraction.Ignore
            ))
            {
                if (rules(hit))
                {
                    return hit.point;
                }
            }
            return null;
        }

        public static Vector3 WorldToScreenPoint(Vector3 position)
        {
            SetMainCamera();

            return mainCamera.WorldToScreenPoint(position);
        }

        public static Vector3 WorldToViewportPoint(Vector3 position)
        {
            SetMainCamera();

            return mainCamera.WorldToViewportPoint(position);
        }

        public static bool IsOnScreen(Vector3 position)
        {
            Vector3 viewportPosition = GameUtil.WorldToViewportPoint(position);
            return viewportPosition.x >= 0 &&
                viewportPosition.x <= 1 &&
                viewportPosition.y >= 0 &&
                viewportPosition.y <= 1;
        }

        public static Vector3 GetRandomDirection()
        {
            Vector3 direction = UnityEngine.Random.onUnitSphere;
            direction.y = 0;
            return direction;
        }

        public static Vector3 GetRandomPosition(Vector3 origin, float radius, bool onNavMesh = false)
        {
            Vector2 offset = UnityEngine.Random.insideUnitCircle * radius;

            // Vector3 position = new Vector3(halfBounds.x, 0, halfBounds.z);
            Vector3 position = new Vector3(origin.x + offset.x, origin.y, origin.z + offset.y);
            if (onNavMesh)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(position, out hit, 15f, NavMesh.AllAreas))
                {
                    position = hit.position;
                }
                else
                {
                    Debug.LogError("Failed to find point on NavMesh");
                }
            }

            return position;
        }

        public static Vector3 Vector2asXZ(Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }

        public static bool VectorsSameDirection(Vector3 a, Vector3 b)
        {
            return Vector3.Dot(a, b) > 0;
        }

        /// <summary>
        /// Get a rotation from an <c>origin</c> to a <c>target</c> constained by <c>axes</c>
        /// </summary>
        /// <returns>
        /// A Quaternion facing the <c>target</c> from the <c>origin</c> with rotation constrained by <c>axes</c>
        /// </returns>
        /// <param name="origin">Position of the origin, where the rotation will be based</param>
        /// <param name="target">Position of the target, where the roation will point</param>
        /// <param name="axes">Which axes to constrain. Desired rotational axes should be 1 to allow rotation, others 0 to constrain rotation</param>
        /// <example>
        /// <code>
        /// // Rotate transform around world up axis to face target
        /// transform.rotation = GameUtil.LookRotationConstrained(transform.position, target.position, Vector3.up);
        /// </code>
        /// </example>
        public static Quaternion LookRotationConstrained(Vector3 origin, Vector3 target, Vector3 axes)
        {
            Vector3 direction = target - origin;
            Vector3 lookAtRotation;
            if (direction != Vector3.zero)
            {
                lookAtRotation = Quaternion.LookRotation(target - origin).eulerAngles;
            }
            else
            {
                lookAtRotation = Quaternion.identity.eulerAngles;
            }
            return Quaternion.Euler(Vector3.Scale(lookAtRotation, axes));
        }

        public static float SquareDistance(Vector3 a, Vector3 b)
        {
            return Vector3.SqrMagnitude(b - a);
        }

        public static bool IsInLayermask(GameObject gameObject, int layerMask)
        {
            return IsInLayermask(gameObject.layer, layerMask);
        }
        public static bool IsInLayermask(int layer, int layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }

        public static T ChooseRandom<T>(List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        public static T ChooseRandom<T>(T[] list)
        {
            return list[UnityEngine.Random.Range(0, list.Length)];
        }

        public static float GetUIAngleFromDirection(Vector3 direction)
        {
            float radians = Mathf.Atan2(direction.y, direction.x);
            return radians * Mathf.Rad2Deg;

        }
    }
}
