using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Clicker : MonoBehaviour
{
    public int totalCoins; // Общее количество монет
    public int totalClicks; // Общее количество кликов

    public TextMeshProUGUI coinsText; // Текстовое поле для отображения количества монет

    // Увеличение клика
    private int clickPower; // Сила клика
    private float upgradeMultiplier; // Множитель для улучшения
    private int currentUpgradeCost; // Стоимость улучшения
    public TextMeshProUGUI clickPowerText; // Текст для отображения силы клика
    public TextMeshProUGUI currentUpgradeCostText; // Текст для отображения стоимости улучшения

    // Увеличение пассивного дохода
    private int passiveIncome; // Пассивный доход
    private int passiveIncomeCost; // Стоимость пассивного дохода
    public TextMeshProUGUI passiveIncomeText; // Текст для отображения пассивного дохода
    public TextMeshProUGUI passiveIncomeCostText; // Текст для отображения стоимости пассивного дохода

    // Удвоение дохода
    private int doubleIncomeCost; // Стоимость удвоения дохода

    // Дополнительные переменные для пассивного дохода
    private int passiveIncomeAmount; // Пассивный доход за раз
    private int doubleIncomeMultiplier; // Множитель для удвоения

    // Кнопки
    public Button upgradeClickButton; // Кнопка для улучшения клика
    public Button upgradePassiveButton; // Кнопка для улучшения пассивного дохода
    public Button doubleIncomeButton; // Кнопка для удвоения дохода




    // Изображение для квеста
    public Image firstQuestImage; // Изображение для достижения квеста
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
            int passiveIncomeThreshold = 5; // Порог для активации корутины
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
        UpdateButtonStates(); // Обновить состояние кнопок
        UpdateUIText(); // Обновить текст на экране
        CheckQuestAchievements(); // Проверить достижения квестов

        SaveGameData();

        Debug.Log(passiveIncome);
    }

    private void UpdateButtonStates()
    {
        // Управление состоянием кнопок на основе количества монет
        upgradeClickButton.interactable = totalCoins >= currentUpgradeCost;
        upgradePassiveButton.interactable = totalCoins >= passiveIncomeCost;
        doubleIncomeButton.interactable = totalCoins >= 10001;
    }

    private void UpdateUIText()
    {
        // Обновление текстовых полей
        coinsText.text = totalCoins.ToString();
        currentUpgradeCostText.text = "(" + currentUpgradeCost.ToString() + " coins)";
        clickPowerText.text = "Click = " + clickPower.ToString();
        passiveIncomeText.text = "Passive = " + passiveIncome.ToString();
        passiveIncomeCostText.text = "(" + passiveIncomeCost.ToString() + " coins)";
    }

    public void Click()
    {
        totalCoins += clickPower; // Увеличить количество монет на силу клик
        totalClicks += 1; // Увеличить счетчик кликов

        SaveGameData(); // Сохранить данные игры
    }

    private void CheckQuestAchievements()
    {
        // Проверка выполнения условий для достижения квеста
        if (totalClicks > 999 && !firstQuest)
        {
            firstQuestImage.color = Color.white; // Меняем цвет изображения при достижении цели
            passiveIncome += 10;
            PlayerPrefs.SetInt("firstQuest", 1);
            firstQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // Запускаем метод каждую секунду
            }
        }
        if (totalClicks > 4999 && !secondQuest)
        {
            secondQuestImage.color = Color.white; // Меняем цвет изображения при достижении цел
            passiveIncome += 50;
            PlayerPrefs.SetInt("secondQuest", 1);
            secondQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // Запускаем метод каждую секунд
            }
        }
        if (totalClicks > 9999 && !thirdQuest)
        {
            thirdQuestImage.color = Color.white; // Меняем цвет изображения при достижении цел
            passiveIncome += 100;
            PlayerPrefs.SetInt("thirdQuest", 1);
            thirdQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // Запускаем метод каждую секунд
            }
        }
        if (totalClicks > 499 && !fourthQuest)
        {
            fourthQuestImage.color = Color.white; // Меняем цвет изображения при достижении цел
            clickPower += 5;
            PlayerPrefs.SetInt("fourthQuest", 1);
            fourthQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // Запускаем метод каждую секунд
            }
        }
        if (totalClicks > 2499 && !fifteenQuest)
        {
            fifteenQuestImage.color = Color.white; // Меняем цвет изображения при достижении цел
            clickPower += 25;
            PlayerPrefs.SetInt("fifteenQuest", 1);
            fifteenQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // Запускаем метод каждую секунд
            }
        }
        if (totalClicks > 5999 && !sixthQuest)
        {
            sixthQuestImage.color = Color.white; // Меняем цвет изображения при достижении цел
            clickPower += 60;
            PlayerPrefs.SetInt("sixthQuest", 1);
            sixthQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // Запускаем метод каждую секунд
            }
        }
        if (totalClicks > 3999 && !sevenQuest)
        {
            sevenQuestImage.color = Color.white; // Меняем цвет изображения при достижении цел
            totalCoins += 100000;
            PlayerPrefs.SetInt("sevenQuest", 1);
            sevenQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // Запускаем метод каждую секунд
            }
        }
        if (totalClicks > 8999 && !eigitQuest)
        {
            eigitQuestImage.color = Color.white; // Меняем цвет изображения при достижении цел
            totalCoins += 250000;
            PlayerPrefs.SetInt("eigitQuest", 1);
            eigitQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // Запускаем метод каждую секунд
            }
        }
        if (totalClicks > 19999 && !nineQuest)
        {
            nineQuestImage.color = Color.white; // Меняем цвет изображения при достижении цел
            totalCoins += 2000000;
            PlayerPrefs.SetInt("nineQuest", 1);
            nineQuest = true;

            if (!IsInvoking("AddPassiveIncome"))
            {
                InvokeRepeating("AddPassiveIncome", 0f, 1f); // Запускаем метод каждую секунд
            }
        }
    }

    public void UpgradeClickPower()
    {
        totalCoins -= currentUpgradeCost; // Уменьшить количество монет на стоимость улучшения
        currentUpgradeCost += 250; // Увеличить стоимость следующего улучшения
        clickPower += 5; // Увеличить силу клика

        SaveGameData(); // Сохранить данные игры
    }

    public void UpgradePassiveIncome()
    {
        totalCoins -= passiveIncomeCost; // Уменьшить количество монет на стоимость повышения дохода
        passiveIncomeCost += 500; // Увеличить стоимость следующего улучшения
        StartCoroutine(PassiveIncomeCoroutine()); // Начать корутину пассивного дохода
        passiveIncome += 5; // Увеличить пассивный доход

        SaveGameData(); // Сохранить данные игры
    }
    public void DoubleIncome()
    {
        totalCoins -= 10000; // Уменьшить количество монет на 10000
        totalCoins -= doubleIncomeCost; // Уменьшить количество монет на стоимость удвоения
        clickPower *= 2; // Удвоить силу клика
        passiveIncome *= 2; // Удвоить пассивный доход

        SaveGameData(); // Сохранить данные игры
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll(); // Удалить все сохраненные данные
        StopAllCoroutines(); // Остановить все корутины
        totalCoins = 0; // Сбросить количество монет
        totalClicks = 0;
        clickPower = 5; // Сбросить силу клика
        currentUpgradeCost = 500; // Сбросить стоимость улучшения
        passiveIncomeCost = 1000; // Сбросить стоимость пассивного дохода
        passiveIncome = 0; // Сбросить пассивный доход


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
            yield return new WaitForSeconds(1); // Ждем 1 секунду
            totalCoins += passiveIncome; // Увеличиваем количество монет на пассивный доход
        }
    }

    IEnumerator DelayedPassiveIncome(int thresholdPassive)
    {
        while (passiveIncome < thresholdPassive)
        {
            yield return null; // Ждем, пока условие не будет выполнено
        }

        // Запускайте корутину только один раз
        if (!IsInvoking("AddPassiveIncome"))
        {
            InvokeRepeating("AddPassiveIncome", 0f, 1f); // Запускаем метод каждую секунду
        }
    }

    private void AddPassiveIncome()
    {
        totalCoins += passiveIncome; // Увеличиваем количество монет на пассивный доход
    }
}

