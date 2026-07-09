using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // 👈 UI 슬라이더 제어를 위해 필수 추가

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 dir;

    public float moveSpeed;

    [SerializeField] float maxHP;
    [SerializeField] float nowHP;

    // 변경: 강사님 커스텀 HPBar 대신 하이어라키의 UI Slider를 직접 연결할 수 있도록 수정
    [SerializeField] Slider hpBar;

    Animator animator;
    int isMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isMove = Animator.StringToHash("isMove");

        // 게임 시작 시 현재 체력을 최대 체력으로 초기화
        nowHP = maxHP;

        // 슬라이더 초기화 (100% 상태로 채우기)
        if (hpBar != null)
        {
            hpBar.value = 1f;
        }
    }

    void Update()
    {
        dir = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            dir += Vector2.up;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            dir += Vector2.left;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            dir += Vector2.down;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            dir += Vector2.right;
        }

        dir = dir.normalized;

        if (animator != null)
        {
            if (dir == Vector2.zero)
            {
                animator.SetBool(isMove, false);
            }
            else
            {
                animator.SetBool(isMove, true);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = dir * moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        nowHP -= damage;

        // 유니티 기본 슬라이더 게이지 반영 (C# 정수 버림 버그 방지를 위해 float 형변환 연산)
        if (hpBar != null)
        {
            hpBar.value = (float)nowHP / maxHP;
        }

        if (nowHP <= 0)
        {
            nowHP = 0;
            Die();
        }
    }

    void Die()
    {
        // 1. 맵에 남은 몬스터들 싹 정리 (기존 코드)
        StageManager.instance.ClearMonsterList();

        // 2. 추가: 플레이어가 죽었으므로 화면에 게임오버 UI 팝업과 재시작 버튼 띄우기
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}
