using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float _minMovingDuration;
        [SerializeField]
        private float _maxMovingDuration;

        [SerializeField]
        private ParticleSystem _deathParticlesPrefab;
        
        private SpriteRenderer _enemySprite;
        private Sequence _moveSequence;
        
        private float _minPointX;
        private float _maxPointX;

        private float _delayBetweenMovements;

        public void Initialize(float minPointX, float maxPointX, float delayBetweenMovements)
        {
            _enemySprite = GetComponent<SpriteRenderer>();
            var offsetX = _enemySprite.bounds.size.x / 2;
            
            _minPointX = minPointX + offsetX;
            _maxPointX = maxPointX - offsetX;
            _delayBetweenMovements = delayBetweenMovements;
            
            Move();
        }
        
        // Вызывается по событию уничтожения врага
        [UsedImplicitly]
        public void Destroy()
        {
            Instantiate(_deathParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void Move()
        {
            var randomMovementDuration = GetRandomMovementDuration();
            var nextPosition = GetNewRandomPosition();

            _moveSequence = DOTween.Sequence();
            _moveSequence.Append(transform.DOMoveX(nextPosition, randomMovementDuration));
            _moveSequence.AppendInterval(_delayBetweenMovements);
            _moveSequence.OnComplete(Move);
        }
        
        private float GetRandomMovementDuration()
        {
            return Random.Range(_minMovingDuration, _maxMovingDuration);
        }

        private float GetNewRandomPosition()
        {
            return Random.Range(_minPointX, _maxPointX);
        }

        private void OnDestroy()
        {
            _moveSequence.Kill();
        }
    }
}
