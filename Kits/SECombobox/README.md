# SECombobox 
A combobox which can be customizated for WPF.

![Den](https://media.giphy.com/media/ZGxdq4dSN8LoZRocHb/giphy.gif)

## Functions:
```
public void SelectItemByText(string Text)
```
- > Selects the item which you defined Text
    - > `Text` Text value of the item which will be selected
 ```
public void SelectItemByIndex(int Index)
```
- > Selects the item which you defined Index
    - > `Index` Index of the item in the combobox, which will be selected
```
public TKey GetSelectedItemSpecificValue<TKey>(string PropertyNames)
```
- > Gets specific property's value which you defined from selected item in Combobox.
    - > `PropertyNames` Properties of selected item type of the combobox
- > Returns spesific property's value from selected item in Combobox. If selected item does not exist or it doesn't contain a property which you defined, it returns default value

**Detail Description:** It provides that you can get a spesific value of a property inside of some UIElements when items in the combobox contain UIElements. Consider that you have added a stack panel item to the combobox. And the stack panel contains a GridPanel and the GridPanel contains a TextBlock. If we want to get text value of the TextBlock from SelectedItem, we can easily get this value by using this function. In our case, we should use this way:
```
SECombobox thisCombobox = new SECombobox();
// Your codes here to customizate thisCombobox

// The stackPanel's first child ([0]) is the GridPanel,
// The GridPanel's first child ([0]) is the TextBlock,
// Text value of the TextBlock is what we want to get.

string selectedTextValue = thisCombobox.GetSelectedItemSpecificValue<string>("Children[0].Children[0].Text");
```
