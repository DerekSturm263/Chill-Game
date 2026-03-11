using UnityEngine;

public class SerializedMethods : MonoBehaviour
{
    public void SpawnPlaceableObject(Asset asset)
    {
        PlaceableObject obj = asset as PlaceableObject;

        GameObject newItem = Instantiate(obj.Object);
        newItem.name = obj.name;
        PlaceableObjectInstance component = newItem.GetComponent<PlaceableObjectInstance>();

        component.SetObject(obj);
    }

    public void PopupShopAsset(Asset asset)
    {
        ButtonInfo[] buttons = new ButtonInfo[]
        {
            new()
            {
                label = "Purchase",
                enabled = SaveMethods.Current.basic_currency_temp >= asset.Price,
                onClick = () =>
                {
                    ++SaveMethods.Current.inventory[asset];
                    SaveMethods.Current.basic_currency_temp -= asset.Price;
                }
            },
            new()
            {
                label = "Cancel",
                enabled = true,
                onClick = () =>
                {
                    PopupMethods.Hide();
                }
            }
        };

        PopupMethods.Display(asset.name, asset.Description, asset.Icon, buttons);
    }
}
