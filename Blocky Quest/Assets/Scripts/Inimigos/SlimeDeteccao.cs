using UnityEngine;

public class SlimeDeteccao : MonoBehaviour
{
    [Header("Configurações de Detecção do Player")]
    public CapsuleCollider zonaDeteccao; // CapsuleCollider que detecta o Player
    private Animator anim;
    private SlimeMovimentacao slimeMovimentacao; // Referência ao script de movimentação do Slime
    private Transform playerTransform; // Referência ao transform do Player

    void Start()
    {
        anim = GetComponent<Animator>();
        slimeMovimentacao = GetComponent<SlimeMovimentacao>(); // Atribuindo o script de movimentação do Slime
        playerTransform = GameObject.FindWithTag("Player").transform; // Encontrando o Player na cena

        // Certifique-se de que a CapsuleCollider está configurada como "isTrigger"
        if (!zonaDeteccao.isTrigger)
        {
            zonaDeteccao.isTrigger = true;
        }
    }

    // === DETECÇÃO DO PLAYER COM COLLIDER ===
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Quando o Player colidir com o Collider, para o Slime e ativa a animação de alerta
            Debug.Log("⚠️ Slime detectou o Player!");

            // Parar o movimento do Slime
            slimeMovimentacao.PausarMovimento();

            // Ativar a animação de Alerta
            anim.SetBool("Alerta", true);

            // Desativar a animação de andar, pois o Slime parou
            anim.SetBool("Andar", false);
        }
    }

    // Quando o Player sair do raio de detecção
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Quando o Player sair, o Slime retoma a patrulha
            Debug.Log("🛑 Slime perdeu o Player!");

            // Retomar o movimento do Slime
            slimeMovimentacao.RetomarMovimento();

            // Desativar a animação de alerta
            anim.SetBool("Alerta", false);

            // Ativar a animação de andar, pois o Slime retoma o movimento
            anim.SetBool("Andar", true);
        }
    }

    void Update()
    {
        // Se o Slime estiver em estado de alerta, sempre olhe para o Player
        if (anim.GetBool("Alerta"))
        {
            LookAtPlayer();
        }
    }

    // Método para fazer o Slime olhar para o Player
    void LookAtPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0; // Ignorar a diferença de altura (faz o slime girar apenas no eixo Y)
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Rotação suave
        }
    }
}
