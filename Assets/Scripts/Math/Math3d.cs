using System;
using UnityEngine;


public class Math3d
{
    private static Transform tempChild;

    private static Transform tempParent;

    private static Vector3[] positionRegister;

    private static float[] posTimeRegister;

    private static int positionSamplesTaken;

    private static Quaternion[] rotationRegister;

    private static float[] rotTimeRegister;

    private static int rotationSamplesTaken;

    public static void Init() {
        Math3d.tempChild = new GameObject("Math3d_TempChild").transform;
        Math3d.tempParent = new GameObject("Math3d_TempParent").transform;
        UnityEngine.Object.DontDestroyOnLoad(Math3d.tempChild.gameObject);
        UnityEngine.Object.DontDestroyOnLoad(Math3d.tempParent.gameObject);
        Math3d.tempChild.parent = Math3d.tempParent;
    }

    public static Vector3 AddVectorLength(Vector3 vector, float size) {
        float num = Vector3.Magnitude(vector);
        float d = (num + size) / num;
        return vector * d;
    }

    public static Vector3 SetVectorLength(Vector3 vector, float size) {
        return Vector3.Normalize(vector) * size;
    }

    public static Quaternion SubtractRotation(Quaternion B, Quaternion A) {
        return Quaternion.Inverse(A) * B;
    }

    public static Quaternion AddRotation(Quaternion A, Quaternion B) {
        return A * B;
    }

    public static Vector3 TransformDirectionMath(Quaternion rotation, Vector3 vector) {
        return rotation * vector;
    }

    public static Vector3 InverseTransformDirectionMath(Quaternion rotation, Vector3 vector) {
        return Quaternion.Inverse(rotation) * vector;
    }

    public static Vector3 RotateVectorFromTo(Quaternion from, Quaternion to, Vector3 vector) {
        Quaternion arg_10_0 = Math3d.SubtractRotation(to, from);
        Vector3 point = Math3d.InverseTransformDirectionMath(from, vector);
        Vector3 vector2 = arg_10_0 * point;
        return Math3d.TransformDirectionMath(from, vector2);
    }

    public static bool PlanePlaneIntersection(out Vector3 linePoint, out Vector3 lineVec, Vector3 plane1Normal, Vector3 plane1Position, Vector3 plane2Normal, Vector3 plane2Position) {
        linePoint = Vector3.zero;
        lineVec = Vector3.zero;
        lineVec = Vector3.Cross(plane1Normal, plane2Normal);
        Vector3 vector = Vector3.Cross(plane2Normal, lineVec);
        float num = Vector3.Dot(plane1Normal, vector);
        if (Mathf.Abs(num) > 0.006f)
        {
            Vector3 rhs = plane1Position - plane2Position;
            float d = Vector3.Dot(plane1Normal, rhs) / num;
            linePoint = plane2Position + d * vector;
            return true;
        }
        return false;
    }

    public static bool LinePlaneIntersection(out Vector3 intersection, Vector3 linePoint, Vector3 lineVec, Vector3 planeNormal, Vector3 planePoint) {
        intersection = Vector3.zero;
        float num = Vector3.Dot(planePoint - linePoint, planeNormal);
        float num2 = Vector3.Dot(lineVec, planeNormal);
        if (num2 != 0f)
        {
            float size = num / num2;
            Vector3 b = Math3d.SetVectorLength(lineVec, size);
            intersection = linePoint + b;
            return true;
        }
        return false;
    }

    public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2) {
        Vector3 arg_10_0 = linePoint2 - linePoint1;
        Vector3 rhs = Vector3.Cross(lineVec1, lineVec2);
        Vector3 lhs = Vector3.Cross(arg_10_0, lineVec2);
        if (Mathf.Abs(Vector3.Dot(arg_10_0, rhs)) < 0.0001f && rhs.sqrMagnitude > 0.0001f)
        {
            float d = Vector3.Dot(lhs, rhs) / rhs.sqrMagnitude;
            intersection = linePoint1 + lineVec1 * d;
            return true;
        }
        intersection = Vector3.zero;
        return false;
    }

    public static bool ClosestPointsOnTwoLines(out Vector3 closestPointLine1, out Vector3 closestPointLine2, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2) {
        closestPointLine1 = Vector3.zero;
        closestPointLine2 = Vector3.zero;
        float num = Vector3.Dot(lineVec1, lineVec1);
        float num2 = Vector3.Dot(lineVec1, lineVec2);
        float num3 = Vector3.Dot(lineVec2, lineVec2);
        float num4 = num * num3 - num2 * num2;
        if (num4 != 0f)
        {
            Vector3 rhs = linePoint1 - linePoint2;
            float num5 = Vector3.Dot(lineVec1, rhs);
            float num6 = Vector3.Dot(lineVec2, rhs);
            float d = (num2 * num6 - num5 * num3) / num4;
            float d2 = (num * num6 - num5 * num2) / num4;
            closestPointLine1 = linePoint1 + lineVec1 * d;
            closestPointLine2 = linePoint2 + lineVec2 * d2;
            return true;
        }
        return false;
    }

    public static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point) {
        float d = Vector3.Dot(point - linePoint, lineVec);
        return linePoint + lineVec * d;
    }

    public static Vector3 ProjectPointOnLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {
        Vector3 vector = Math3d.ProjectPointOnLine(linePoint1, (linePoint2 - linePoint1).normalized, point);
        int num = Math3d.PointOnWhichSideOfLineSegment(linePoint1, linePoint2, vector);
        if (num == 0)
        {
            return vector;
        }
        if (num == 1)
        {
            return linePoint1;
        }
        if (num == 2)
        {
            return linePoint2;
        }
        return Vector3.zero;
    }

    public static Vector3 ProjectPointOnPlane(Vector3 planeNormal, Vector3 planePoint, Vector3 point) {
        float num = Math3d.SignedDistancePlanePoint(planeNormal, planePoint, point);
        num *= -1f;
        Vector3 b = Math3d.SetVectorLength(planeNormal, num);
        return point + b;
    }

    public static Vector3 ProjectVectorOnPlane(Vector3 planeNormal, Vector3 vector) {
        return vector - Vector3.Dot(vector, planeNormal) * planeNormal;
    }

    public static Vector3 ProjectVectorOnPlaneNormalized(Vector3 planeNormal, Vector3 vector) {
        return (vector - Vector3.Dot(vector, planeNormal) * planeNormal).normalized * vector.magnitude;
    }

    public static float SignedDistancePlanePoint(Vector3 planeNormal, Vector3 planePoint, Vector3 point) {
        return Vector3.Dot(planeNormal, point - planePoint);
    }

    public static float SignedDotProduct(Vector3 vectorA, Vector3 vectorB, Vector3 normal) {
        return Vector3.Dot(Vector3.Cross(normal, vectorA), vectorB);
    }

    public static float SignedVectorAngle(Vector3 referenceVector, Vector3 otherVector, Vector3 normal) {
        Vector3 lhs = Vector3.Cross(normal, referenceVector);
        return Vector3.Angle(referenceVector, otherVector) * Mathf.Sign(Vector3.Dot(lhs, otherVector));
    }

    public static float AngleVectorPlane(Vector3 vector, Vector3 normal) {
        float num = (float)Math.Acos((double)Vector3.Dot(vector, normal));
        return 1.57079637f - num;
    }

    public static float DotProductAngle(Vector3 vec1, Vector3 vec2) {
        double num = (double)Vector3.Dot(vec1, vec2);
        if (num < -1.0)
        {
            num = -1.0;
        }
        if (num > 1.0)
        {
            num = 1.0;
        }
        return (float)Math.Acos(num);
    }

    public static void PlaneFrom3Points(out Vector3 planeNormal, out Vector3 planePoint, Vector3 pointA, Vector3 pointB, Vector3 pointC) {
        planeNormal = Vector3.zero;
        planePoint = Vector3.zero;
        Vector3 vector = pointB - pointA;
        Vector3 vector2 = pointC - pointA;
        planeNormal = Vector3.Normalize(Vector3.Cross(vector, vector2));
        Vector3 vector3 = pointA + vector / 2f;
        Vector3 vector4 = pointA + vector2 / 2f;
        Vector3 lineVec = pointC - vector3;
        Vector3 lineVec2 = pointB - vector4;
        Vector3 vector5;
        Math3d.ClosestPointsOnTwoLines(out planePoint, out vector5, vector3, lineVec, vector4, lineVec2);
    }

    public static Vector3 GetForwardVector(Quaternion q) {
        return q * Vector3.forward;
    }

    public static Vector3 GetUpVector(Quaternion q) {
        return q * Vector3.up;
    }

    public static Vector3 GetRightVector(Quaternion q) {
        return q * Vector3.right;
    }

    public static Quaternion QuaternionFromMatrix(Matrix4x4 m) {
        return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
    }

    public static Vector3 PositionFromMatrix(Matrix4x4 m) {
        Vector4 column = m.GetColumn(3);
        return new Vector3(column.x, column.y, column.z);
    }

    public static Quaternion LookRotationExtended(Vector3 alignWithVector, Vector3 alignWithNormal, Vector3 customForward, Vector3 customUp) {
        Quaternion arg_15_0 = Quaternion.LookRotation(alignWithVector, alignWithNormal);
        Quaternion rotation = Quaternion.LookRotation(customForward, customUp);
        return arg_15_0 * Quaternion.Inverse(rotation);
    }

    public static void TransformWithParent(out Quaternion childRotation, out Vector3 childPosition, Quaternion parentRotation, Vector3 parentPosition, Quaternion startParentRotation, Vector3 startParentPosition, Quaternion startChildRotation, Vector3 startChildPosition) {
        childRotation = Quaternion.identity;
        childPosition = Vector3.zero;
        Math3d.tempParent.rotation = startParentRotation;
        Math3d.tempParent.position = startParentPosition;
        Math3d.tempParent.localScale = Vector3.one;
        Math3d.tempChild.rotation = startChildRotation;
        Math3d.tempChild.position = startChildPosition;
        Math3d.tempChild.localScale = Vector3.one;
        Math3d.tempParent.rotation = parentRotation;
        Math3d.tempParent.position = parentPosition;
        childRotation = Math3d.tempChild.rotation;
        childPosition = Math3d.tempChild.position;
    }

    public static void PreciseAlign(ref GameObject gameObjectInOut, Vector3 alignWithVector, Vector3 alignWithNormal, Vector3 alignWithPosition, Vector3 triangleForward, Vector3 triangleNormal, Vector3 trianglePosition) {
        gameObjectInOut.transform.rotation = Math3d.LookRotationExtended(alignWithVector, alignWithNormal, triangleForward, triangleNormal);
        Vector3 b = gameObjectInOut.transform.TransformPoint(trianglePosition);
        Vector3 translation = alignWithPosition - b;
        gameObjectInOut.transform.Translate(translation, Space.World);
    }

    public static void VectorsToTransform(ref GameObject gameObjectInOut, Vector3 positionVector, Vector3 directionVector, Vector3 normalVector) {
        gameObjectInOut.transform.position = positionVector;
        gameObjectInOut.transform.rotation = Quaternion.LookRotation(directionVector, normalVector);
    }

    public static int PointOnWhichSideOfLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point) {
        Vector3 rhs = linePoint2 - linePoint1;
        Vector3 lhs = point - linePoint1;
        if (Vector3.Dot(lhs, rhs) <= 0f) {
            return 1;
        }
        if (lhs.magnitude <= rhs.magnitude) {
            return 0;
        }
        return 2;
    }

    public static float MouseDistanceToLine(Vector3 linePoint1, Vector3 linePoint2) {
        Camera arg_0B_0 = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 linePoint3 = arg_0B_0.WorldToScreenPoint(linePoint1);
        Vector3 linePoint4 = arg_0B_0.WorldToScreenPoint(linePoint2);
        Vector3 vector = Math3d.ProjectPointOnLineSegment(linePoint3, linePoint4, mousePosition);
        vector = new Vector3(vector.x, vector.y, 0f);
        return (vector - mousePosition).magnitude;
    }

    public static float MouseDistanceToCircle(Vector3 point, float radius) {
        Camera arg_0C_0 = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 vector = arg_0C_0.WorldToScreenPoint(point);
        vector = new Vector3(vector.x, vector.y, 0f);
        return (vector - mousePosition).magnitude - radius;
    }

    public static bool IsLineInRectangle(Vector3 linePoint1, Vector3 linePoint2, Vector3 rectA, Vector3 rectB, Vector3 rectC, Vector3 rectD) {
        bool flag = false;
        bool expr_0E = Math3d.IsPointInRectangle(linePoint1, rectA, rectC, rectB, rectD);
        if (!expr_0E) {
            flag = Math3d.IsPointInRectangle(linePoint2, rectA, rectC, rectB, rectD);
        }
        if (!expr_0E && !flag) {
            bool arg_4F_0 = Math3d.AreLineSegmentsCrossing(linePoint1, linePoint2, rectA, rectB);
            bool flag2 = Math3d.AreLineSegmentsCrossing(linePoint1, linePoint2, rectB, rectC);
            bool flag3 = Math3d.AreLineSegmentsCrossing(linePoint1, linePoint2, rectC, rectD);
            bool flag4 = Math3d.AreLineSegmentsCrossing(linePoint1, linePoint2, rectD, rectA);
            return arg_4F_0 | flag2 | flag3 | flag4;
        }
        return true;
    }

    public static bool IsPointInRectangle(Vector3 point, Vector3 rectA, Vector3 rectC, Vector3 rectB, Vector3 rectD) {
        Vector3 vector = rectC - rectA;
        float size = -(vector.magnitude / 2f);
        vector = Math3d.AddVectorLength(vector, size);
        Vector3 arg_55_0 = rectA + vector;
        Vector3 vector2 = rectB - rectA;
        float num = vector2.magnitude / 2f;
        Vector3 vector3 = rectD - rectA;
        float num2 = vector3.magnitude / 2f;
        float magnitude = (Math3d.ProjectPointOnLine(arg_55_0, vector2.normalized, point) - point).magnitude;
        return (Math3d.ProjectPointOnLine(arg_55_0, vector3.normalized, point) - point).magnitude <= num && magnitude <= num2;
    }

    public static bool AreLineSegmentsCrossing(Vector3 pointA1, Vector3 pointA2, Vector3 pointB1, Vector3 pointB2) {
        Vector3 vector = pointA2 - pointA1;
        Vector3 vector2 = pointB2 - pointB1;
        Vector3 point;
        Vector3 point2;
        if (Math3d.ClosestPointsOnTwoLines(out point, out point2, pointA1, vector.normalized, pointB1, vector2.normalized)) {
            bool arg_3D_0 = Math3d.PointOnWhichSideOfLineSegment(pointA1, pointA2, point) != 0;
            int num = Math3d.PointOnWhichSideOfLineSegment(pointB1, pointB2, point2);
            return !arg_3D_0 && num == 0;
        }
        return false;
    }

    public static bool LinearAcceleration(out Vector3 vector, Vector3 position, int samples) {
        Vector3 a = Vector3.zero;
        vector = Vector3.zero;
        if (samples < 3) {
            samples = 3;
        }
        if (Math3d.positionRegister == null) {
            Math3d.positionRegister = new Vector3[samples];
            Math3d.posTimeRegister = new float[samples];
        }
        for (int i = 0; i < Math3d.positionRegister.Length - 1; i++) {
            Math3d.positionRegister[i] = Math3d.positionRegister[i + 1];
            Math3d.posTimeRegister[i] = Math3d.posTimeRegister[i + 1];
        }
        Math3d.positionRegister[Math3d.positionRegister.Length - 1] = position;
        Math3d.posTimeRegister[Math3d.posTimeRegister.Length - 1] = Time.time;
        Math3d.positionSamplesTaken++;
        if (Math3d.positionSamplesTaken >= samples) {
            for (int j = 0; j < Math3d.positionRegister.Length - 2; j++)
            {
                Vector3 a2 = Math3d.positionRegister[j + 1] - Math3d.positionRegister[j];
                float num = Math3d.posTimeRegister[j + 1] - Math3d.posTimeRegister[j];
                if (num == 0f) {
                    return false;
                }
                Vector3 b = a2 / num;
                a2 = Math3d.positionRegister[j + 2] - Math3d.positionRegister[j + 1];
                num = Math3d.posTimeRegister[j + 2] - Math3d.posTimeRegister[j + 1];
                if (num == 0f)
                {
                    return false;
                }
                Vector3 a3 = a2 / num;
                a += a3 - b;
            }
            a /= (float)(Math3d.positionRegister.Length - 2);
            float d = Math3d.posTimeRegister[Math3d.posTimeRegister.Length - 1] - Math3d.posTimeRegister[0];
            vector = a / d;
            return true;
        }
        return false;
    }

    public static bool AngularAcceleration(out Vector3 vector, Quaternion rotation, int samples)
    {
        Vector3 a = Vector3.zero;
        vector = Vector3.zero;
        if (samples < 3)
        {
            samples = 3;
        }
        if (Math3d.rotationRegister == null)
        {
            Math3d.rotationRegister = new Quaternion[samples];
            Math3d.rotTimeRegister = new float[samples];
        }
        for (int i = 0; i < Math3d.rotationRegister.Length - 1; i++)
        {
            Math3d.rotationRegister[i] = Math3d.rotationRegister[i + 1];
            Math3d.rotTimeRegister[i] = Math3d.rotTimeRegister[i + 1];
        }
        Math3d.rotationRegister[Math3d.rotationRegister.Length - 1] = rotation;
        Math3d.rotTimeRegister[Math3d.rotTimeRegister.Length - 1] = Time.time;
        Math3d.rotationSamplesTaken++;
        if (Math3d.rotationSamplesTaken >= samples)
        {
            for (int j = 0; j < Math3d.rotationRegister.Length - 2; j++)
            {
                Quaternion rotation2 = Math3d.SubtractRotation(Math3d.rotationRegister[j + 1], Math3d.rotationRegister[j]);
                float num = Math3d.rotTimeRegister[j + 1] - Math3d.rotTimeRegister[j];
                if (num == 0f)
                {
                    return false;
                }
                Vector3 b = Math3d.RotDiffToSpeedVec(rotation2, num);
                rotation2 = Math3d.SubtractRotation(Math3d.rotationRegister[j + 2], Math3d.rotationRegister[j + 1]);
                num = Math3d.rotTimeRegister[j + 2] - Math3d.rotTimeRegister[j + 1];
                if (num == 0f)
                {
                    return false;
                }
                Vector3 a2 = Math3d.RotDiffToSpeedVec(rotation2, num);
                a += a2 - b;
            }
            a /= (float)(Math3d.rotationRegister.Length - 2);
            float d = Math3d.rotTimeRegister[Math3d.rotTimeRegister.Length - 1] - Math3d.rotTimeRegister[0];
            vector = a / d;
            return true;
        }
        return false;
    }

    public static float LinearFunction2DBasic(float x, float Qx, float Qy)
    {
        return x * (Qy / Qx);
    }

    public static float LinearFunction2DFull(float x, float Px, float Py, float Qx, float Qy)
    {
        float arg_09_0 = Qy - Py;
        float num = Qx - Px;
        float num2 = arg_09_0 / num;
        return Py + num2 * (x - Px);
    }

    private static Vector3 RotDiffToSpeedVec(Quaternion rotation, float deltaTime)
    {
        float num;
        if (rotation.eulerAngles.x <= 180f)
        {
            num = rotation.eulerAngles.x;
        }
        else
        {
            num = rotation.eulerAngles.x - 360f;
        }
        float num2;
        if (rotation.eulerAngles.y <= 180f)
        {
            num2 = rotation.eulerAngles.y;
        }
        else
        {
            num2 = rotation.eulerAngles.y - 360f;
        }
        float num3;
        if (rotation.eulerAngles.z <= 180f)
        {
            num3 = rotation.eulerAngles.z;
        }
        else
        {
            num3 = rotation.eulerAngles.z - 360f;
        }
        return new Vector3(num / deltaTime, num2 / deltaTime, num3 / deltaTime);
    }
}
