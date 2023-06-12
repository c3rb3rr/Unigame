using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private BossController _bossController;
    
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        _bossController = GetComponent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    
}