using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlipPage : MonoBehaviour
{
    private enum ButtonType { NextButton, PreviousButton }

    [SerializeField] private Button nextBtn; // Bot?o de avan?ar
    [SerializeField] private Button previousBtn; // Bot?o de retroceder
    [SerializeField] private Button closeBtn;

    [SerializeField] private Button[] pageButtons; // Array para os bot?es de p?gina (1 a 6)

    // Bot?es extras para ativar/desativar
    [SerializeField] private Button extraButton1;
    [SerializeField] private Button extraButton2;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool isClicked;
    private float rotationElapsed;

    [SerializeField] private float rotationDuration = 1f;
    [SerializeField] private float rotationSpeed = 180f;

    // P?ginas do livro
    private int leftPage = 1;
    private int rightPage = 2;

    // Refer?ncias para os textos das p?ginas
    [SerializeField] private TextMeshProUGUI leftPageText;
    [SerializeField] private TextMeshProUGUI rightPageText;

    void Start()
    {
        startRotation = transform.rotation;

        // Configura??o dos bot?es de navega??o
        if (nextBtn != null)
            nextBtn.onClick.AddListener(() => StartFlipPage(ButtonType.PreviousButton));

        if (previousBtn != null)
            previousBtn.onClick.AddListener(() => StartFlipPage(ButtonType.NextButton));

        if (closeBtn != null)
            closeBtn.onClick.AddListener(() => CloseBook());

        UpdatePageNumbers();
        UpdatePageButtons(); // Configura o estado inicial dos bot?es
        UpdateExtraButtons(); // Configura o estado inicial dos bot?es extras
    }

    void Update()
    {
        if (isClicked)
        {
            rotationElapsed += Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (rotationElapsed >= rotationDuration)
            {
                isClicked = false;
                rotationElapsed = 0f;
                transform.rotation = startRotation;

                // Atualiza as p?ginas quando a rota??o termina
                UpdatePageNumbers();
                UpdatePageButtons(); // Atualiza os bot?es de acordo com as p?ginas vis?veis
                UpdateExtraButtons(); // Atualiza o estado dos bot?es extras
            }
        }
    }

    private void StartFlipPage(ButtonType type)
    {
        if (isClicked || !gameObject.activeSelf) return; // Verifica se o livro est? ativo

        isClicked = true;
        rotationElapsed = 0f;

        if (type == ButtonType.NextButton)
        {
            targetRotation = startRotation * Quaternion.Euler(0, -180, 0);
            IncrementPages();
        }
        else if (type == ButtonType.PreviousButton)
        {
            targetRotation = startRotation * Quaternion.Euler(0, 180, 0);
            DecrementPages();
        }

        nextBtn.interactable = false;
        previousBtn.interactable = false;
    }

    private void IncrementPages()
    {
        leftPage += 2;
        rightPage += 2;
    }

    private void DecrementPages()
    {
        if (leftPage > 1)
        {
            leftPage -= 2;
            rightPage -= 2;
        }
    }

    private void UpdatePageNumbers()
    {
        // Atualiza os textos nas p?ginas
        if (leftPageText != null)
            leftPageText.text = leftPage.ToString();

        if (rightPageText != null)
            rightPageText.text = rightPage.ToString();

        Debug.Log($"Left Page: {leftPage}, Right Page: {rightPage}");
    }

    private void UpdatePageButtons()
    {
        // Desativa todos os bot?es inicialmente
        for (int i = 0; i < pageButtons.Length; i++)
        {
            pageButtons[i].gameObject.SetActive(false); // Desativa todos os bot?es inicialmente
        }

        // L?gica para ativar os bot?es conforme as p?ginas
        bool activateButtonsPage1_2 = (leftPage == 1 && rightPage == 2);
        bool activateButtonsPage3_4 = (leftPage == 3 && rightPage == 4);
        bool activateButtonsPage5_6 = (leftPage == 5 && rightPage == 6);

        // Ativa os bot?es corretos para cada conjunto de p?ginas
        if (activateButtonsPage1_2)
        {
            pageButtons[0].gameObject.SetActive(true); // Bot?o para a p?gina 1
            pageButtons[1].gameObject.SetActive(true); // Bot?o para a p?gina 2
        }
        else if (activateButtonsPage3_4)
        {
            pageButtons[2].gameObject.SetActive(true); // Bot?o para a p?gina 3
            pageButtons[3].gameObject.SetActive(true); // Bot?o para a p?gina 4
        }
        else if (activateButtonsPage5_6)
        {
            pageButtons[4].gameObject.SetActive(true); // Bot?o para a p?gina 5
            pageButtons[5].gameObject.SetActive(true); // Bot?o para a p?gina 6
        }

        // Ativa os bot?es de navega??o (pr?ximo e anterior)
        nextBtn.interactable = !activateButtonsPage1_2;
        previousBtn.interactable = !activateButtonsPage5_6;

        // Atualiza a interatividade dos bot?es
        for (int i = 0; i < pageButtons.Length; i++)
        {
            int page = i + 1;
            bool isActive = (page == leftPage || page == rightPage);
            pageButtons[i].interactable = isActive;

            Debug.Log($"Button {page}: {(isActive ? "Active" : "Inactive")}");
        }


        // Debug para verificar a ativa??o dos bot?es extras
        Debug.Log($"Extra Buttons Active - Pages 1-2: {activateButtonsPage1_2}, Pages 3-4: {activateButtonsPage3_4}, Pages 5-6: {activateButtonsPage5_6}");
    }


    private void UpdateExtraButtons()
    {
        // Define quais bot?es extra ser?o ativados com base nas p?ginas
        bool activateButtonsPage1_2 = (leftPage == 1 && rightPage == 2);
        bool activateButtonsPage3_4 = (leftPage == 3 && rightPage == 4);
        bool activateButtonsPage5_6 = (leftPage == 5 && rightPage == 6);

        // Para as p?ginas 1 e 2, ativar os bot?es extra 1 e 2
        if (extraButton1 != null)
            extraButton1.gameObject.SetActive(activateButtonsPage1_2);
        if (extraButton2 != null)
            extraButton2.gameObject.SetActive(activateButtonsPage1_2);

        // Para as p?ginas 3 e 4, ativar bot?es espec?ficos para essas p?ginas
        if (extraButton1 != null)
            extraButton1.gameObject.SetActive(activateButtonsPage3_4);
        if (extraButton2 != null)
            extraButton2.gameObject.SetActive(activateButtonsPage3_4);

        // Para as p?ginas 5 e 6, ativar bot?es espec?ficos para essas p?ginas
        if (extraButton1 != null)
            extraButton1.gameObject.SetActive(activateButtonsPage5_6);
        if (extraButton2 != null)
            extraButton2.gameObject.SetActive(activateButtonsPage5_6);

        Debug.Log($"Extra Buttons Active - Pages 1-2: {activateButtonsPage1_2}, Pages 3-4: {activateButtonsPage3_4}, Pages 5-6: {activateButtonsPage5_6}");
    }

    public void CloseBook()
    {
        // Reseta a rota??o para a inicial
        transform.rotation = startRotation;

        // Reseta as p?ginas para o in?cio do livro
        leftPage = 1;
        rightPage = 2;

        // Atualiza os n?meros das p?ginas para refletir o estado inicial
        UpdatePageNumbers();

        // Mostra os textos das p?ginas ao fechar (se necess?rio)
        if (leftPageText != null)
            leftPageText.text = leftPage.ToString();

        if (rightPageText != null)
            rightPageText.text = rightPage.ToString();

        // Atualiza os bot?es das p?ginas
        UpdatePageButtons();
        UpdateExtraButtons(); // Garante que os bot?es extras fiquem ativos ao fechar o livro

        // Dispara o evento de fechamento (caso necess?rio)
        AppEvents.CloseBookFunction();

        Debug.Log("Livro fechado. P?ginas reiniciadas para 1 e 2.");
    }
}
