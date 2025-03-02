using UnityEngine;

public class Movimentação : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade ajustável pelo Inspector

    void Update()
    {
        // Captura entrada do teclado (WASD)
        float horizontal = Input.GetAxis("Horizontal"); // A (-1) / D (+1)
        float vertical = Input.GetAxis("Vertical"); // W (+1) / S (-1)

        // Calcula direção do movimento
        Vector3 movimento = new Vector3(horizontal, 0, vertical) * velocidade * Time.deltaTime;

        // Move o objeto na direção correta
        transform.Translate(movimento, Space.World);
    }
}
