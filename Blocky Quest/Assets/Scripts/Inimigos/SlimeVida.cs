using UnityEngine;
using System.Collections;

public class SlimeVida : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public int quantidadeHP = 3; // Quantidade de HP do Slime
    private int vidaAtual; // Vida atual

    private Animator anim;
    private bool morto = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        vidaAtual = quantidadeHP; // Inicializa a vida com a quantidade de HP
    }

    // === RECEBENDO DANO ===
    public void GetHit(int amount)
    {
        if (morto) return;

        vidaAtual -= amount;
        Debug.Log($"⚔️ Slime recebeu {amount} de dano! Vida restante: {vidaAtual}");

        if (vidaAtual <= 0)
        {
            StartCoroutine(Morrer());
        }
    }

    // === Morrer ===
    IEnumerator Morrer()
    {
        morto = true;
        anim.SetBool("Morto", true); // Ativa animação de morte
        Debug.Log("💀 Slime morreu!");

        // Espera a duração da animação de morte
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // Espera 1.5 segundos após a animação de morte
        yield return new WaitForSeconds(1.5f);

        // Destrói o objeto após o tempo de espera
        Destroy(gameObject);
    }
}
