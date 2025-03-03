using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade ajustável pelo Inspector
    public float velocidadeRotacao = 100f; // Velocidade de rotação ajustável
    private Animator animator;

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

        // Controla animação
        bool estaAndando = vertical != 0;
        animator.SetBool("Andando", estaAndando);
    }
}
