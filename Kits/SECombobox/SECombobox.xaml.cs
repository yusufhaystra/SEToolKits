using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FutbolOyuncuBilgileri.Kartlar
{
    /// <summary>
    /// SECombobox.xaml Special Edition Combobox
    /// </summary>
    public partial class SECombobox : UserControl
    {

        #region Definitions

        public static readonly DependencyProperty PopupAnimationTypeProperty = DependencyProperty.Register("PopupAnimationType", typeof(PopupAnimation), typeof(SECombobox), new PropertyMetadata(PopupAnimation.Fade));

        public static readonly DependencyProperty RightBackgroundBrushProperty = DependencyProperty.Register("RightBackgroundBrush", typeof(Brush), typeof(SECombobox), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(178, 51, 121))));

        public static readonly DependencyProperty ArrowBrushProperty = DependencyProperty.Register("ArrowBrush", typeof(Brush), typeof(SECombobox), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public static readonly DependencyProperty ThisBorderBrushProperty = DependencyProperty.Register("ThisBorderBrush", typeof(Brush), typeof(SECombobox), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public static readonly DependencyProperty ThisBorderThicknessProperty = DependencyProperty.Register("ThisBorderThickness", typeof(Thickness), typeof(SECombobox), new PropertyMetadata(new Thickness(1)));

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(SECombobox), new PropertyMetadata(new PropertyChangedCallback(ItemTemplateChanged)));

        public static readonly DependencyProperty ThisCornerRadiusProperty = DependencyProperty.Register("ThisCornerRadius", typeof(CornerRadius), typeof(SECombobox), new PropertyMetadata(new CornerRadius(8), new PropertyChangedCallback(ThisCornerRadiusChanged)));

        public static readonly DependencyProperty RightCornerRadiusProperty = DependencyProperty.Register("RightCornerRadius", typeof(CornerRadius), typeof(SECombobox), new PropertyMetadata(new CornerRadius(0, 8, 8, 0)));

        public static readonly DependencyProperty RightColumnWidthProperty = DependencyProperty.Register("RightColumnWidth", typeof(double), typeof(SECombobox), new PropertyMetadata(32d));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource1", typeof(IEnumerable), typeof(SECombobox), new PropertyMetadata(new PropertyChangedCallback(ItemsSourceChanged)));

        private bool _IsLeftClickOn { get; set; }

        /// <summary>
        /// Represents state of clicking on left side to open items combobox. The default value is: False
        /// </summary>
        public bool IsLeftClickOn
        {
            get { return this._IsLeftClickOn; }
            set
            {
                this._IsLeftClickOn = value;
                if (this._IsLeftClickOn)
                {
                    this.ComboBox.MouseLeftButtonUp += ComboBox_MouseLeftButtonUp;
                }
                else
                {
                    this.ComboBox.MouseLeftButtonUp -= ComboBox_MouseLeftButtonUp;
                }
            }

        }



        /// <summary>
        /// Represents PopupAnimationType of the combobox
        /// </summary>
        public PopupAnimation PopupAnimationType
        {
            get { return (PopupAnimation)GetValue(PopupAnimationTypeProperty); }
            set { SetValue(PopupAnimationTypeProperty, value); }
        }

        /// <summary>
        /// Represents right background color of the combobox item
        /// </summary>
        public Brush RightBackgroundBrush
        {
            get { return (Brush)GetValue(RightBackgroundBrushProperty); }
            set { SetValue(RightBackgroundBrushProperty, value); }
        }

        /// <summary>
        /// Represents right arrow's color of the combobox item
        /// </summary>
        public Brush ArrowBrush
        {
            get { return (Brush)GetValue(ArrowBrushProperty); }
            set { SetValue(ArrowBrushProperty, value); }
        }

        /// <summary>
        /// Represents border color of the combobox item
        /// </summary>
        public Brush ThisBorderBrush
        {
            get { return (Brush)GetValue(ThisBorderBrushProperty); }
            set { SetValue(ThisBorderBrushProperty, value); }
        }

        /// <summary>
        /// Represents border thickness of the combobox
        /// </summary>
        public Thickness ThisBorderThickness
        {
            get { return (Thickness)GetValue(ThisBorderThicknessProperty); }
            set { SetValue(ThisBorderThicknessProperty, value); }
        }

        /// <summary>
        /// Represents left border child of the combobox item
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// Represents right border's radius
        /// </summary>
        public CornerRadius RightCornerRadius
        {
            get { return (CornerRadius)GetValue(RightCornerRadiusProperty); }
            set { SetValue(RightCornerRadiusProperty, value); }
        }

        /// <summary>
        /// Represents corner radius of the combobox item
        /// </summary>
        public CornerRadius ThisCornerRadius
        {
            get { return (CornerRadius)GetValue(ThisCornerRadiusProperty); }
            set { SetValue(ThisCornerRadiusProperty, value);}
        }

        /// <summary>
        /// Represents right column width of the combobox item.
        /// </summary>
        public double RightColumnWidth
        {
            get { return (double)GetValue(RightColumnWidthProperty); }
            set { SetValue(RightColumnWidthProperty, value); }
        }

        /// <summary>
        /// Represents item source of the combobox item.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Combobox Definitons:

        /// <summary>
        /// Gets or sets the first item in the current selection or returns null if the selection is empty
        /// </summary>
        /// <returns>The first item in the current selection or null if the selection is empty.</returns>
        public object SelectedItem
        {
            get { return this.ComboBox.SelectedItem; }
            set { this.ComboBox.SelectedItem = value; }
        }

        /// <summary>
        /// Gets or sets the index of the first item in the current selection or returns negative one (-1) if the selection is empty.
        /// </summary>
        ///<returns>The index of first item in the current selection. The default value is negative one (-1).</returns>
        public int SelectedIndex
        {
            get { return this.ComboBox.SelectedIndex; }
            set { 
                if (value >= -1)
                    this.ComboBox.SelectedIndex = value;
            }
        }


        /// <summary>
        /// Occurs when the selection of a System.Windows.Controls.Primitives.Selector changes.
        /// </summary>
        public event SelectionChangedEventHandler SelectionChanged
        {
            add { this.ComboBox.SelectionChanged += value; }
            remove { this.ComboBox.SelectionChanged -= value; }
        }


        #endregion

        #region Public events

        public static void ItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SECombobox thisCombobox = d as SECombobox;
            thisCombobox._ItemTemplateChanged(e);
        }

        public static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SECombobox thisCombobox = d as SECombobox;
            thisCombobox._ItemsSourceChanged(e);
        }

        public static void ThisCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SECombobox thisCombobox = d as SECombobox;
            thisCombobox._ThisCornerRadiusChanged(e);
            
        }



        public void _ThisCornerRadiusChanged(DependencyPropertyChangedEventArgs e)
        {
            // If RightCornerRadius value has not been defined, i will change RightCornerRadius value according to CornerRadius value.
            if (this.RightCornerRadius.TopLeft == 0 && this.RightCornerRadius.TopRight == 8 && this.RightCornerRadius.BottomLeft == 0 && this.RightCornerRadius.BottomRight == 8)
            {
                // It means RightCornerRadius value has not been defined.
                CornerRadius newCornerRadius = (CornerRadius)e.NewValue;
                this.RightCornerRadius = new CornerRadius(0, newCornerRadius.TopRight, newCornerRadius.BottomRight, 0);
            }

        }

        public void _ItemTemplateChanged(DependencyPropertyChangedEventArgs e)
        {
            // Setting items child :
            MainCombobox.ItemTemplate = (DataTemplate)e.NewValue;
        }

        public void _ItemsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
             this.MainCombobox.ItemsSource = (IEnumerable)e.NewValue;
            
        }

      

        #endregion


        /// <summary>
        /// Represents this Combobox
        /// </summary>
        public ComboBox ComboBox
        {
            get { return this.MainCombobox; }
            set { this.MainCombobox = value; }
        }

        /// <summary>
        /// Sets selected item according to Text parameter if the selected item's string equals to Text parameter
        /// </summary>
        /// <param name="Text">Text value which will be selected</param>
        public void SelectItemByText(string Text)
        {
            if (Text == null)
                return;
            // Checking all item's text of the combobox
            for (int ix = 0; ix < this.ComboBox.Items.Count; ix++)
            {
                if (this.ComboBox.Items[ix].ToString() == Text)
                {
                    this.ComboBox.SelectedIndex = ix; // Setting new selected index
                }
            }
        }

        /// <summary>
        /// Sets the item which you defined index
        /// </summary>
        /// <param name="Index">The index of item of the combobox, which will be selected </param>
        public void SelectItemByIndex(int Index)
        {
            // If the index smaller than zero, it can't be accepted.
            if (Index < 0)
                return;
            this.ComboBox.SelectedIndex = Index; // Setting new selected index
        }

        /// <summary>
        /// Gets specific property's value which you defined from selected item in Combobox.
        /// </summary>
        /// <param name="PropertyNames">Properties of selected item type of the combobox. Sample: "Children[0].Items[4].Text"</param>
        /// <returns>Returns spesific property's value from selected item in Combobox. If selected item does not exist or it doesn't contain a property which you defined, it returns default value</returns>
        public TKey GetSelectedItemSpecificValue<TKey>(string PropertyNames)
        {
            if (this.ComboBox.SelectedIndex == -1) // Checking the selected item exists
                return default;

            if (PropertyNames != null && PropertyNames.Length > 0)
            {
            
                object lastItem = this.ComboBox.SelectedItem; // For beginning: this is the real selected item of the combobox.
                Type lastContentType = lastItem.GetType(); // This value represents that the last viewed item.
                // But in below loop, it will represent the last item.

                /* In below loop, i will try to get properties of each classes.
                 One of them is done, it will be new content property type. For example:
                 Let's say the selected item's type is StackPanel and it contains a Grid item and also it contains a WrapPanel and the WrapPanel contains a TextBlock
                 And consider that i want to get the TextBlock's text.
                 In that case:

                 StackPanel => Grid => WrapPanel => TextBlock
                 Including properties to get them:     
                 --->
                 StackPanel => StackPanel.Children (This property returns UIElementCollection type and it contains a Grid item in our sample)
                 StackPanel.Children => StackPanel.Children[0(This index will defined as parameter)] And this returns UIElement. This UIElement is a GridPanel
                 --->
                 GridPanel => GridPanel.Children (This property returns UIElementCollection type and it contains a WrapPanel item in our sample)
                 GridPanel.Children => GridPanel.Children[0(This index will defined as parameter)] And this returns UIElement. This UIElement is a WrapPanel
                 And these things happen again and again until we are in last of PropertyNames array.
                 ...
                 WrapPanel => WrapPanel.Children (This property returns UIElementCollection type and it contains a TextBlock item in our sample)
                 WrapPanel.Children => WrapPanel.Children[0(This index will defined as parameter)] And this returns UIElement. This UIElement is a TextBlock
                 
                 I need to get text property of this TextBlock. The last thing that i need to do is that getting value of this property and return it.
                  
                */

                /* If there is a list in these types which there are lists in our sample, The indexes are included inside of PropertyNames array.
                 * No propety name can contain "[" or "]". So, if a propertyName contains a string which contains "[",
                 * it can't be a property name because as i said, no propety name can contain "[" !
                 * So if the string contains "[" which i can easily check it, it means the current lastContentType is a Collection or list or something else (doesn't matter)
                 * Sample: sampleStackPanel.Children[0] => "Children[0]" propertName contains "[" and it means that it's a list, array or collection.
                 */
                string[] _PropertyNames = PropertyNames.Split('.');
                if (_PropertyNames == null)
                    return default;
                if (_PropertyNames.Length == 0)
                    return default;

                foreach (string propertyName in _PropertyNames)
                {
                    // If this propertyName contains "[", it means that this is a List or e.g. and be needed only a spesific item in the list or e.g.
                    // I check this.
                    if (propertyName.Contains("["))
                    {
                        // It means this is a list or e.g.
                        // I need to find out list property's name and index of the item.
                        int indexBegin = propertyName.IndexOf("[") + 1;
                        int indexLast = propertyName.IndexOf("]");
                        // Checking the list property's name was defined correctly:
                        // Correct sample list property name: sampleStackPanel.Children[0]
                        // Wrong sample list property name: sampleStackPanel.Children[ OR sampleStackPanel.Children[a] ...
                        if (indexLast != -1 && indexBegin < propertyName.Length)
                        {
                            // It looks like correct for now:
                            string indexStringOfItem = propertyName.Substring(indexBegin, indexLast - indexBegin);
                            
                            if (int.TryParse(indexStringOfItem, out int indexOfItem))
                            {
                                // It means it's a number 
                                // I need to remove "[0]" from propertyName. For example, propertName equals to "Children[0]" and it's from a stackPanel.
                                // The real property name would be "Children" and we can acces with "Children" name actually. I found out the index and i have to remove it.
                                string newPropertyName = propertyName.Substring(0, indexBegin - 1);
                                PropertyInfo listPropertyInfo = lastContentType.GetProperty(newPropertyName); // Gets property of the selected type. If this property does not exist, it will return null
                                if (listPropertyInfo == null)
                                    return default;

                                object undefinedList = listPropertyInfo.GetValue(lastItem);
                                //Type collectionType = listPropertyInfo.GetType(); // For sampleStackPanel.Children[0], it would be UIElementCollection. Because "Children" is a UIElementCollection
                                if (undefinedList == null)
                                    return default;

                                if (undefinedList.GetType().IsArray) // It means it's an array
                                {
                                    // Getting array
                                    if (undefinedList is object[] arrayCollection)
                                        lastItem = arrayCollection[indexOfItem]; // Setting the last value of properties in this loop at the moment.
                                    else
                                        return default;

                                }
                                else // It means it's not an array, it's a list
                                {
                                    if (undefinedList is IList listCollection)
                                        lastItem = listCollection[indexOfItem]; // Setting the last value of properties in this loop at the moment.
                                    else
                                        return default;
                                }
                            }                           
                            else
                            {
                                // It means the between "[" and "]" thing is not a number (Like: Children[a]) so it's a wrong property name:
                                return default;
                            }
                        }
                        else
                        {
                            // It is definitly wrong property name because no property name contains "[".
                            return default;
                        }
                    }
                    else // It means it's a normal property or a list but not be needed a spesific item of the list.
                    {
                        // StackPanel => Last
                        PropertyInfo thisPropertyInfo = lastContentType.GetProperty(propertyName); // Gets property of the selected type. If this property does not exist, it will return null
                        if (thisPropertyInfo == null)
                            return default;

                        lastItem = thisPropertyInfo.GetValue(lastItem);
                    }

                    // In the last of the loop, i need to update type of lastItem :
                    lastContentType = lastItem.GetType();

                }

                // After above loop, lastItem will be wanted value. So, it's enough to return it:
                if (lastItem is TKey Thekey)
                    return Thekey;

                return default;
            }
            else
            {
                // It means, the exact value of selected item is being wanted -which is weird-
                if (this.ComboBox.SelectedItem is TKey Thekey)
                    return Thekey;

                return default;
            }
        
        }       

        /// <summary>
        /// Gets ready Special Edition Combobox control item
        /// </summary>
        public SECombobox()
        {
            InitializeComponent();
        }

        private void ComboBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // This event fires if IsLeftClickOn value equals to True
            this.ComboBox.IsDropDownOpen = true;
        }
    }


}
