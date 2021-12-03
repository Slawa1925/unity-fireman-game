using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class ZombieMap : MonoBehaviour
    {
        [SerializeField]
        private GameObject _root;

        private List<ZombieController> _zombieComponents = new List<ZombieController>();

        private void Awake()
        {
            _zombieComponents = _root.gameObject.GetComponentsInChildren<ZombieController>().ToList();
        }

        public List<Vector3> AlivePositions() => _zombieComponents
            .Where(z => z.IsAlive)
            .Select(z=>z.gameObject.transform.position)
            .ToList();
    }
}