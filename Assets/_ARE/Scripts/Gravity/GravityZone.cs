using UnityEngine;

public class GravityZone : MonoBehaviour
{
    private Vector3 gravityDirection;
    public float gravityStrength = 9.81f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto que entra é o jogador
        {
            Rigidbody rb = other.GetComponentInParent<Rigidbody>();
            PlayerMovement pm = other.GetComponentInParent<PlayerMovement>();

            if (rb != null)
            {
                // Remove a gravidade padrão
                rb.useGravity = false;

                //gravityDirection = pm.isForceDown ? Vector3.up : Vector3.down;

                // Aplica a gravidade personalizada
                rb.AddForce(gravityDirection.normalized * -gravityStrength, ForceMode.Acceleration);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Restaura a gravidade padrão ao sair da zona
                rb.useGravity = true;
            }
        }
    }
}
