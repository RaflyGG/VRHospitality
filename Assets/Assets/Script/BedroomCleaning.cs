using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class BedroomCleaning : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdThreshold = 2.0f; // Waktu dalam detik
    private float holdDownStartTime;
    private bool isHolding = false;
    public Image progressImage; // Gambar yang menunjukkan progress

    public GameObject bed;

    public UnityEvent completed;

    private void Start()
    {

        if (progressImage != null)
        {
            progressImage.fillAmount = 0; // Pastikan progress image di-reset
        }
    }

    private void Update()
    {
        if (isHolding)
        {
            float holdDuration = Time.time - holdDownStartTime;
            // Update progress image sesuai dengan waktu penekanan
            progressImage.fillAmount = Mathf.Clamp01(holdDuration / holdThreshold);

            if (holdDuration >= holdThreshold)
            {
                completed.Invoke();
                isHolding = false; // Hindari pemanggilan event berulang kali
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holdDownStartTime = Time.time; // Simpan waktu saat tombol mulai ditekan
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        float holdDownTime = Time.time - holdDownStartTime; // Hitung durasi penekanan
        if (holdDownTime >= holdThreshold)
        {
            completed.Invoke();// Panggil event jika tombol ditahan lebih dari 2 detik
        }
        // Reset progress image saat tombol dilepaskan
        if (progressImage != null)
        {
            progressImage.fillAmount = 0;
        }
    }


}

