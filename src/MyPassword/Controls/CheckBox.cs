using MyPassword.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MyPassword.Controls
{
    public class Checkbox : Image
    {

        private const string CheckboxUnCheckedImageName = "IconUnChecked";
        private const string CheckboxCheckedImageName = "IconChecked";


        public Checkbox()
        {
            Source = IconHelper.GetIcon(CheckboxUnCheckedImageName);
            var imageTapGesture = new TapGestureRecognizer();
            imageTapGesture.Tapped += ImageTapGestureOnTapped;
            GestureRecognizers.Add(imageTapGesture);
            PropertyChanged += OnPropertyChanged;
            IsClickable = true;
        }

        private void ImageTapGestureOnTapped(object sender, EventArgs eventArgs)
        {
            if (IsEnabled && IsClickable)
            {
                Checked = !Checked;
            }
        }
        

        /// <summary>
        /// The checked changed event.
        /// </summary>
        public event EventHandler<bool> CheckedChanged;

        /// <summary>
        /// The checked state property.
        /// </summary>
        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(bool), typeof(Checkbox), false, BindingMode.TwoWay, propertyChanged: OnCheckedPropertyChanged);

        public bool Checked
        {
            get
            {
                return (bool)GetValue(CheckedProperty);
            }

            set
            {
                if (Checked != value)
                {
                    SetValue(CheckedProperty, value);
                    CheckedChanged?.Invoke(this, value);
                }
            }
        }


        /// <summary>
        /// The checked changed event.
        /// </summary>
        public event EventHandler<bool> IsClickableChanged;

        public static readonly BindableProperty IsClickableProperty = BindableProperty.Create("IsClickable", typeof(bool), typeof(Checkbox), false, BindingMode.TwoWay, propertyChanged: OnIsClickablePropertyChanged);

        public bool IsClickable
        {
            get
            {
                return (bool)GetValue(IsClickableProperty);
            }

            set
            {
                if (IsClickable != value)
                {
                    SetValue(IsClickableProperty, value);
                    IsClickableChanged?.Invoke(this, value);
                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e?.PropertyName == IsEnabledProperty.PropertyName)
            {
                Opacity = IsEnabled ? 1 : 0.5;
            }
        }

        private static void OnIsClickablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var checkBox = bindable as Checkbox;
            if (checkBox != null)
            {
                var value = newValue as bool?;
                checkBox.IsClickable = value.GetValueOrDefault();
                //checkBox.Source = value.GetValueOrDefault() ? IconHelper.GetIcon(CheckboxCheckedImageName) : IconHelper.GetIcon(CheckboxUnCheckedImageName);
            }
        }


        private static void OnCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var checkBox = bindable as Checkbox;
            if (checkBox != null)
            {
                var value = newValue as bool?;
                checkBox.Checked = value.GetValueOrDefault();
                checkBox.Source = value.GetValueOrDefault() ? IconHelper.GetIcon(CheckboxCheckedImageName) : IconHelper.GetIcon(CheckboxUnCheckedImageName);
            }
        }



    }
}
