using UnityEngine;

public class AnimacaoPlayer : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade ajustável pelo Inspector
    public float velocidadeRotacao = 100f; // Velocidade de rotação ajustável
    private Animator animator;
    private bool atacando = false;
    private bool defendendo = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Captura entrada do teclado
        float vertical = Input.GetAxis("Vertical"); // W (+1) / S (-1)
        float horizontal = Input.GetAxis("Horizontal"); // A (-1) / D (+1)

        // Move para frente e para trás
        Vector3 movimento = transform.forward * vertical * velocidade * Time.deltaTime;
        transform.position += movimento;

        // Rotaciona para esquerda e direita
        float rotacao = horizontal * velocidadeRotacao * Time.deltaTime;
        transform.Rotate(0, rotacao, 0);

        // Controla animação de movimento
        bool estaAndando = vertical != 0;
        animator.SetBool("Andando", estaAndando);

        // Animação de ataque (Trigger)
        if (Input.GetMouseButtonDown(0) && !atacando)
        {
            atacando = true;
            animator.SetTrigger("Ataque");
            Invoke(nameof(ResetarAtaque), 0.5f); // Pequeno delay para permitir repetir o ataque
        }

        // Animação de defesa (Trigger)
        if (Input.GetMouseButtonDown(1) && !defendendo)
        {
            defendendo = true;
            animator.SetTrigger("Defesa");
            Invoke(nameof(ResetarDefesa), 0.5f); // Pequeno delay para permitir repetir a defesa
        }
    }

    void ResetarAtaque()
    {
        atacando = false;
    }

    void ResetarDefesa()
    {
        defendendo = false;
    }
}