using UnityEngine.Events;
using UnityEngine.UI;

public class UIButton : Button
{
    public void Initialize(UnityAction buttonAction)
    {
        print(buttonAction.Method.Name);

        onClick.AddListener(buttonAction);
        GetComponentInChildren<Text>().text = buttonAction.Method.Name;
    }
}
