using UnityEngine;
using Cinemachine;

public class CinemachineLookahead : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private Player player;
    private CinemachineFramingTransposer framingTransposer;

    [SerializeField] private float lookaheadDistance = 2f; // Distance to lookahead
    [SerializeField] private float transitionSpeed = 5f;   // Speed of transition

    private Vector3 targetOffset;
    private bool isSwitchingDirection;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        targetOffset = Vector3.zero;

        // Initialize Dead Zone and Soft Zone
        framingTransposer.m_DeadZoneWidth = 0.1f;
        framingTransposer.m_DeadZoneHeight = 0.3f;
        framingTransposer.m_SoftZoneWidth = 0.2f;
        framingTransposer.m_SoftZoneHeight = 0.3f;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (player.IsWalking())
        {
            if (horizontal > 0) // Moving right
            {
                targetOffset = new Vector3(lookaheadDistance, 0, 0);
                isSwitchingDirection = true;
            }
            else if (horizontal < 0) // Moving left
            {
                targetOffset = new Vector3(-lookaheadDistance, 0, 0);
                isSwitchingDirection = true;
            }
            else
            {
                isSwitchingDirection = false;
            }
        }
        else
        {
            isSwitchingDirection = false;
        }

        // Smoothly transition the offset
        if (isSwitchingDirection)
        {
            framingTransposer.m_TrackedObjectOffset = Vector3.Lerp(framingTransposer.m_TrackedObjectOffset, targetOffset, transitionSpeed * Time.deltaTime);
        }
    }
}
