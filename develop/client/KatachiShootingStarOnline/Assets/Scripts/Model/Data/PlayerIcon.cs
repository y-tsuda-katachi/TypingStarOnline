using TMPro;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private float _moveSpeed = 3f;
    private Animator animator;
    private RectTransform rectTransform;
    private RectTransform parentTransform;
    private Player player;
    private float progress;
    private float width;

    public Player Player
    {
        get => player;
        private set => player = value;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
        parentTransform = rectTransform?.parent.GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        rectTransform.anchoredPosition =
            Vector2.Lerp(
                rectTransform.anchoredPosition,
                rectTransform.right * (width * progress),
                Time.deltaTime * _moveSpeed);
    }

    public PlayerIcon Init(Player player)
    {
        Player = player;
        progress = 0f;
        width = parentTransform.rect.width;
        InitLabel();
        return this;
    }

    public void UpdateProgress(float progress)
    {
        this.progress = progress;
        animator.SetTrigger("Forward");
    }
    private void InitLabel() => _nameLabel.text = player?.playerName;
}