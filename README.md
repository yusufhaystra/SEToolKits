# SEToolKits
Special Edition Tool Kits are custom kits for WPF.

## Description
Kits of this project make your programs look better and customization. 
They are used to customizate some basic components (Like a combobox). It provides more customization option than basic components. 

I am going to add more kits and make them better

## How To Use
To use these kits:

* Add .xaml and .cs files of the kit(s) which you would like to add, to your project file.
* You can easily use the kit(s) in your project by calling their names. You can view properties and functions of them below.

1. **SECombobox**
   - Properties
     - `ArrowBrush`
     - `ThisBorderBrush`
     - `ThisBorderThickness`
     - `ItemTemplate`
     - `RightCornerRadius`
     - `ThisCornerRadius`
     - `RightColumnWidth`
     - `ItemsSource`
     - `SelectedItem`
     - `SelectedIndex`
     - `ComboBox`
   - Functions
     - `SelectItemByText(string Text)`
     - `SelectItemByIndex(int Index)`
     - `GetSelectedItemSpecificValue<TKey>(string PropertyNames)`
   - Events
     - `SelectionChanged`