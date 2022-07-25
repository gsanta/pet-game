using System;
using AI.state.character.states;
using Character.player;
using Character.utils;
using Characters.Common;
using Characters.Helpers;
using GameLogic;
using UnityEngine;

namespace Characters.Enemies
{
    public class Enemy : MonoBehaviour, ICharacter, IMoveAble
    {
        public float moveSpeed = 5f;
        
        private Animator _animator;
        
        private Rigidbody2D _rb;
        
        private Vector2 _movement;
        
        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;

        private GameManager _gameManager;

        private Shooting _shooting;

        private Health _health;
        
        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");
        
        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Direction _moveDirection = Direction.Down;

        private bool _isActive = false;

        private RoamingState _roamingState;
        
        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, GameManager gameManager)
        {
            _playerStore = playerStore;
            _enemyStore = enemyStore;
            _gameManager = gameManager;
        }

        public void SetRoamingState(RoamingState roamingState)
        {
            _roamingState = roamingState;
            _roamingState.Start();
        }

        private void Start()
        {
            _shooting = GetComponent<Shooting>();
            _health = GetComponent<Health>();
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            InvokeRepeating(nameof(Shoot), 0, 2);
        }

        private void Update()
        {
            UpdateActive();
            
            if (!_isActive)
            {
                return;
            }
            
            var player = _playerStore.GetActivePlayer();
            var direction = player.transform.position - transform.position;
            var radians = Mathf.Atan2(direction.y, direction.x);
            var rotationVector = new Vector2((float) Math.Cos(radians), (float) Math.Sin(radians));
            direction.Normalize();
            _movement = direction;
            
            _moveDirection = MovementUtil.UpdateMoveDirection(_movement, _moveDirection);
        
            // _animator.SetFloat(HorizontalMovement, rotationVector.x);
            // _animator.SetFloat(VerticalMovement, rotationVector.y);

            _roamingState?.Update();
        }

        private void UpdateActive()
        {
            if (!_gameManager.IsGameStarted())
            {
                if (_isActive)
                {
                    _isActive = false;
                    CancelInvoke(nameof(Shoot));
                }
            }
            else
            {
                _isActive = true;
            }
        }

        private void Shoot()
        {
            _shooting.Shoot();
        }

        // private void LateUpdate()
        // {
        //     MoveCharacter(_movement);
        // }

        private void MoveCharacter(Vector2 direction)
        {
            _rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
        }

        public Direction GetMoveDirection()
        {
            return _moveDirection;
        }

        public void SetMovement(Vector2 movement)
        {
            _movement = movement;
        }

        public Vector2 GetMovement()
        {
            return _movement;
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public Health GetHealth()
        {
            return _health;
        }

        public void Die()
        {
            _enemyStore.Remove(this);
            Destroy(gameObject);
        }
    }
}