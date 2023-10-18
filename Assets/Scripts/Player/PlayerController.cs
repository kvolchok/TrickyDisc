using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _movementVelocity;

        [SerializeField]
        private SpriteRenderer _aimSprite;

        [SerializeField]
        private PlayerRotator _playerRotator;

        [SerializeField]
        private PlayerMoveTimeLimiter _playerMoveTimeLimiter;

        [SerializeField]
        private AudioSource _moveAudioClip;

        [SerializeField]
        private ParticleSystem _deathParticlesPrefab;
        
        private Vector3 _startPosition;
        private bool _isMoving;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        // Вызывается через коллбэк кнопки передвижения
        [UsedImplicitly]
        public void Move()
        {
            if (!_isMoving)
            {
                _isMoving = !_isMoving;
                _aimSprite.enabled = false;
                _playerRotator.StopRotation();
                _playerMoveTimeLimiter.StopTimeLimiter();
                _moveAudioClip.Play();
                
                _rigidbody.velocity = transform.up * _movementVelocity;
            }
        }

        // Вызывается через ивент при коллизии игрока с врагом
        [UsedImplicitly]
        public void ChangeDirection()
        {
            _rigidbody.velocity *= -1;
        }
        
        // вызывается через ивент при возвращении игрока в startPointTrigger
        [UsedImplicitly]
        public void ResetPosition()
        {
            if (_isMoving)
            {
                _isMoving = !_isMoving;
                _aimSprite.enabled = true;
                _playerRotator.StartRotation();
                _playerMoveTimeLimiter.RestartTimeLimiter();
                
                _rigidbody.velocity = Vector2.zero;
                transform.position = _startPosition;
            }
        }
        
        // Вызывается по ивенту смерти игрока
        [UsedImplicitly]
        public void OnPlayerDied()
        {
            Instantiate(_deathParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}