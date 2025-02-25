using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class AddressableLoader
{
    public static async Task<List<T>> LoadAllAssets<T>(string label)
    {
        List<T> assets = new List<T>();

        // Load all assets with the given label
        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, null);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            assets.AddRange(handle.Result);
        }
        else
        {
            Debug.LogError($"Failed to load {typeof(T).Name} assets from Addressables with label '{label}'.");
        }

        return assets;
    }

    public static async Task<List<Dialog>> LoadAllDialogs()
    {
        return await AddressableLoader.LoadAllAssets<Dialog>(AddressableLabels.ADDRESSABLE_LABEL_DIALOGS);
    }

    public static async Task<List<Item>> LoadAllItems()
    {
        return await AddressableLoader.LoadAllAssets<Item>(AddressableLabels.ADDRESSABLE_LABEL_ITEMS);
    }

    public static async Task<List<NPC>> LoadAllNPCs()
    {
        return await AddressableLoader.LoadAllAssets<NPC>(AddressableLabels.ADDRESSABLE_LABEL_NPCS);
    }

    public static async Task<List<Tutorial>> LoadAllTutorials()
    {
        return await AddressableLoader.LoadAllAssets<Tutorial>(AddressableLabels.ADDRESSABLE_LABEL_TUTORIALS);
    }

    public static async Task<List<Quest>> LoadAllQuests()
    {
        return await AddressableLoader.LoadAllAssets<Quest>(AddressableLabels.ADDRESSABLE_LABEL_QUESTS);
    }
}