using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class DancingLineTransform
    {
        public double x;
        public double y;
        public double z;

        public void SetPosition(float xx, float yy, float zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }

        //private int ID;
        //private Transform transform;

        //public void SetID(int id)
        //{
        //    this.ID = id;
        //}

        //public int GetID()
        //{
        //    return this.ID;
        //}

        //public void SetPosition(float x, float y, float z)
        //{
        //    transform.position = new Vector3(x, y, z);
        //}

        //public Vector3 GetPosition()
        //{
        //    return this.transform.position;
        //}

    }

}
