using UnityEngine;

public class EspadaPlayer : MonoBehaviour
{
    public int dano = 1; // Dano inicial da espada (pode ser modificado no Inspector)
    public int durabilidade = 100; // Durabilidade inicial da espada (pode ser modificada no Inspector)
    private bool espadaAtiva = true; // Para desativar a espada quando a durabilidade for 0

    private void Start()
    {
        // Adiciona o Rigidbody caso não tenha
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // Deixa o Rigidbody kinemático
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Se a espada não estiver ativa (durabilidade chegou a 0), não faz nada
        if (!espadaAtiva)
            return;

        // Verifica se a colisão é com um inimigo
        if (other.CompareTag("Inimigo"))
        {
            Debug.Log("Colidiu com um inimigo!");

            // Envia a mensagem "GetHit" para o objeto com a tag "Inimigo" e passa o dano
            other.gameObject.SendMessage("GetHit", dano, SendMessageOptions.DontRequireReceiver);

            // Exibe no console o dano causado e a durabilidade restante
            Debug.Log("Dano causado: " + dano + ". Durabilidade restante: " + durabilidade);

            // Reduz a durabilidade da espada
            durabilidade--;

            // Verifica se a durabilidade chegou a 0
            if (durabilidade <= 0)
            {
                // Exibe no console que a espada foi destruída
                Debug.Log("A espada foi destruída!");
                // Desativa a espada
                espadaAtiva = false;

                // Destrói a espada quando a durabilidade chega a 0
                Destroy(gameObject);
            }
        }
    }
}
