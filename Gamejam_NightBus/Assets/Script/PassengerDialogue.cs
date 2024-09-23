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
    private float dialogueDuration = 5f; // 对话框存在时间
    private float dialogueInterval = 3f; // 对话生成间隔
    private Transform currentDialoguePosition;

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
        dialoguePositions.Add(canvasTransform.Find("DialoguePosition5"));
        dialoguePositions.Add(canvasTransform.Find("DialoguePosition6"));
        dialoguePositions.Add(canvasTransform.Find("DialoguePosition7"));
        dialoguePositions.Add(canvasTransform.Find("DialoguePosition8"));

        // 初始化每个乘客的台词
        InitializeDialogues();

        // 获取 Passenger 名称
        passengerName = seatIndex.thisPassenger.passengerName;

        // 检查乘客是否需要触发对话逻辑
        if (passengerName == "eyemon" || passengerName == "Loudmon" || passengerName == "Coldmon" || passengerName == "Talkmon")
        {
            // 检查乘客是否已坐下
            if (seatIndex.currentAnimState == "Sit")
            {
                TriggerDialogue("Need"); // 开始显示 Need 文本
            }
        }
    }

    private void InitializeDialogues()
    {
        dialogues["eyemon"] = new List<string> { "Wanna music...", "M/>'.^%&USIC!", "♫~" };
        dialogues["Loudmon"] = new List<string> { "Beep beep!", "Beeeeeeeeeeeeeeeeeeeeeep!", "[Groaning]" };
        dialogues["Coldmon"] = new List<string> { "Barbecue in the fire...", "Hellish Heat! Melting!", "Wuhu~ Cold air!" };
        talkmonDialogueList = new List<string> {
        "Lonely...",
        "Yayaya~ha!",
        "Sad...",
        "Buttons...",
        "Driving without talking...",
        "Awkward...",
        "Wanna talk...",
        "Friends in here...",
        "Biubiu~",
        "Ulah Ha!"
    };
    }

    // 触发对话
    public void TriggerDialogue(string dialogueType)
    {
        if (isDialogueActive) return;

        isDialogueActive = true;

        if (passengerName != "Talkmon")
        {
            ShowDialogue(dialogues[passengerName][0], fixedDialoguePosition); // 显示第1句台词 (Need)，固定位置
        }
        else
        {
            StartCoroutine(GenerateRandomTalkmonDialogue());
        }
    }

    private void ShowDialogue(string text, Transform position)
    {
        currentDialogue = Instantiate(dialoguePrefab, position.position, Quaternion.identity, canvasTransform);

        // 在这里动态查找 TextMeshProUGUI 组件，而不是依赖手动绑定
        TextMeshProUGUI dialogueText = currentDialogue.GetComponentInChildren<TextMeshProUGUI>();

        if (dialogueText != null)
        {
            StartCoroutine(TypeDialogue(text, dialogueText));
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found on dialoguePrefab.");
        }

        StartCoroutine(RemoveDialogueAfterTime(dialogueDuration));
    }

    IEnumerator TypeDialogue(string text, TextMeshProUGUI dialogueText)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    IEnumerator RemoveDialogueAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(currentDialogue);
        isDialogueActive = false;
    }

    IEnumerator GenerateRandomTalkmonDialogue()
    {
        while (true)
        {
            if (!isDialogueActive)
            {
                // 随机选择对话框生成位置
                int randomIndex = Random.Range(0, dialoguePositions.Count);
                currentDialoguePosition = dialoguePositions[randomIndex];

                // 随机选择 Talkmon 的台词
                int randomLineIndex = Random.Range(0, talkmonDialogueList.Count);
                string randomLine = talkmonDialogueList[randomLineIndex];

                ShowDialogue(randomLine, currentDialoguePosition);

                yield return new WaitForSeconds(dialogueDuration + dialogueInterval);
            }

            yield return null;
        }
    }

    // 检查 Ghost 需求的满足情况
    private void CheckGhostNeeds()
    {
        if (seatIndex.thisPassenger.ghostNeed == PowerfulGhostNeed.Music && gameRoot.musicOn)
        {
            ShowDialogue(dialogues["eyemon"][2], fixedDialoguePosition); // Eyemon的 Complete 文本
        }
        else if (seatIndex.thisPassenger.ghostNeed == PowerfulGhostNeed.AirCondition && gameRoot.airConditionOn)
        {
            ShowDialogue(dialogues["Coldmon"][2], fixedDialoguePosition); // Coldmon的 Complete 文本
        }
        else if (seatIndex.thisPassenger.ghostNeed == PowerfulGhostNeed.Horn && gameRoot.hornOn)
        {
            ShowDialogue(dialogues["Loudmon"][2], fixedDialoguePosition); // Loudmon的 Complete 文本
        }
    }

}
