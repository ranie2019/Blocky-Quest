using UnityEngine;

public class AnimacaoPlayer : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [SerializeField] private float velocidade = 5f; // Velocidade de movimento ajustável
    [SerializeField] private float velocidadeRotacao = 100f; // Velocidade de rotação ajustável

    private Animator animator;
    private Rigidbody rb;
    private bool atacando = false;
    private bool defendendo = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Chama as funções de movimento, rotação e ações do jogador
        ProcessarMovimento();
        ProcessarRotacao();
        ProcessarAtaqueEDefesa();
        AtualizarAnimações();
    }

    // Lógica de Movimento
    private void ProcessarMovimento()
    {
        float vertical = Input.GetAxisRaw("Vertical"); // W (+1) / S (-1)

        // Movimenta o jogador para frente e para trás
        Vector3 movimento = transform.forward * vertical * velocidade * Time.deltaTime;
        rb.MovePosition(rb.position + movimento);
    }

    // Lógica de Rotação
    private void ProcessarRotacao()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A (-1) / D (+1)

        // Realiza a rotação do jogador apenas quando uma tecla de direção for pressionada
        if (horizontal != 0)
        {
            float rotacao = horizontal * velocidadeRotacao * Time.deltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotacao, 0));
        }
    }

    // Lógica de Ataque e Defesa
    private void ProcessarAtaqueEDefesa()
    {
        // Verifica o ataque
        if (Input.GetMouseButtonDown(0) && !atacando)
        {
            atacando = true;
            animator.SetBool("Ataque", true);
            Invoke(nameof(ResetarAtaque), 0.5f); // Duração do ataque
        }

        // Verifica a defesa (segurando o botão direito do mouse)
        defendendo = Input.GetMouseButton(1);
        animator.SetBool("Defesa", defendendo);
    }

    // Atualiza as animações de movimento
    private void AtualizarAnimações()
    {
        // Define se o jogador está andando ou não com base no movimento vertical
        animator.SetBool("Andando", Input.GetAxisRaw("Vertical") != 0);
    }

    // Função que reseta o estado de ataque
    void ResetarAtaque()
    {
        atacando = false;
        animator.SetBool("Ataque", false);
    }
}
