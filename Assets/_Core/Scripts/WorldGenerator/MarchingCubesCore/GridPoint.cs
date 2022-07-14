using UnityEngine;

namespace _Core.Scripts.MarchingCubesCore
{

    public class GridPoint
    {
        private Vector3 _position = Vector3.zero;
        private bool _on = false;

        public Vector3 Position
        {
            get { return _position; }
            set { _position = new Vector3(value.x, value.y, value.z); }
        }

        public bool On
        {
            get { return _on; }
            set { _on = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Position, On);
        }
    }
}
