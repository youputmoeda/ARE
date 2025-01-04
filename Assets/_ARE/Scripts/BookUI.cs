using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [Header("Book Images")]
    [SerializeField] private RawImage bookFront;
    [SerializeField] private RawImage bookOpen;
    [SerializeField] private RawImage leftPage;
    [SerializeField] private RawImage rightPage;
    [SerializeField] private RawImage flipperRightPage;
    [SerializeField] private RawImage flipperLeftPage;

    [Header("Arrows")]
    [SerializeField] private Button arrowLeft;
    [SerializeField] private Button arrowRight;

    [Header("Close Button")]
    [SerializeField] private Button closeButton;

    [Header("Page Numbers")]
    [SerializeField] private TextMeshProUGUI leftPageNumberText;
    [SerializeField] private TextMeshProUGUI rightPageNumberText;

    [Header("Buttons on Pages")]
    [SerializeField] private List<GameObject> scenesButtons;

    [Header("Flip Page Animation")]
    [SerializeField] private float flipDuration = 0.5f;
    [SerializeField] private AnimationCurve flipCurve;

    private bool isFlipping = false;
    private float flipTimer = 0f;

    private Quaternion flipperStartRotation;
    private Quaternion flipperEndRotation;
    private bool isNextPageFlip;

    private int currentPage = 0;
    private bool isOpening = true;

    private PlayerController _player;

    private void Start()
    {
        arrowLeft.onClick.AddListener(PreviousPage);
        arrowRight.onClick.AddListener(NextPage);
        closeButton.onClick.AddListener(CloseBook);
        
        UpdatePage();

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (isFlipping)
        {
            flipTimer += Time.deltaTime;
            float t = flipTimer / flipDuration;
            t = flipCurve != null ? flipCurve.Evaluate(t) : t; // Aplica curva de animação (se houver)

            // Rotaciona suavemente a página flipper
            if (isNextPageFlip)
            {
                flipperRightPage.transform.rotation = Quaternion.Lerp(flipperStartRotation, flipperEndRotation, t);
            }
            else
            {
                flipperLeftPage.transform.rotation = Quaternion.Lerp(flipperStartRotation, flipperEndRotation, t);
            }

            // Finaliza a animação
            if (flipTimer >= flipDuration)
            {
                isFlipping = false;

                // Garante que a rotação final seja aplicada
                if (isNextPageFlip)
                {
                    flipperRightPage.transform.rotation = flipperEndRotation;
                    flipperRightPage.gameObject.SetActive(false); // Desativa a página flipper
                    flipperRightPage.transform.rotation = Quaternion.Euler(0, -180, 0); // Reseta para posição inicial
                }
                else
                {
                    flipperLeftPage.transform.rotation = flipperEndRotation;
                    flipperLeftPage.gameObject.SetActive(false); // Desativa a página flipper
                    flipperLeftPage.transform.rotation = Quaternion.Euler(0, 0, 0); // Reseta para posição inicial
                }

                // Atualiza as páginas após o flip
                UpdatePage();
            }
        }
    }

    public void DeactivateObjects()
    {
        flipperRightPage.transform.SetParent(gameObject.transform);
        flipperRightPage.gameObject.SetActive(false);
    }

    public void ActivateObject()
    {
        flipperRightPage.transform.SetParent(bookFront.transform);

        if (isOpening)
            flipperRightPage.transform.rotation = Quaternion.Euler(0, -90, 0);

        else
            flipperRightPage.transform.rotation = Quaternion.Euler(0, 0, 0);

        isOpening = !isOpening;
        flipperRightPage.gameObject.SetActive(true);
    }

    private void StartFlip(bool isNextPage)
    {
        if (isFlipping) return; // Impede flip simultâneo

        isFlipping = true;
        flipTimer = 0f;
        isNextPageFlip = isNextPage;

        // Configura as rotações de início e fim para o flipper
        if (isNextPage)
        {
            flipperRightPage.gameObject.SetActive(true);
            flipperStartRotation = Quaternion.Euler(0, 180, 0);
            flipperEndRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            flipperLeftPage.gameObject.SetActive(true); // Ativa a página flipper
            flipperStartRotation = Quaternion.Euler(0, 0, 0);    // Posição inicial do LeftPage
            flipperEndRotation = Quaternion.Euler(0, 180, 0);  // Posição final do LeftPage
        }
    }

    private void NextPage()
    {
        if (currentPage < Math.Round((scenesButtons.Count / 2.0) - 1, MidpointRounding.AwayFromZero))
        {
            StartFlip(true);
            currentPage++;
            Invoke(nameof(UpdatePage), 2f);
        }
    }

    private void PreviousPage()
    {
        if (currentPage > 0)
        {
            StartFlip(false);
            currentPage--;
            Invoke(nameof(UpdatePage), 2f);
        }
    }

    private void CloseBook()
    {
        currentPage = 0;
        Invoke(nameof(UpdatePage), 2f);
        Invoke(nameof(GiveControlls), 2f);
    }

    private void GiveControlls()
    {
        gameObject.SetActive(false);
        SetCursorStateScript.SetCursorState(false);

        if (_player)
            _player.enabled = true;
    }

    private void UpdatePage()
    {
        if (leftPageNumberText != null)
            leftPageNumberText.text = (currentPage * 2 + 1).ToString();

        if (rightPageNumberText != null)
            rightPageNumberText.text = (currentPage * 2 + 2).ToString();

        for (int i = 0; i < scenesButtons.Count; i++)
        {
            scenesButtons[i].SetActive(i == currentPage * 2 || i == currentPage * 2 + 1);
        }

        arrowLeft.interactable = currentPage > 0;
        arrowRight.interactable = currentPage < Math.Round((scenesButtons.Count / 2.0) - 1, MidpointRounding.AwayFromZero);
    }
}
