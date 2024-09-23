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
    //private float dialogueInterval = 2.5f; // 对话生成间隔
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


        // 初始化每个乘客的台词
        InitializeDialogues();

        // 获取 Passenger 名称
        passengerName = seatIndex.thisPassenger.passengerName;


        // 检查乘客是否需要触发对话逻辑
        if (passengerName == "Eyemon" || passengerName == "Loudmon" || passengerName == "Coldmon" || passengerName == "Talkmon")
        {

            Debug.Log(passengerName + " is sitting, triggering dialogue...");
            TriggerDialogue("Need"); // 开始显示 Need 文本

        }
    }

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
        // 生成对话框时设置活动状态
        isDialogueActive = true;  // 确保对话框生成时，活动状态设置为 true
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

    IEnumerator RemoveDialogueAfterTime(float time)
    {
        // 确保对话框在指定时间后被移除
        yield return new WaitForSeconds(time);

        if (currentDialogue != null)
        {
            Destroy(currentDialogue);  // 移除当前对话框
            Debug.Log("Dialogue removed after " + time + " seconds.");
            currentDialogue = null;  // 清空引用，防止再次使用这个引用
        }

        // 重置状态，允许生成下一个对话框
        isDialogueActive = false;
        Debug.Log("isDialogueActive reset to false.");
    }



    IEnumerator GenerateRandomTalkmonDialogue()
    {
        int lastIndex = -1;  // 用于记录上一次生成的位置

        while (true)  // 持续生成对话
        {
            // 确保不会选择上一次的位置
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, dialoguePositions.Count);
            }
            while (randomIndex == lastIndex);  // 如果随机到的索引与上次相同，重新生成

            // 记录当前生成的位置索引
            lastIndex = randomIndex;

            currentDialoguePosition = dialoguePositions[randomIndex];
            Debug.Log("Talkmon dialogue at position: " + currentDialoguePosition.position);

            // 随机选择 Talkmon 的台词
            int randomLineIndex = Random.Range(0, talkmonDialogueList.Count);
            string randomLine = talkmonDialogueList[randomLineIndex];

            // 显示对话框
            ShowDialogue(randomLine, currentDialoguePosition);

            // 等待对话框消失后再生成下一个
            yield return new WaitForSeconds(dialogueDuration);

            isDialogueActive = false;  // 重置活动状态，允许生成下一个对话框
        }
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


    // 检查 Ghost 需求的满足情况
    public void CheckGhostNeeds()
    {
        if (seatIndex.thisPassenger.ghostNeed == PowerfulGhostNeed.Music && gameRoot.musicOn)
        {
            ShowDialogue(dialogues["Eyemon"][2], fixedDialoguePosition); // Eyemon的 Complete 文本
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

    // 在切换视角的时候清除生成的对话框
    public void ClearAllDialogues()
    {
        if (currentDialogue != null)
        {
            Destroy(currentDialogue);
        }

        StopAllCoroutines();
        isDialogueActive = false;
    }
    private void OnDisable()
    {
        ClearAllDialogues();
    }
}