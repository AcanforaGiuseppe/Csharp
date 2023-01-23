using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    class Colliders
    {
        public List<CircleCollider> CircleColliders;
        public int Count
        {
            get { return CircleColliders.Count; }
        }

        public Colliders()
        {
            CircleColliders = new List<CircleCollider>();
        }

        public void AddCollider(CircleCollider c)
        {
            CircleColliders.Add(c);
        }

        public void SetActive(bool b)
        {
            for (int i = 0; i < CircleColliders.Count; i++)
            {
                CircleColliders[i].IsActive = b;
            }
        }

        public void ClearAll()
        {
            SetActive(false);
            CircleColliders.Clear();
        }
    }
}
