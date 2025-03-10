using UnityEngine;

public class SlimeMovimentacao : MonoBehaviour
{
    [Header("Configurações de Movimentação")]
    public Transform[] pontos; // Lista de pontos de patrulha
    public float velocidade = 2f; // Velocidade de movimento do Slime
    private int indiceAtual = 0;

    private Animator anim;
    private bool pausado = false; // Flag para verificar se o movimento está pausado

    void Start()
    {
        anim = GetComponent<Animator>();
        EscolherNovoDestino();
    }

    void Update()
    {
        if (!pausado)
        {
            MoverParaDestino();
        }
    }

    void MoverParaDestino()
    {
        if (pontos.Length == 0) return;

        Vector3 destino = pontos[indiceAtual].position;
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);

        // Faz o Slime girar para olhar para o destino
        Vector3 direcao = (destino - transform.position).normalized;
        if (direcao != Vector3.zero)
        {
            Quaternion rotacao = Quaternion.LookRotation(direcao);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, Time.deltaTime * 5f);
        }

        // Ativa a animação de "Andar"
        bool estaAndando = Vector3.Distance(transform.position, destino) > 0.1f;
        anim.SetBool("Andar", estaAndando);

        // Se chegou ao destino, escolhe o próximo ponto
        if (!estaAndando)
        {
            EscolherNovoDestino();
        }
    }

    void EscolherNovoDestino()
    {
        if (pontos.Length == 0) return;

        int novoIndice;
        do
        {
            novoIndice = Random.Range(0, pontos.Length);
        } while (novoIndice == indiceAtual);

        indiceAtual = novoIndice;
    }

    // Função para pausar o movimento
    public void PausarMovimento()
    {
        pausado = true;
    }

    // Função para retomar o movimento
    public void RetomarMovimento()
    {
        pausado = false;
    }
}
