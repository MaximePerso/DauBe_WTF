﻿using System.Windows;
using System.Windows.Controls;
using RealTimeGraphX;   

namespace DauBe_WTF
{
    public class WpfGraphControl : Control
    {
        /// <summary>
        /// Gets or sets the graph controller.
        /// </summary>
        public IGraphController Controller
        {
            get { return (IGraphController)GetValue(ControllerProperty); }
            set { SetValue(ControllerProperty, value); }
        }
        public static readonly DependencyProperty ControllerProperty =
            DependencyProperty.Register("Controller", typeof(IGraphController), typeof(WpfGraphControl), new PropertyMetadata(null));

        /// <summary>
        /// Initializes the <see cref="WpfGraphControl"/> class.
        /// </summary>
        static WpfGraphControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WpfGraphControl), new FrameworkPropertyMetadata(typeof(WpfGraphControl)));
        }
    }
}
