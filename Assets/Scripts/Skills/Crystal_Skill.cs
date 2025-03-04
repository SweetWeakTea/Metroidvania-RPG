using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal_Skill : Skill
{
    [SerializeField] private float crystalDuration;
    [SerializeField] private GameObject crystalPrefab;
    private GameObject currentCrystal;

    [Header("Crystal simple")]
    [SerializeField] private UI_SkillTreeSlot unlockCrystalButton;
    public bool crystalUnlocked {  get; private set; }

    [Header("Crystal mirage")]
    [SerializeField] private UI_SkillTreeSlot unlockCloneInsteadButton;
    public bool cloneInsteadOfCrystal {  get; private set; }

    [Header("Explosive crystal")]
    [SerializeField] private UI_SkillTreeSlot unlockExplosiveButton;
    [SerializeField] private float explosiveCooldown;
    public bool canExplode { get; private set; }

    [Header("Moving crystal")]
    [SerializeField] private UI_SkillTreeSlot unlockMovingCrystalButton;
    public bool canMoveToEnemy { get; private set; }
    [SerializeField] private float moveSpeed;

    [Header("Multi stacking crystal")]
    [SerializeField] private UI_SkillTreeSlot unlockMultiStackButton;
    public bool canUseMultiStacks { get; private set; }
    [SerializeField] private int amountOfStacks;
    [SerializeField] private float multiStackCooldown;
    [SerializeField] private float useTimeWindow;
    [SerializeField] private List<GameObject> crystalList = new List<GameObject>();

    protected override void Start()
    {
        base.Start();

        unlockCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockCrystal);
        unlockCloneInsteadButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalMirage);
        unlockExplosiveButton.GetComponent<Button>().onClick.AddListener(UnlockExplosiveCrystal);
        unlockMovingCrystalButton.GetComponent<Button>().onClick.AddListener(UnlockMovingCrystal);
        unlockMultiStackButton.GetComponent<Button>().onClick.AddListener(UnlockMultiStack);
    }

    // ���� Crystal ����
    #region Unlock skill region
    protected override void CheckUnlock()
    {
        UnlockCrystal();
        UnlockCrystalMirage();
        UnlockExplosiveCrystal();
        UnlockMovingCrystal();
        UnlockMultiStack();
    }

    private void UnlockCrystal()
    {
        if (unlockCrystalButton.unlocked)
            crystalUnlocked = true;
    }

    private void UnlockCrystalMirage()
    {
        if (unlockCloneInsteadButton.unlocked)
            cloneInsteadOfCrystal = true;
    }

    private void UnlockExplosiveCrystal()
    {
        if (unlockExplosiveButton.unlocked)
        {
            canExplode = true;
            cooldown = explosiveCooldown;
        }
    }

    private void UnlockMovingCrystal()
    {
        if (unlockMovingCrystalButton.unlocked)
            canMoveToEnemy = true;
    }

    private void UnlockMultiStack()
    {
        if (unlockMultiStackButton.unlocked)
            canUseMultiStacks = true;
    }
    #endregion

    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrystal())
            return;

        if (currentCrystal == null)
        {
            CreateCrystal();
        }
        else
        {
            if (canMoveToEnemy)
                return;

            Vector2 playerPos = player.transform.position;
            player.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = playerPos;

            if (cloneInsteadOfCrystal)
            {
                SkillManager.instance.clone.CreateClone(currentCrystal.transform, Vector3.zero);
                Destroy(currentCrystal);
            }
            else
            {
                currentCrystal.GetComponent<Crystal_Skill_Controller>()?.FinishCrystal();
            }
        }
    }

    public void CreateCrystal()
    {
        currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
        Crystal_Skill_Controller currentCrystalScript = currentCrystal.GetComponent<Crystal_Skill_Controller>();

        currentCrystalScript.SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(currentCrystal.transform), player);
    }

    public void CurrentCrystalChooseRandomTargers() => currentCrystal.GetComponent<Crystal_Skill_Controller>().ChooseRandomEnemy();

    private bool CanUseMultiCrystal()
    {
        if (canUseMultiStacks)
        {
            if (crystalList.Count > 0)
            {
                if (crystalList.Count == amountOfStacks)
                    Invoke("ResetAbility", useTimeWindow);

                cooldown = 0;
                GameObject crystalToSpawn = crystalList[crystalList.Count - 1];
                GameObject newCrystal = Instantiate(crystalToSpawn, player.transform.position, Quaternion.identity);

                crystalList.Remove(crystalToSpawn);

                newCrystal.GetComponent<Crystal_Skill_Controller>().
                    SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(newCrystal.transform), player);

                if (crystalList.Count <= 0)
                {
                    cooldown = multiStackCooldown;
                    RefillCrystal();
                }

                return true;
            }
        }

        return false;
    }

    private void RefillCrystal()
    {
        int amountToAdd = amountOfStacks - crystalList.Count;

        for (int i = 0; i < amountToAdd; i++)
        {
            crystalList.Add(crystalPrefab);
        }
    }

    private void ResetAbility()
    {
        if (cooldownTimer > 0)
            return;

        cooldownTimer = multiStackCooldown;
        RefillCrystal();
    }
}
