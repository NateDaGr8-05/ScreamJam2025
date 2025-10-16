using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RageMeter : MonoBehaviour
{
    // Rage Settings
    public float currentRage = 0f;
    public float decayRate = 0.05f;
    public float ragePerKill = 0.5f;
    public float rageDuration = 10f;

    // Slider preset for top right
    public Slider rageSlider;

    // For visual distonction later
    public Image rageFill;

    private bool raging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Slowly decrease rage if not raging already
        if (!raging)
        {
            if (currentRage > 0)
            {
                currentRage -= decayRate * Time.deltaTime;
                currentRage = Mathf.Clamp01(currentRage);
            }
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (rageSlider != null)
        {
            rageSlider.value = currentRage;
        }

        if (rageFill != null)
        {
            rageFill.color = Color.Lerp(Color.yellow, Color.red, currentRage);
        }
    }

    public void AddRage()
    {
        // Don't add to rage meter if raging
        if (raging) return;

        currentRage += ragePerKill;


        if (currentRage >= 1f)
        {
            StartCoroutine(RageTimer());
        }

        //Debug.Log("rage added" + currentRage);
        UpdateUI();
    }

    public void ResetRage()
    {
        currentRage = 0f;
        UpdateUI();
    }

    private IEnumerator RageTimer()
    {
        raging = true;
        currentRage = 1f;
        UpdateUI();

        // Activate player rage buffs
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
            player.ActivateRage();

        // Stay at full rage for the duration
        float timer = 0f;
        while (timer < rageDuration)
        {
            currentRage = 1f;
            timer += Time.deltaTime;
            UpdateUI();
            yield return null;
        }

        // After duration, reset
        //Debug.Log("Rage ended");
        raging = false;
        ResetRage();
    }
}
