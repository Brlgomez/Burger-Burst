using System;
using UnityEngine;
using UnityEngine.Purchasing;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string coins_100 = "100_coins";
    public static string coins_250 = "250_coins";
    public static string coins_1000 = "1000_coins";
    public static string coins_2500 = "2500_coins";

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(coins_100, ProductType.Consumable);
        builder.AddProduct(coins_250, ProductType.Consumable);
        builder.AddProduct(coins_1000, ProductType.Consumable);
        builder.AddProduct(coins_2500, ProductType.Consumable);

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void Buy100Coins()
    {
        BuyProductID(coins_100);
    }

    public void Buy250Coins()
    {
        BuyProductID(coins_250);
    }

    public void Buy1000Coins()
    {
        BuyProductID(coins_1000);
    }

    public void Buy2500Coins()
    {
        BuyProductID(coins_2500);
    }

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, coins_100, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GetComponent<PlayerPrefsManager>().IncreaseCoins(100);
            GetComponent<VibrationManager>().SuccessTapticFeedback();
            GetComponent<SoundAndMusicManager>().PlayIAPSound();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, coins_250, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GetComponent<PlayerPrefsManager>().IncreaseCoins(250);
            GetComponent<VibrationManager>().SuccessTapticFeedback();
            GetComponent<SoundAndMusicManager>().PlayIAPSound();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, coins_1000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GetComponent<PlayerPrefsManager>().IncreaseCoins(1000);
            GetComponent<VibrationManager>().SuccessTapticFeedback();
            GetComponent<SoundAndMusicManager>().PlayIAPSound();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, coins_2500, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GetComponent<PlayerPrefsManager>().IncreaseCoins(2500);
            GetComponent<VibrationManager>().SuccessTapticFeedback();
            GetComponent<SoundAndMusicManager>().PlayIAPSound();
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public string GetPrice100()
    {
        if (IsInitialized())
        {
            if (m_StoreController.products.WithID(coins_100).availableToPurchase)
            {
                return m_StoreController.products.WithID(coins_100).metadata.localizedPriceString;
            }
        }
        return "";
    }

    public string GetPrice250()
    {
        if (IsInitialized())
        {
            if (m_StoreController.products.WithID(coins_250).availableToPurchase)
            {
                return m_StoreController.products.WithID(coins_250).metadata.localizedPriceString;
            }
        }
        return "";
    }

    public string GetPrice1000()
    {
        if (IsInitialized())
        {
            if (m_StoreController.products.WithID(coins_1000).availableToPurchase)
            {
                return m_StoreController.products.WithID(coins_1000).metadata.localizedPriceString;
            }
        }
        return "";
    }

    public string GetPrice2500()
    {
        if (IsInitialized())
        {
            if (m_StoreController.products.WithID(coins_2500).availableToPurchase)
            {
                return m_StoreController.products.WithID(coins_2500).metadata.localizedPriceString;
            }
        }
        return "";
    }
}
