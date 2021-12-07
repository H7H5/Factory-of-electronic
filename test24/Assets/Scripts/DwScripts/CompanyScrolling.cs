using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyScrolling : MonoBehaviour
{
    [Range(1, 20)]
    [Header("Controllers")]
    public int companyCount;
    [Range(0, 500)]
    public int companyOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 5f)]
    public float scaleOffset;
    [Range(1f, 20f)]
    public float scaleSpeed;

    [Header("Other objects")]
    public GameObject companyPrefab;
    public ScrollRect scrollRect;

    private GameObject[] instCompany;
    private Vector2[] companyPosition;
    private Vector2[] companyScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedCompanyId;
    private bool isScrolling;

    void Start()
    {
        contentRect = GetComponent<RectTransform>();
        instCompany = new GameObject[companyCount];
        companyPosition = new Vector2[companyCount];
        companyScale = new Vector2[companyCount];
        for (int i = 0; i < companyCount; i++)
        {
            instCompany[i] = Instantiate(companyPrefab, transform, false);
            if (i == 0) continue;
            instCompany[i].transform.localPosition = new Vector2(instCompany[i].transform.localPosition.x, 
                instCompany[i - 1].transform.localPosition.y + companyPrefab.GetComponent<RectTransform>().sizeDelta.y + companyOffset);
            companyPosition[i] = -instCompany[i].transform.localPosition;
        }
    }

    private void FixedUpdate()
    {
        if (contentRect.anchoredPosition.y >= companyPosition[0].y && !isScrolling || contentRect.anchoredPosition.y <= companyPosition[companyPosition.Length - 1].y && !isScrolling)
        {
            scrollRect.inertia = false;
        }
        float nereastPos = float.MaxValue;
        for (int i = 0; i < companyCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.y - companyPosition[i].y);
            if(distance < nereastPos)
            {
                nereastPos = distance;
                selectedCompanyId = i;
            }
            float scale = Mathf.Clamp(1 / (distance / companyOffset) * scaleOffset, 0.8f, 1f);
            companyScale[i].x = Mathf.SmoothStep(instCompany[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            companyScale[i].y = Mathf.SmoothStep(instCompany[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            instCompany[i].transform.localScale = companyScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, companyPosition[selectedCompanyId].y, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }
    
    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}
