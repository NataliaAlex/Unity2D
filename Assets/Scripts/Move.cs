using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{
    [SerializeField] private Transform[] _points;  // Путь из точек
    [SerializeField] private float _duration = 1f;    // Время до точки
    [SerializeField] private float _idleTime = 1f;    // Время остановки
    [SerializeField] private float _speedRotate = 1f; // Скорость поворота
    [SerializeField] private bool _loop = true;      // Петля
    [SerializeField] private Animator _animator; // Animator
    
    private float _scale = 1f; // Скорость изменения размера

    private int _currentPointIndex = 0; // Номер текущей точки

    public static int IsWalking = Animator.StringToHash("isWalking"); // Параметр анимации для создания переменной

    public float IdleTime => _idleTime; // Свойство для других программ

    private void Start()
    {
        _animator = GetComponent<Animator>(); // Получение компонента Animator
        _animator.SetBool(IsWalking, true); // Начало анимации
    }


    public void MoveToNextPoint()
    {
        if (_currentPointIndex < _points.Length)
        {
            Vector3 direction = (_points[_currentPointIndex].position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                //Quaternion — класс в Unity, который используется для представления поворотов
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                StartCoroutine(RotateTo(targetRotation)); // Запуск корутины для поворота
            }

            _animator.SetBool(IsWalking, true);

            // Реализация движения через DOTween
            DOTween.Sequence()
                    .Join(transform.DOMove(_points[_currentPointIndex].position, _duration).SetEase(Ease.Linear)) // Движение к точке
                    .OnComplete(OnReachedPoint) // Вызов функции после анимации
                    .Append(transform.DOScale(new Vector3(2f, 2f, 2f), _scale).SetEase(Ease.Linear)) // Изменение масштаба
                    .Append(transform.DOScale(new Vector3(1f, 1f, 1f), _scale).SetEase(Ease.Linear)); // Изменение масштаба
        }
    }

    private IEnumerator RotateTo(Quaternion targetRotation)
    {
        while (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _speedRotate * Time.deltaTime);
            yield return null;
        }
    }

    private void OnReachedPoint()
    {
        _animator.SetBool(IsWalking, false);

        _currentPointIndex++;

        if (_currentPointIndex >= _points.Length)
        {
            if (_loop)
            {
                _currentPointIndex = 0; // Возврат к началу
            }
            else
            {
                return; // Конец пути
            }
        }
    }

    public void OnFootstep()
    {
    }
}