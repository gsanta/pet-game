using game.scene.level;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float spawnTime = 5f;

        private float _timer;

        private bool _isEnabled = false;

        private EnemyFactory _enemyFactory;

        public void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public void Spawn(Level level)
        {
            // _enemyFactory.Create(level);
        }
        
        private void Update()
        {
            // if (!_isEnabled)
            // {
            //     return;
            // }
            //
            // _timer -= Time.deltaTime;
            // if (!(_timer <= 0f)) return;
            //
            // _enemyFactory.Create();
            // _timer = spawnTime;
        }
    }
}