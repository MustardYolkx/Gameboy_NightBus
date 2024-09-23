using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassengerDialogue : MonoBehaviour
{
    public GameObject dialoguePrefab;
    private Transform canvasTransform; // 父级Canvas的位置
    private Transform fixedDialoguePosition; // eyemon, Loudmon, Coldmon 固定的生成位置
    private List<Transform> dialoguePositions = new List<Transform>(); // Talkmon 使用的多个生成位置

    private PassengerSeatIndex seatIndex;
    private GameRoot gameRoot;

    private Dictionary<string, List<string>> dialogues = new Dictionary<string, List<string>>();
    private List<string> talkmonDialogueList = new List<string>();

    private GameObject currentDialogue;
    private bool isDialogueActive = false;
    private string passengerName;
    private float typeSpeed = 0.05f; // 可编辑的对话加载速度
    private float dialogueDuration = 3f; // 对话框存在时间
    private Transform currentDialoguePosition;

    // 初始化
    private void Start()
    {
        seatIndex = GetComponent<PassengerSeatIndex>();
        gameRoot = GameRoot.GetInstance();

        // 查找 Canvas Transform
        canvasTransform = GameObject.Find("Canvas").transform;

        // 查找固定对话框生成位置
        fixedDialoguePosition = canvasTransform.Find("FixedDialoguePosition");

        // 查找 Talkmon 使用的多个对话框生成位置
        dialoguePositions.Add(canvasTransform.Find("DialoguePosition1"));
        dialoguePositions.Add(canvasTransform.Find("DialoguePosition2"));
        dialoguePositions.Add(canvasTransform.Find("DialoguePosition3"));
        dialoguePositions.Add(canvasTransform.Find("DialoguePosition4"));

        // 初始化每个乘客的台词
        InitializeDialogues();

        // 获取 Passenger 名称
        passengerName = seatIndex.thisPassenger.passengerName;
    }

    // 初始化乘客的台词
    private void InitializeDialogues()
    {
        dialogues["Eyemon"] = new List<string> { "Wanna music...", "M/>'.^%&USIC!", "♫~" };
        dialogues["Loudmon"] = new List<string> { "Beep beep!", "Beeeeeeeeeeeeeeeeeeeeeep!", "[Groaning]" };
        dialogues["Coldmon"] = new List<string> { "Barbecue in the fire...", "Hellish Heat! Melting!", "Wuhu~ Cold air!" };
        talkmonDialogueList = new List<string> {
        "Lonely...",
        "Yayaya~ha!",
        "Sad...",
        "Buttons...",
        "Awkward...",
        "Wanna talk...",
        "Friends in here...",
        "Ulah Ha!"
    };
    }

    // 生成对话框（公共接口）
    public void ShowDialogue(string passengerName, string dialogueType)
    {
        if (isDialogueActive) return;

        isDialogueActive = true;

        if (passengerName != "Talkmon")
        {
            ShowDialogueText(dialogues[passengerName][0], fixedDialoguePosition); // 显示第1句台词 (Need)，固定位置
        }
        else
        {
            StartCoroutine(GenerateRandomTalkmonDialogue());
        }
    }

    // 核心生成对话框的逻辑
    private void ShowDialogueText(string text, Transform position)
    {
        // 生成对话框时设置活动状态
        isDialogueActive = true;
        currentDialogue = Instantiate(dialoguePrefab, position.position, Quaternion.identity, canvasTransform);

        // 查找并显示TextMeshPro组件的文字
        TextMeshProUGUI dialogueText = currentDialogue.GetComponentInChildren<TextMeshProUGUI>();

        if (dialogueText != null)
        {
            StartCoroutine(TypeDialogue(text, dialogueText));
        }

        // 销毁对话框计时器
        StartCoroutine(RemoveDialogueAfterTime(dialogueDuration));
    }

    // 对话框定时销毁
    IEnumerator RemoveDialogueAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (currentDialogue != null)
        {
            Destroy(currentDialogue);
            currentDialogue = null;
        }

        // 重置状态
        isDialogueActive = false;
    }

    // Talkmon的随机生成对话框
    private IEnumerator GenerateRandomTalkmonDialogue()
    {
        int lastIndex = -1;

        while (true)
        {
            // 随机选择不同的位置
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, dialoguePositions.Count);
            }
            while (randomIndex == lastIndex);

            lastIndex = randomIndex;

            currentDialoguePosition = dialoguePositions[randomIndex];

            // 随机选择 Talkmon 的台词
            int randomLineIndex = Random.Range(0, talkmonDialogueList.Count);
            string randomLine = talkmonDialogueList[randomLineIndex];

            // 显示对话框
            ShowDialogueText(randomLine, currentDialoguePosition);

            yield return new WaitForSeconds(dialogueDuration);
        }
    }

    // 逐字显示文本
    private IEnumerator TypeDialogue(string text, TextMeshProUGUI dialogueText)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    // 清除所有对话框（公共接口）
    public void ClearAllDialogues()
    {
        if (currentDialogue != null)
        {
            Destroy(currentDialogue);
        }

        StopAllCoroutines();
        isDialogueActive = false;
    }

    // 检查 Ghost 需求的满足情况，并在指定位置生成对话框
    public void CheckGhostNeeds()
    {
        if (seatIndex.thisPassenger.ghostNeed == PowerfulGhostNeed.Music && gameRoot.musicOn)
        {
            // 如果满足 Music 需求，在固定位置生成对话框并显示文本
            ShowDialogueText(dialogues["Eyemon"][2], fixedDialoguePosition); // Eyemon 的 Complete 文本
        }
        else if (seatIndex.thisPassenger.ghostNeed == PowerfulGhostNeed.AirCondition && gameRoot.airConditionOn)
        {
            // 如果满足 AirCondition 需求，在固定位置生成对话框并显示文本
            ShowDialogueText(dialogues["Coldmon"][2], fixedDialoguePosition); // Coldmon 的 Complete 文本
        }
        else if (seatIndex.thisPassenger.ghostNeed == PowerfulGhostNeed.Horn && gameRoot.hornOn)
        {
            // 如果满足 Horn 需求，在固定位置生成对话框并显示文本
            ShowDialogueText(dialogues["Loudmon"][2], fixedDialoguePosition); // Loudmon 的 Complete 文本
        }
    }

}
