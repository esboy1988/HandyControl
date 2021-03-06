﻿using System.Windows;
using System.Windows.Controls.Primitives;
using HandyControl.Data;
using HandyControl.Expression.Shapes;

namespace HandyControl.Controls
{
    [TemplatePart(Name = IndicatorTemplateName, Type = typeof(Arc))]
    public class CircleProgressBar : RangeBase
    {
        private const string IndicatorTemplateName = "PART_Indicator";

        public static readonly DependencyProperty ArcThicknessProperty = DependencyProperty.Register(
            "ArcThickness", typeof(double), typeof(CircleProgressBar), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ShowTextProperty = DependencyProperty.Register(
            "ShowText", typeof(bool), typeof(CircleProgressBar), new PropertyMetadata(true));

        private Arc _indicator;

        static CircleProgressBar()
        {
            FocusableProperty.OverrideMetadata(typeof(CircleProgressBar),
                new FrameworkPropertyMetadata(BooleanBoxes.FalseBox));
            MaximumProperty.OverrideMetadata(typeof(CircleProgressBar), new FrameworkPropertyMetadata(100.0));
        }

        public bool ShowText
        {
            get => (bool) GetValue(ShowTextProperty);
            set => SetValue(ShowTextProperty, value);
        }

        public double ArcThickness
        {
            get => (double) GetValue(ArcThicknessProperty);
            set => SetValue(ArcThicknessProperty, value);
        }

        private void SetProgressBarIndicatorAngle()
        {
            if (_indicator == null) return;
            var minimum = Minimum;
            var maximum = Maximum;
            var num = Value;
            _indicator.EndAngle = (maximum <= minimum ? 0 : (num - minimum) / (maximum - minimum)) * 360;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _indicator = GetTemplateChild(IndicatorTemplateName) as Arc;
            if (_indicator != null)
            {
                _indicator.StartAngle = 0;
                _indicator.EndAngle = 0;
            }

            SetProgressBarIndicatorAngle();
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            SetProgressBarIndicatorAngle();
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            SetProgressBarIndicatorAngle();
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            SetProgressBarIndicatorAngle();
        }
    }
}