using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private IStoreController controller;
    private IExtensionProvider extensions;

    void Start()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("100_coins", ProductType.Consumable, new IDs
        {
            {"100_coins", GooglePlay.Name},
            {"100_coins", AppleAppStore.Name}
        });
        builder.AddProduct("250_coins", ProductType.Consumable, new IDs
        {
            {"250_coins", GooglePlay.Name},
            {"250_coins", AppleAppStore.Name}
        });
        builder.AddProduct("1000_coins", ProductType.Consumable, new IDs
        {
            {"1000_coins", GooglePlay.Name},
            {"1000_coins", AppleAppStore.Name}
        });
        builder.AddProduct("2500_coins", ProductType.Consumable, new IDs
        {
            {"2500_coins", GooglePlay.Name},
            {"2500_coins", AppleAppStore.Name}
        });
        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// Called when Unity IAP is ready to make purchases.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }

    /// <summary>
    /// Called when Unity IAP encounters an unrecoverable initialization error.
    ///
    /// Note that this will not be called if Internet is unavailable; Unity IAP
    /// will attempt initialization until it becomes available.
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error) { }

    /// <summary>
    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        switch (e.purchasedProduct.definition.id)
        {
            case "100_coins":
                GetComponent<PlayerPrefsManager>().IncreaseCoins(100);
                break;
            case "250_coins":
                GetComponent<PlayerPrefsManager>().IncreaseCoins(250);
                break;
            case "1000_coins":
                GetComponent<PlayerPrefsManager>().IncreaseCoins(1000);
                break;
            case "2500_coins":
                GetComponent<PlayerPrefsManager>().IncreaseCoins(2500);
                break;
        }
        GetComponent<VibrationManager>().SuccessTapticFeedback();
        GetComponent<SoundAndMusicManager>().PlayIAPSound();
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        GetComponent<VibrationManager>().ErrorTapticFeedback();
    }

    public void OnPurchaseClicked100()
    {
        controller.InitiatePurchase("100_coins");
    }

    public void OnPurchaseClicked250()
    {
        controller.InitiatePurchase("250_coins");
    }

    public void OnPurchaseClicked1000()
    {
        controller.InitiatePurchase("1000_coins");
    }

    public void OnPurchaseClicked2500()
    {
        controller.InitiatePurchase("2500_coins");
    }

    public string GetPrice100()
    {
        return controller.products.WithID("100_coins").metadata.localizedPriceString;
    }

    public string GetPrice250()
    {
        return controller.products.WithID("250_coins").metadata.localizedPriceString;
    }

    public string GetPrice1000()
    {
        return controller.products.WithID("1000_coins").metadata.localizedPriceString;
    }

    public string GetPrice2500()
    {
        return controller.products.WithID("2500_coins").metadata.localizedPriceString;
    }
}