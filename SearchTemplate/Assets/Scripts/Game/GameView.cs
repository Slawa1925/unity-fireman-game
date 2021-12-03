using System.Linq;
using UnityEngine;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Health _player;
        [SerializeField] private ZombieMap _zombieMap;
        
        [SerializeField] private GameObject _winBlock;
        [SerializeField] private GameObject _gameOverBlock;


        private void Update()
        {
            if (!_zombieMap.AlivePositions().Any())
            {
                _winBlock.SetActive(true);
                return;
            }

            if (_player.HealthPoints <= 0)
            {
                _gameOverBlock.SetActive(true);
                return;
            }

            //_player.TakeDamage(Time.deltaTime);
        }
    }
}