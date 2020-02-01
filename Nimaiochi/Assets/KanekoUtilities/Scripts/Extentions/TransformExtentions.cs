using UnityEngine;

namespace KanekoUtilities
{
    public static class TransformExtentions
    {
        //座標
        public static void SetPositionX(this Transform self, float x)
        {
            Vector3 pos = self.position;
            pos.x = x;
            self.position = pos;
        }
        public static void AddPositoinX(this Transform self, float x)
        {
            Vector3 pos = self.position;
            pos.x += x;
            self.position = pos;
        }
        public static void SetLocalPositionX(this Transform self, float x)
        {
            Vector3 pos = self.localPosition;
            pos.x = x;
            self.localPosition = pos;
        }
        public static void AddLocalPositoinX(this Transform self, float x)
        {
            Vector3 pos = self.localPosition;
            pos.x += x;
            self.localPosition = pos;
        }

        public static void SetPositionY(this Transform self, float y)
        {
            Vector3 pos = self.position;
            pos.y = y;
            self.position = pos;
        }
        public static void AddPositoinY(this Transform self, float y)
        {
            Vector3 pos = self.position;
            pos.y += y;
            self.position = pos;
        }
        public static void SetLocalPositionY(this Transform self, float y)
        {
            Vector3 pos = self.localPosition;
            pos.y = y;
            self.localPosition = pos;
        }
        public static void AddLocalPositoinY(this Transform self, float y)
        {
            Vector3 pos = self.localPosition;
            pos.y += y;
            self.localPosition = pos;
        }

        public static void SetPositionZ(this Transform self, float z)
        {
            Vector3 pos = self.position;
            pos.z = z;
            self.position = pos;
        }
        public static void AddPositoinZ(this Transform self, float z)
        {
            Vector3 pos = self.position;
            pos.z += z;
            self.position = pos;
        }
        public static void SetLocalPositionZ(this Transform self, float z)
        {
            Vector3 pos = self.localPosition;
            pos.z = z;
            self.localPosition = pos;
        }
        public static void AddLocalPositoinZ(this Transform self, float z)
        {
            Vector3 pos = self.localPosition;
            pos.z += z;
            self.localPosition = pos;
        }

        //回転
        public static void SetRotationX(this Transform self, float x)
        {
            Vector3 angle = self.eulerAngles;
            angle.x = x;
            self.rotation = Quaternion.Euler(angle);
        }
        public static void AddRotationX(this Transform self, float x)
        {
            Vector3 angle = self.eulerAngles;
            angle.x += x;
            self.rotation = Quaternion.Euler(angle);
        }
        public static void SetLocalRotationX(this Transform self, float x)
        {
            Vector3 angle = self.localEulerAngles;
            angle.x = x;
            self.localRotation = Quaternion.Euler(angle);
        }
        public static void AddLocalRotationX(this Transform self, float x)
        {
            Vector3 angle = self.localEulerAngles;
            angle.x += x;
            self.localRotation = Quaternion.Euler(angle);
        }

        public static void SetRotationY(this Transform self, float y)
        {
            Vector3 angle = self.eulerAngles;
            angle.y = y;
            self.rotation = Quaternion.Euler(angle);
        }
        public static void AddRotationY(this Transform self, float y)
        {
            Vector3 angle = self.eulerAngles;
            angle.y += y;
            self.rotation = Quaternion.Euler(angle);
        }
        public static void SetLocalRotationY(this Transform self, float y)
        {
            Vector3 angle = self.localEulerAngles;
            angle.y = y;
            self.localRotation = Quaternion.Euler(angle);
        }
        public static void AddLocalRotationY(this Transform self, float y)
        {
            Vector3 angle = self.localEulerAngles;
            angle.y += y;
            self.localRotation = Quaternion.Euler(angle);
        }

        public static void SetRotationZ(this Transform self, float z)
        {
            Vector3 angle = self.eulerAngles;
            angle.z = z;
            self.rotation = Quaternion.Euler(angle);
        }
        public static void AddRotationZ(this Transform self, float z)
        {
            Vector3 angle = self.eulerAngles;
            angle.z += z;
            self.rotation = Quaternion.Euler(angle);
        }
        public static void SetLocalRotationZ(this Transform self, float z)
        {
            Vector3 angle = self.localEulerAngles;
            angle.z = z;
            self.localRotation = Quaternion.Euler(angle);
        }
        public static void AddLocalRotationZ(this Transform self, float z)
        {
            Vector3 angle = self.localEulerAngles;
            angle.z += z;
            self.localRotation = Quaternion.Euler(angle);
        }

        //スケール
        public static void SetLocalScaleX(this Transform transform, float x)
        {
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }
        public static void AddLocalScaleX(this Transform transform, float x)
        {
            transform.SetLocalScaleX(transform.localScale.x + x);
        }

        public static void SetLocalScaleY(this Transform transform, float y)
        {
            transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
        }
        public static void AddLocalScaleY(this Transform transform, float y)
        {
            transform.SetLocalScaleY(transform.localScale.y + y);
        }

        public static void SetLocalScaleZ(this Transform transform, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
        }
        public static void AddLocalScaleZ(this Transform transform, float z)
        {
            transform.SetLocalScaleZ(transform.localScale.z + z);
        }

        public static void TranslateX(this Transform self, float x)
        {
            self.Translate(x, 0, 0);
        }
        public static void TranslateX(this Transform self, float x, Space space)
        {
            self.Translate(x, 0, 0, space);
        }

        public static void TranslateY(this Transform self, float y)
        {
            self.Translate(0, y, 0);
        }
        public static void TranslateY(this Transform self, float y, Space space)
        {
            self.Translate(0, y, 0, space);
        }

        public static void TranslateZ(this Transform self, float z)
        {
            self.Translate(0, 0, z);
        }
        public static void TranslateZ(this Transform self, float z, Space space)
        {
            self.Translate(0, 0, z, space);
        }
    }
}