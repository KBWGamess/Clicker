using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Clicker : MonoBehaviour
{
    public int totalCoins; // ����� ���������� �����
    public int totalClicks; // ����� ���������� ������

    public TextMeshProUGUI coinsText; // ��������� ���� ��� ����������� ���������� �����

    // ���������� �����
    private int clickPower; // ���� �����
    private float upgradeMultiplier; // ��������� ��� ���������
    private int currentUpgradeCost; // ��������� ���������
    public TextMeshProUGUI clickPowerText; // ����� ��� ����������� ���� �����
    public TextMeshProUGUI currentUpgradeCostText; // ����� ��� ����������� ��������� ���������

    // ���������� ���������� ������
    private int passiveIncome; // ��������� �����
    private int passiveIncomeCost; // ��������� ���������� ������
    public TextMeshProUGUI passiveIncomeText; // ����� ��� ����������� ���������� ������
    public TextMeshProUGUI passiveIncomeCostText; // ����� ��� ����������� ��������� ���������� ������

    // �������� ������
    private int doubleIncomeCost; // ��������� �������� ������

    // �������������� ���������� ��� ���������� ������
    private int passiveIncomeAmount; // ��������� ����� �� ���
    private int doubleIncomeMultiplier; // ��������� ��� ��������

    // ������
    public Button upgradeClickButton; // ������ ��� ��������� �����
    public Button upgradePassiveButton; // ������ ��� ��������� ���������� ������
    public Button doubleIncomeButton; // ������ ��� �������� ������




    // ����������� ��� ������
    public Image firstQuestImage; // ����������� ��� ���������� ������
    public Image secondQuestImage;
    public Image thirdQuestImage;
    public Image fourthQuestImage;
    public Image fifteenQuestImage;
    public Image sixthQuestImage;
    public Image sevenQuestImage;
    public Image eigitQuestImage;
    public Image nineQuestImage;



    private bool firstQuest = false;
    private bool secondQuest = false;
    private bool thirdQuest = false;
    private bool fourthQuest = false;
    private bool fifteenQuest = false;
    private bool sixthQuest = false;
    private bool sevenQuest = false;
    private bool eigitQuest = false;
    private bool nineQuest = false;



    void Start()
    {
        LoadGameData();

        if (passiveIncome > 0)
        {
            int passiveIncomeThreshold = 5; // ����� ��� ��������� ��������
            int passiveIncomeMultiplier = passiveIncome / passiveIncomeThreshold;

            for (int i = 1; i <= passiveIncomeMultiplier; i++)
            {
                StartCoroutine(DelayedPassiveIncome(i * passiveIncomeThreshold));
            }
        }

        if(PlayerPrefs.GetInt("firstQuest", 0) == 1)
        {
            firstQuest = true;
            firstQuestImage.color = Color.white; 
        }

        if (PlayerPrefs.GetInt("secondQuest", 0) == 1)
        {
            secondQuest = true;
            secondQuestImage.color = Color.white;
        }

        if (PlayerPrefs.GetInt("thirdQuest", 0) == 1)
        {
            thirdQuest = true;
            thirdQuestImage.color = Color.white;
        }

        if (PlayerPrefs.GetInt("fourthQuest", 0) == 1)
        {
            fourthQuest = true;
            fourthQuestImage.color = Color.white;
        }

        if (PlayerPrefs.GetInt("fifteenQuest", 0) == 1)
        {
            fifteenQuest = true;
            fifteenQuestImage.color = Color.white;
        }

        if (PlayerPrefs.GetInt("sixthnQuest", 0) == 1)
        {
            sixthQuest = true;
            sixthQuestImage.color = Color.white;
        }

        if (PlayerPrefs.GetInt("sevenQuest", 0) == 1)
        {
            sevenQuest = true;
            sevenQuestImage.color = Color.white;
        }

        if (PlayerPrefs.GetInt("eigitQuest", 0) == 1)
        {
            eigitQuest = true;
            eigitQuestImage.color = Color.white;
        }

        if (PlayerPrefs.GetInt("nineQuest", 0) == 1)
        {
            nineQuest = true;
            nineQuestImage.color = Color.white;
        }
    }

    void Update()
    {
        UpdateButtonStates(); // �������� ��������� ������
        UpdateUIText(); // �������� ����� �� ������
        CheckQuestAchievements(); // ��������� ���������� �������

        SaveGameData();

        Debug.Log(passiveIncome);
    }

    private void UpdateButtonStates()
    {
        // ���������� ���������� ������ �� ������ ���������� �����
        upgradeClickButton.interactable = totalCoins >= currentUpgradeCost;
        upgradePassiveButton.interactable = totalCoins >= passiveIncomeCost;
        doubleIncomeButton.interactable = totalCoins >= 10001;
    }

    private void UpdateUIText()
    {
        // ���������� ��������� �����
        coinsText.text = totalCoins.ToString();
        currentUpgradeCostText.text = "(" + currentUpgradeCost.ToString() + " coins)";
        clickPowerText.text = "Click = " + clickPower.ToString();
        passiveIncomeText.text = "Passive = " + passiveIncome.ToString();
        passiveIncomeCostText.text = "(" + passiveIncomeCost.ToString() + " coins)";
    }

    public void Click()
    {
        totalCoins += clickPower; // ��������� ���������� ����� �� ���� ����
        totalClicks += 1; // ��������� ������� ������

        SaveGameData(); // ��������� ������ ����
    }

    private void CheckQuestAchievements()
    {
        // �������� ���������� ������� ��� ���������� ������
        if (totalClicks > 999 && !firstQuest)
        {
            firstQuestImage.color = Color.white; // ������ ���� ����������� ��� ���������� ����
            passiveIncome += 10;
            PlayerPrefs.SetInt("firstQuest", 1);
            firstQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // ��������� ����� ������ �������
            }
        }
        if (totalClicks > 4999 && !secondQuest)
        {
            secondQuestImage.color = Color.white; // ������ ���� ����������� ��� ���������� ���
            passiveIncome += 50;
            PlayerPrefs.SetInt("secondQuest", 1);
            secondQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // ��������� ����� ������ ������
            }
        }
        if (totalClicks > 9999 && !thirdQuest)
        {
            thirdQuestImage.color = Color.white; // ������ ���� ����������� ��� ���������� ���
            passiveIncome += 100;
            PlayerPrefs.SetInt("thirdQuest", 1);
            thirdQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // ��������� ����� ������ ������
            }
        }
        if (totalClicks > 499 && !fourthQuest)
        {
            fourthQuestImage.color = Color.white; // ������ ���� ����������� ��� ���������� ���
            clickPower += 5;
            PlayerPrefs.SetInt("fourthQuest", 1);
            fourthQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // ��������� ����� ������ ������
            }
        }
        if (totalClicks > 2499 && !fifteenQuest)
        {
            fifteenQuestImage.color = Color.white; // ������ ���� ����������� ��� ���������� ���
            clickPower += 25;
            PlayerPrefs.SetInt("fifteenQuest", 1);
            fifteenQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // ��������� ����� ������ ������
            }
        }
        if (totalClicks > 5999 && !sixthQuest)
        {
            sixthQuestImage.color = Color.white; // ������ ���� ����������� ��� ���������� ���
            clickPower += 60;
            PlayerPrefs.SetInt("sixthQuest", 1);
            sixthQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // ��������� ����� ������ ������
            }
        }
        if (totalClicks > 3999 && !sevenQuest)
        {
            sevenQuestImage.color = Color.white; // ������ ���� ����������� ��� ���������� ���
            totalCoins += 100000;
            PlayerPrefs.SetInt("sevenQuest", 1);
            sevenQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // ��������� ����� ������ ������
            }
        }
        if (totalClicks > 8999 && !eigitQuest)
        {
            eigitQuestImage.color = Color.white; // ������ ���� ����������� ��� ���������� ���
            totalCoins += 250000;
            PlayerPrefs.SetInt("eigitQuest", 1);
            eigitQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // ��������� ����� ������ ������
            }
        }
        if (totalClicks > 19999 && !nineQuest)
        {
            nineQuestImage.color = Color.white; // ������ ���� ����������� ��� ���������� ���
            totalCoins += 2000000;
            PlayerPrefs.SetInt("nineQuest", 1);
            nineQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // ��������� ����� ������ ������
            }
        }
    }

    public void UpgradeClickPower()
    {
        totalCoins -= currentUpgradeCost; // ��������� ���������� ����� �� ��������� ���������
        currentUpgradeCost += 250; // ��������� ��������� ���������� ���������
        clickPower += 5; // ��������� ���� �����

        SaveGameData(); // ��������� ������ ����
    }

    public void UpgradePassiveIncome()
    {
        totalCoins -= passiveIncomeCost; // ��������� ���������� ����� �� ��������� ��������� ������
        passiveIncomeCost += 500; // ��������� ��������� ���������� ���������
        StartCoroutine(PassiveIncomeCoroutine()); // ������ �������� ���������� ������
        passiveIncome += 5; // ��������� ��������� �����

        SaveGameData(); // ��������� ������ ����
    }
    public void DoubleIncome()
    {
        totalCoins -= 10000; // ��������� ���������� ����� �� 10000
        totalCoins -= doubleIncomeCost; // ��������� ���������� ����� �� ��������� ��������
        clickPower *= 2; // ������� ���� �����
        passiveIncome *= 2; // ������� ��������� �����

        SaveGameData(); // ��������� ������ ����
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll(); // ������� ��� ����������� ������
        StopAllCoroutines(); // ���������� ��� ��������
        totalCoins = 0; // �������� ���������� �����
        totalClicks = 0;
        clickPower = 5; // �������� ���� �����
        currentUpgradeCost = 500; // �������� ��������� ���������
        passiveIncomeCost = 1000; // �������� ��������� ���������� ������
        passiveIncome = 0; // �������� ��������� �����


        firstQuestImage.color = new Color(0.2f, 0.2f, 0.2f);
        secondQuestImage.color = new Color(0.2f, 0.2f, 0.2f);
        thirdQuestImage.color = new Color(0.2f, 0.2f, 0.2f);
        fourthQuestImage.color = new Color(0.2f, 0.2f, 0.2f);
        fifteenQuestImage.color = new Color(0.2f, 0.2f, 0.2f);
        sixthQuestImage.color = new Color(0.2f, 0.2f, 0.2f);
        secondQuestImage.color = new Color(0.2f, 0.2f, 0.2f);
        eigitQuestImage.color = new Color(0.2f, 0.2f, 0.2f);
        nineQuestImage.color = new Color(0.2f, 0.2f, 0.2f);
    }

    private void LoadGameData()
    {
        totalCoins = PlayerPrefs.GetInt("totalCoins");

        clickPower = PlayerPrefs.HasKey("clickPower") ? PlayerPrefs.GetInt("clickPower") : 5;
        currentUpgradeCost = PlayerPrefs.HasKey("currentUpgradeCost") ? PlayerPrefs.GetInt("currentUpgradeCost") : 500;
        passiveIncome = PlayerPrefs.HasKey("passiveIncome") ? PlayerPrefs.GetInt("passiveIncome") : 0;
        passiveIncomeCost = PlayerPrefs.HasKey("passiveIncomeCost") ? PlayerPrefs.GetInt("passiveIncomeCost") : 1000;
        totalClicks = PlayerPrefs.HasKey("totalClicks") ? PlayerPrefs.GetInt("totalClicks") : 0;
    }

    private void SaveGameData()
    {
        PlayerPrefs.SetInt("passiveIncomeCost", passiveIncomeCost);
        PlayerPrefs.SetInt("totalCoins", totalCoins);
        PlayerPrefs.SetInt("currentUpgradeCost", currentUpgradeCost);
        PlayerPrefs.SetInt("clickPower", clickPower);
        PlayerPrefs.SetInt("passiveIncome", passiveIncome);
        PlayerPrefs.SetInt("totalClicks", totalClicks);
    }

    IEnumerator PassiveIncomeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // ���� 1 �������
            totalCoins += passiveIncome; // ����������� ���������� ����� �� ��������� �����
        }
    }

    IEnumerator DelayedPassiveIncome(int thresholdPassive)
    {
        while (passiveIncome < thresholdPassive)
        {
            yield return null; // ����, ���� ������� �� ����� ���������
        }

        // ���������� �������� ������ ���� ���
        if (!IsInvoking("AddPassiveIncome"))
        {
            InvokeRepeating("AddPassiveIncome", 0f, 1f); // ��������� ����� ������ �������
        }
    }

    private void AddPassiveIncome()
    {
        totalCoins += passiveIncome; // ����������� ���������� ����� �� ��������� �����
    }
}

