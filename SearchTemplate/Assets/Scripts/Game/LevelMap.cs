using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class LevelMap : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _root;
        [SerializeField] private List<Vector3> _points;
        [SerializeField] private float _size;
        [SerializeField] private float _groundHeight;

        public float GroundHeight => _groundHeight;
        public float Size => _size;
        public IReadOnlyList<Vector3> Points => _points;

        public void AddPoint(Vector3 point)
        {
            if ((point.x * point.x > _size * _size) || (point.z * point.z > _size * _size))
            {
                return;
            }

            if (_points.Contains(point))
            {
                return;
            }
            _points.Add(point);
        }

        public void RemovePoint(Vector3 point)
        {
            _points.Remove(point);
        }

        public void SavePoints()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Vector3>));
            FileStream stream = new FileStream(Application.dataPath + "/Resources/level_data.xml", FileMode.Create);
            serializer.Serialize(stream, _points);
            stream.Close();
        }

        public void LoadPoints()
        {
            if (!File.Exists(Application.dataPath + "/Resources/level_data.xml"))
            {
                Debug.LogError("No level_data file found!");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Vector3>));
            FileStream stream = new FileStream(Application.dataPath + "/Resources/level_data.xml", FileMode.Open);
            _points = (List<Vector3>)serializer.Deserialize(stream);
            stream.Close();

            InstantiatePoints();
        }

        public void InstantiatePoints()
        {
            Clear();

            foreach (var p in _points.Distinct())
            {
                var prefab = PrefabUtility.InstantiatePrefab(_prefab, _root) as GameObject;
                prefab.transform.position = p;
            }
        }

        public void Clear()
        {
            var count = _root.childCount;
            for (var i = count - 1; i >= 0; i--)
            {
                DestroyImmediate(_root.GetChild(i).gameObject);
            }
        }

        public void ClearPoints()
        {
            Clear();
            _points.Clear();
        }
    }
}